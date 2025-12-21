'use strict';

import { API_ROUTES } from '/js/config/api.js';

// Экспортируем функцию инициализации
export function init() {
  const roleLabels = {
    'ADMIN': { ru: 'Администратор', en: 'Administrator' },
    'USER': { ru: 'Участник', en: 'User' },
    'MODERATOR': { ru: 'Модератор', en: 'Moderator' },
    'SECTION_ADMIN': { ru: 'Администратор секции', en: 'Section Administrator' },
    'REVIEWER': { ru: 'Рецензент', en: 'Reviewer' }
  };

  function getDisplayRole(role, lang) {
    return roleLabels[role]?.[lang] || role;
  }

  const savedLang = localStorage.getItem('lang');
  const browserLang = navigator.language.startsWith('ru') ? 'ru' : 'en';
  const currentLang = savedLang || browserLang;

  const rolesContainer = document.getElementById('roles-container');
  const errorEl = document.getElementById('error');

  if (!rolesContainer || !errorEl) return;

  const roles = JSON.parse(localStorage.getItem('roles') || '[]');

  if (!Array.isArray(roles) || roles.length === 0) {
    errorEl.textContent = currentLang === 'ru' ? 'Нет доступных ролей.' : 'No roles available.';
  } else if (roles.length === 1) {
    changeRoleAndRedirect(roles[0]);
  } else {
    roles.forEach(role => {
      const button = document.createElement('button');
      button.className = 'btn btn-outline w-full capitalize';
      button.textContent = getDisplayRole(role, currentLang);
      button.addEventListener('click', () => changeRoleAndRedirect(role));
      rolesContainer.appendChild(button);
    });
  }

  async function changeRoleAndRedirect(role) {
    const token = localStorage.getItem('authToken');
    if (!token) {
      errorEl.textContent = currentLang === 'ru'
        ? 'Токен отсутствует. Пожалуйста, войдите снова.'
        : 'Auth token missing. Please log in again.';
      return;
    }

    try {
      const res = await fetch(API_ROUTES.auth.changeRole, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({ role })
      });

      if (!res.ok) {
        const err = await res.json().catch(() => ({}));
        throw new Error(err?.message || (currentLang === 'ru' ? 'Не удалось сменить роль' : 'Failed to change role'));
      }

      const { token: newToken, role: newRole } = await res.json();
      localStorage.setItem('authToken', newToken);
      localStorage.setItem('selectedRole', newRole);

      // ✅ SPA-навигация вместо window.location
      if (window.core?.navigate) {
        window.core.navigate('/');
      } else {
        window.location.href = '/';
      }
    } catch (err) {
      errorEl.textContent = err.message || (currentLang === 'ru' ? 'Ошибка при смене роли' : 'Error changing role');
    }
  }
}