import { API_ROUTES } from '/js/config/api.js';

let cachedConferences = [];
let isLoading = false;

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
 * Экранирование HTML
 */
function escapeHtml(text) {
  const div = document.createElement('div');
  div.textContent = text;
  return div.innerHTML;
}

/**
 * Класс бейджа статуса
 */
function getStatusBadgeClass(isActive) {
  return isActive ? 'badge-success' : 'badge-neutral';
}

/**
 * Полная перерисовка списка конференций (включая кнопку)
 */
function renderAllConferences() {
  const listEl = document.getElementById('conferences-list');
  if (!listEl) return;

  // 🔥 Полная очистка — как в news.js
  listEl.innerHTML = '';

  const selectedRole = localStorage.getItem("selectedRole");

  // ВСЕГДА отображаем кнопку, если роль подходит
  if (selectedRole === "ADMIN") {
    const addBtn = document.createElement('button');
    addBtn.innerText = 'Создать конференцию';
    addBtn.setAttribute('data-i18n', 'conferences.create-conference-button');
    addBtn.classList.add('btn', 'mb-4');
    addBtn.addEventListener('click', () => {
      if (window.core?.navigate) {
        window.core.navigate('/conferences/create');
      }
    });
    listEl.appendChild(addBtn);
  }

  // Отображаем конференции
  if (!Array.isArray(cachedConferences) || cachedConferences.length === 0) {
    const emptyEl = document.createElement('p');
    emptyEl.setAttribute('data-i18n', 'conferences.empty');
    emptyEl.textContent = 'Нет доступных конференций';
    listEl.appendChild(emptyEl);
  } else {
    cachedConferences.forEach(conf => {
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
        window.__spa_conferenceId = conf.id;
        if (window.core?.navigate) {
          window.core.navigate('/conference');
        }
      });

      listEl.appendChild(card);
    });
  }

  // Применяем переводы
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

  // Отображаем UI немедленно (кнопка появится сразу)
  renderAllConferences();

  if (errorEl) errorEl.classList.add('hidden');
  if (loadingEl) loadingEl.classList.remove('hidden');

  if (isLoading) return;
  isLoading = true;

  fetch(API_ROUTES.conferences.getConferences, {
    method: 'GET',
    headers: { 'Accept': 'application/json' }
  })
    .then(response => {
      if (!response.ok) throw new Error(`HTTP ${response.status}`);
      return response.json();
    })
    .then(data => {
      cachedConferences = data.conferenceItems || [];
      renderAllConferences();
    })
    .catch(err => {
      console.error('Ошибка загрузки конференций:', err);
      if (errorEl) {
        errorEl.textContent = `Ошибка: ${err.message}`;
        errorEl.classList.remove('hidden');
      }
    })
    .finally(() => {
      isLoading = false;
      if (loadingEl) loadingEl.classList.add('hidden');
    });

  // Обновлять при смене языка
  window.addEventListener('languageChanged', renderAllConferences);
}