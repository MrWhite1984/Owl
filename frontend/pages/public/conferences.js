// /pages/public/conferences.js

let cachedConferences = [];

/**
 * Преобразует статус (isActive) в человекочитаемый вид
 */
function formatStatus(isActive) {
  const lang = localStorage.getItem('lang') || 'ru';
  if (lang === 'ru') {
    return isActive ? 'Активна' : 'Завершена';
  } else {
    return isActive ? 'Active' : 'Completed';
  }
}

/**
 * Преобразует дату в формат "10 декабря 2025"
 */
function formatDate(dateStr) {
  if (!dateStr) return '';
  const date = new Date(dateStr);
  const lang = localStorage.getItem('lang') || 'ru';
  return date.toLocaleDateString(lang, {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  });
}

/**
 * Рендерит список конференций из кэша
 */
function renderConferences(conferences) {
  const listEl = document.getElementById('conferences-list');
  if (!listEl) return;

  listEl.innerHTML = '';



  if (!Array.isArray(conferences) || conferences.length === 0) {
    listUl.innerHTML = '<p data-i18n="conferences.empty">Нет доступных конференций</p>';
    return;
  }

  listEl.innerHTML = '';

  const selectedRole = localStorage.getItem("selectedRole");

  // Кнопка создания
  if (selectedRole === "ADMIN") {
    const addNewsBtn = document.createElement('button');
    addNewsBtn.innerText = 'Создать конференцию';
    addNewsBtn.setAttribute('data-i18n', 'conferences.create-conference-button');
    addNewsBtn.classList.add('btn');
    addNewsBtn.addEventListener('click', async () => {
      try {
        await core.loadPage('../pages/conferences/create-conference.html');
      } catch (err) {
        console.error('Failed to load page:', err);
        document.getElementById('page-content').innerHTML = '<p>Ошибка загрузки страницы</p>';
      }
    });
    listEl.appendChild(addNewsBtn);
  }

  conferences.forEach(conf => {
    const card = document.createElement('div');
    card.className = 'card bg-base-100 shadow-md hover:shadow-lg transition-shadow cursor-pointer';
    card.innerHTML = `
      <div class="card-body p-4">
        <h3 class="card-title text-lg font-bold">${escapeHtml(conf.title)}</h3>
        <div class="text-sm space-y-1 mt-2">
          <div>
            <span class="font-medium" data-i18n="conferences.status">Статус:</span>
            <span class="ml-1 badge ${getStatusBadgeClass(conf.isActive)}">${formatStatus(conf.isActive)}</span>
          </div>
          <div>
            <span class="font-medium" data-i18n="conferences.created-at">Создана:</span>
            <span class="ml-1">${formatDate(conf.startDate)}</span>
          </div>
        </div>
      </div>
    `;

    card.addEventListener('click', () => {
      if (window.core?.loadPage) {
        // Сохраняем ID во временную глобальную переменную
        window.__spa_conferenceId = conf.id;
        // Загружаем страницу БЕЗ изменения URL
        window.core.loadPage('/pages/public/conference.html');
      }
    });

    listEl.appendChild(card);
  });

  // Применить переводы для статических надписей («Статус», «Создана»)
  if (typeof window.applyTranslations === 'function') {
    window.applyTranslations(listEl);
  }
}

export function init() {
  'use strict';

  const listEl = document.getElementById('conferences-list');
  const loadingEl = document.getElementById('loading');
  const errorEl = document.getElementById('error');

  if (!listEl) return;

  listEl.innerHTML = '';
  errorEl.classList.add('hidden');
  loadingEl.classList.remove('hidden');

  fetch('https://localhost:7077/api/Conferences/get-conferences-list', {
    method: 'GET',
    headers: { 'Accept': 'application/json' }
  })
    .then(response => {
      if (!response.ok) throw new Error(`HTTP ${response.status}`);
      return response.json();
    })
    .then(data => {
      cachedConferences = data.conferenceItems || [];
      renderConferences(cachedConferences);
    })
    .catch(err => {
      console.error('Ошибка загрузки конференций:', err);
      errorEl.textContent = `Ошибка: ${err.message}`;
      errorEl.classList.remove('hidden');
    })
    .finally(() => {
      loadingEl.classList.add('hidden');
    });

  // 🔁 Обновлять при смене языка
  window.addEventListener('languageChanged', () => {
    renderConferences(cachedConferences);
  });
}

// Вспомогательные функции
function escapeHtml(text) {
  const div = document.createElement('div');
  div.textContent = text;
  return div.innerHTML;
}

function getStatusBadgeClass(isActive) {
  return isActive ? 'badge-success' : 'badge-neutral';
}