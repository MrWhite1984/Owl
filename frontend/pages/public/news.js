import { API_ROUTES } from '/js/config/api.js';

export function init() {
  'use strict';

  const existingHandler = window._newsScrollHandler;
  if (existingHandler) {
    window.removeEventListener('scroll', existingHandler);
  }

  let allNews = [];
  let nextDateTime = null;
  let isLoading = false;
  let hasMore = true;

  const newsContainer = document.getElementById('news-container');
  const loadingEl = document.getElementById('loading');
  const noMoreEl = document.getElementById('no-more');

  if (!newsContainer) {
    console.warn('news.js: #news-container not found');
    return;
  }

  if (noMoreEl) noMoreEl.style.display = 'none';

  function autolinkUrls(text) {
    if (!text) return '';

    const div = document.createElement('div');
    div.textContent = text;
    let escapedText = div.innerHTML;

    const urlRegex = /https?:\/\/(www\.)?[-a-zA-Z0-9@:%._+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_+.~#?&//=]*)/g;

    return escapedText.replace(urlRegex, url => {
      try {
        new URL(url);
        return `<a href="${url}" target="_blank" rel="noopener noreferrer" class="link link-primary">${url}</a>`;
      } catch {
        return url;
      }
    });
  }

  function renderNewsItem(newsItem) {
    const author = newsItem.author || {};
    const isVerified = author.isVerified === true;

    const verifiedBadge = isVerified
      ? `<div class="flex items-center gap-1">
          <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 text-green-500" viewBox="0 0 20 20" fill="currentColor">
            <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z" clip-rule="evenodd"/>
          </svg>
        </div>`
      : '';

    function escapeHtml(text) {
      const div = document.createElement('div');
      div.textContent = text;
      return div.innerHTML;
    }

    const surname = escapeHtml(author.surname || '');
    const name = escapeHtml(author.name || '');
    const patronymic = escapeHtml(author.patronymic || '');
    const jobTitle = escapeHtml(author.jobTitle || '');
    const title = escapeHtml(newsItem.title || '');
    const content = autolinkUrls(newsItem.content || '');
    const currentLang = localStorage.getItem('lang') || (navigator.language.startsWith('ru') ? 'ru' : 'en');
    const date = newsItem.createdAt
      ? new Date(newsItem.createdAt).toLocaleDateString(currentLang, {
          year: 'numeric',
          month: 'long',
          day: 'numeric'
        })
      : '';

    const article = document.createElement('article');
    article.className = 'border border-base-300 rounded-lg p-5 hover:shadow-md transition-shadow';
    article.innerHTML =
      `<h2 class="text-xl font-semibold mb-2">${title}</h2>` +
      `<p>${content}</p>` +
      `<div class="text-sm text-gray-500 mb-3 flex items-center gap-4">` +
        `<span>${date}</span>` +
        `<div class="flex items-center gap-1">` +
          `<span class="font-medium">${surname} ${name} ${patronymic}</span>` +
          `<span>—</span>` +
          `<span>${jobTitle}</span>` +
        `</div>` +
        verifiedBadge +
      `</div>`;

    return article;
  }

  function renderAllNews() {
    newsContainer.innerHTML = '';

    // ВСЕГДА отображаем кнопку, если роль подходит
    const selectedRole = localStorage.getItem("selectedRole");
    if (selectedRole === "ADMIN" || selectedRole === "MODERATOR" || selectedRole === "SECTION_ADMIN") {
      const addNewsBtn = document.createElement('button');
      addNewsBtn.innerText = 'Создать новость';
      addNewsBtn.setAttribute('data-i18n', 'news.create-news-button');
      addNewsBtn.classList.add('btn', 'mb-6');
      addNewsBtn.addEventListener('click', async () => {
        try {
          await core.navigate('/news/create'); // ← виртуальный маршрут
        } catch (err) {
          console.error('Failed to load create-news page:', err);
          document.getElementById('page-content').innerHTML = '<p>Ошибка загрузки страницы</p>';
        }
      });
      newsContainer.appendChild(addNewsBtn);
    }

    // Отображаем все новости
    allNews.forEach(newsItem => {
      newsContainer.appendChild(renderNewsItem(newsItem));
    });

    // Применяем переводы
    if (typeof window.applyTranslations === 'function') {
      window.applyTranslations(newsContainer);
    }

    // Показываем "больше нет", если нужно
    if (!hasMore && noMoreEl) {
      noMoreEl.style.display = 'block';
      newsContainer.appendChild(noMoreEl);
    }
  }

  const PART_SIZE = 10;

  async function loadNews() {
    if (isLoading || !hasMore) return;

    isLoading = true;
    if (loadingEl) loadingEl.style.display = 'block';

    const url = new URL(API_ROUTES.news.getPart, window.location.origin);
    if (nextDateTime) {
      url.searchParams.append('startDate', nextDateTime.toISOString());
    } else {
      url.searchParams.append('startDate', new Date().toISOString());
    }
    url.searchParams.append('partSize', String(PART_SIZE + 1));

    try {
      const response = await fetch(url, {
        method: 'GET',
        headers: { 'Accept': 'application/json' }
      });

      if (!response.ok) {
        throw new Error(`HTTP ${response.status}`);
      }

      const data = await response.json();

      if (!data?.news || !Array.isArray(data.news)) {
        hasMore = false;
        renderAllNews();
        return;
      }

      let newsToRender = data.news;
      if (newsToRender.length > PART_SIZE) {
        newsToRender = newsToRender.slice(0, PART_SIZE);
        hasMore = true;
        nextDateTime = new Date(newsToRender[PART_SIZE - 1].createdAt);
      } else {
        hasMore = false;
        if (newsToRender.length > 0) {
          nextDateTime = new Date(newsToRender[newsToRender.length - 1].createdAt);
        } else {
          nextDateTime = null;
        }
      }

      allNews = [...allNews, ...newsToRender];
      renderAllNews();
    } catch (err) {
      console.error('Ошибка загрузки новостей:', err);
    } finally {
      isLoading = false;
      if (loadingEl) loadingEl.style.display = 'none';
    }
  }

  function isNearBottom() {
    return window.innerHeight + window.scrollY >= document.body.offsetHeight - 500;
  }

  const scrollHandler = () => {
    if (isNearBottom()) {
      loadNews();
    }
  };

  window._newsScrollHandler = scrollHandler;
  window.addEventListener('scroll', scrollHandler);

  const onLanguageChange = () => {
    renderAllNews();
  };
  window.addEventListener('languageChanged', onLanguageChange);
  window._newsLanguageHandler = onLanguageChange;

  // Отображаем UI немедленно (кнопка появится сразу)
  renderAllNews();

  // Загружаем первую порцию новостей
  loadNews();
}