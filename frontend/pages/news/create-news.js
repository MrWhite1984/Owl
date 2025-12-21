// pages/news/create-news.js
import { API_ROUTES } from '/js/config/api.js';
export function init() {
  'use strict';

  const form = document.getElementById('create-news-form');
  const cancelBtn = document.getElementById('cancel-create');

  if (!form || !cancelBtn) {
    console.warn('Элементы формы создания новости не найдены');
    return;
  }

  // === Обработчик отправки ===
  form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const titleInput = document.getElementById('news-title');
    const contentTextarea = document.getElementById('news-content');

    const title = titleInput?.value.trim();
    const content = contentTextarea?.value.trim();

    if (!title || !content) return;

    const token = localStorage.getItem('authToken');
    if (!token) {
      alert('Требуется авторизация');
      return;
    }

    try {
      const response = await fetch(API_ROUTES.news.create, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({ title, content })
      });

      if (response.ok) {
        alert('Новость успешно создана!');
        // Возврат к списку новостей — через загрузку страницы
        window.core?.loadPage('/news');
      } else {
        const errorText = await response.text();
        alert('Ошибка: ' + (errorText || 'Неизвестная ошибка'));
      }
    } catch (err) {
      console.error('Ошибка сети:', err);
      alert('Не удалось подключиться к серверу');
    }
  });

  // === Обработчик отмены ===
  cancelBtn.addEventListener('click', () => {
    window.core?.loadPage('/news');
  });
}