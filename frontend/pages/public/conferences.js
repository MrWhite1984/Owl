// /pages/public/conferences.js

/**
 * Преобразует статус в человекочитаемый вид
 */
function formatStatus(status) {
  const map = {
    'Draft': 'Черновик',
    'Active': 'Активна',
    'Finished': 'Завершена',
    'Archived': 'Архив'
  };
  return map[status] || status;
}

/**
 * Преобразует дату в формат "10 декабря 2025"
 */
function formatDate(dateStr) {
  if (!dateStr) return '';
  const date = new Date(dateStr);
  return date.toLocaleDateString('ru', {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  });
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

  // Загружаем список конференций
  fetch('https://localhost:7077/api/Conferences', {
    method: 'GET',
    headers: { 'Accept': 'application/json' }
  })
    .then(response => {
      if (!response.ok) throw new Error(`HTTP ${response.status}`);
      return response.json();
    })
    .then(conferences => {
      if (!Array.isArray(conferences) || conferences.length === 0) {
        listEl.innerHTML = '<p data-i18n="conferences.empty">Нет доступных конференций</p>';
        return;
      }

      listEl.innerHTML = '';

      conferences.forEach(conf => {
        const card = document.createElement('div');
        card.className = 'card bg-base-100 shadow-md hover:shadow-lg transition-shadow cursor-pointer';
        card.innerHTML = `
          <div class="card-body p-4">
            <h3 class="card-title text-lg font-bold">${escapeHtml(conf.title)}</h3>
            <div class="text-sm space-y-1 mt-2">
              <div>
                <span class="font-medium" data-i18n="conferences.status">Статус:</span>
                <span class="ml-1 badge ${getStatusBadgeClass(conf.status)}">${formatStatus(conf.status)}</span>
              </div>
              <div>
                <span class="font-medium" data-i18n="conferences.created-at">Создана:</span>
                <span class="ml-1">${formatDate(conf.createdAt)}</span>
              </div>
            </div>
          </div>
        `;

        card.addEventListener('click', () => {
          // Переход на страницу конференции
          if (window.core?.loadPage) {
            window.core.loadPage(`/pages/public/conference.html?id=${conf.id}`);
          }
        });

        listEl.appendChild(card);
      });

      // Применяем переводы, если есть
      if (typeof window.applyTranslations === 'function') {
        window.applyTranslations(listEl);
      }
    })
    .catch(err => {
      console.error('Ошибка загрузки конференций:', err);
      errorEl.textContent = `Ошибка: ${err.message}`;
      errorEl.classList.remove('hidden');
    })
    .finally(() => {
      loadingEl.classList.add('hidden');
    });
}

// Вспомогательные функции
function escapeHtml(text) {
  const div = document.createElement('div');
  div.textContent = text;
  return div.innerHTML;
}

function getStatusBadgeClass(status) {
  switch (status) {
    case 'Active': return 'badge-success';
    case 'Finished': return 'badge-neutral';
    case 'Archived': return 'badge-ghost';
    case 'Draft': return 'badge-warning';
    default: return 'badge-outline';
  }
}