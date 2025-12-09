// pages/public/news.js

export function init() {
  'use strict';

  const existingHandler = window._newsScrollHandler;
  if (existingHandler) {
    window.removeEventListener('scroll', existingHandler);
  }

  // === Теперь храним ВСЕ загруженные новости ===
  let allNews = [];
  let nextDateTime = null;
  let isLoading = false;
  let hasMore = true;

  const newsContainer = document.getElementById('news-container');
  const loadingEl = document.getElementById('loading');
  const noMoreEl = document.getElementById('no-more');

  if (!newsContainer) return;

  if (noMoreEl) noMoreEl.style.display = 'none';


  // Функция рендера одной новости
  function renderNewsItem(newsItem) {
    const author = newsItem.author || {};
    const isVerified = author.isVerified === true;

    const verifiedBadge = isVerified
      ? '<div class="flex items-center gap-1">' +
      '<svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 text-green-500" viewBox="0 0 20 20" fill="currentColor">' +
      '<path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z" clip-rule="evenodd"/>' +
      '</svg>' +
      '</div>'
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
    const content = escapeHtml(newsItem.content || '');
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
      '<h2 class="text-xl font-semibold mb-2">' + title + '</h2>' +
      '<p>' + content + '</p>' +
      '<div class="text-sm text-gray-500 mb-3 flex items-center gap-4">' +
      '<span>' + date + '</span>' +
      '<div class="flex items-center gap-1">' +
      '<span class="font-medium">' + surname + ' ' + name + ' ' + patronymic + '</span>' +
      '<span>—</span>' +
      '<span>' + jobTitle + '</span>' +
      '</div>' +
      verifiedBadge +
      '</div>';

    return article;
  }

  // Отрисовка ВСЕХ новостей (для смены языка)
  function renderAllNews() {
    newsContainer.innerHTML = '';

    const selectedRole = localStorage.getItem("selectedRole");
    // Кнопка создания
    if (selectedRole === "ADMIN" || selectedRole === "MODERATOR" || selectedRole === "SECTION_ADMIN") {
      const addNewsBtn = document.createElement('button');
      addNewsBtn.innerText = 'Создать новость';
      addNewsBtn.setAttribute('data-i18n', 'news.create-news-button');
      addNewsBtn.classList.add('btn');
      addNewsBtn.addEventListener('click', async () => {
        try {
          await core.loadPage('../pages/news/create-news.html');
        } catch (err) {
          console.error('Failed to load page:', err);
          document.getElementById('page-content').innerHTML = '<p>Ошибка загрузки страницы</p>';
        }
      });
      newsContainer.appendChild(addNewsBtn);
    }

    // Все новости
    allNews.forEach(newsItem => {
      newsContainer.appendChild(renderNewsItem(newsItem));
    });

    if (typeof window.applyTranslations === 'function') {
      window.applyTranslations(newsContainer);
    }

    if (!hasMore && noMoreEl) {
      noMoreEl.style.display = 'block';
    }
  }

  // Добавление новой порции (для скролла)
  function appendNews(newsItems) {
    newsItems.forEach(newsItem => {
      newsContainer.appendChild(renderNewsItem(newsItem));
    });
  }

  const PART_SIZE = 10;


  async function loadNews() {
    if (isLoading || !hasMore) return;

    isLoading = true;
    if (loadingEl) loadingEl.style.display = 'block';

    const url = new URL('https://localhost:7077/api/News/get-part-news', window.location.origin);
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
        renderAllNews(); // ← Обновляем UI даже при пустом ответе
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

      // Добавляем к общему списку
      allNews = [...allNews, ...newsToRender];

      // Перерисовываем ВСЁ
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

  // Обработчик смены языка
  const onLanguageChange = () => {
    renderAllNews(); // Перерисовывает ВСЁ на новом языке
  };
  window.addEventListener('languageChanged', onLanguageChange);
  window._newsLanguageHandler = onLanguageChange;

  // Загружаем первую порцию
  loadNews();
}