// frontend/js/core.js

// Сохраняем оригинальный body.className и innerHTML при старте
let originalBodyClassName = '';
let originalBodyContent = '';

const core = {
  routeMap: {
    '/': { fragment: '/pages/public/news.html', pageName: 'news' },
    '/news': { fragment: '/pages/public/news.html', pageName: 'news' },
    '/news/create': { fragment: '/pages/news/create-news.html', pageName: 'create_news' },
    '/conferences': { fragment: '/pages/public/conferences-list.html', pageName: 'conferences_page' },
    '/conferences/create': { fragment: '', pageName: 'create_conference_page' },
    '/conference': { fragment: '/pages/public/conference.html', pageName: 'conference' },
    '/login': { fragment: '/pages/auth/login.html', pageName: 'login' },
    '/register': { fragment: '/pages/auth/register.html', pageName: 'register' },
  },

  pageInitializers: {
    news: async () => {
      const module = await import('../pages/public/news.js');
      module.init?.();
    },
    create_news: async () => {
      const module = await import('../pages/news/create-news.js');
      module.init?.();
    },
    conferences_page: async () => {
      const module = await import('../pages/public/conferences.js');
      module.init?.();
    },
    conference: async () => {
      const module = await import('../pages/public/conference.js');
      module.init?.();
    },
    login: async () => {
      const module = await import('../pages/auth/login.js');
      module.init?.();
    },
    register: async () => {
      const module = await import('../pages/auth/register.js');
      module.init?.();
    }
  },

  async loadPage(url) {
    history.pushState({ url }, '', url);

    const isAuthPage = url === '/login' || url === '/register';

    if (isAuthPage) {
      // Сохраняем оригинальное состояние при первом уходе
      if (!originalBodyClassName) {
        originalBodyClassName = document.body.className;
        originalBodyContent = document.getElementById('page-content')?.innerHTML || '';
      }

      try {
        const res = await fetch(this.routeMap[url].fragment);
        if (!res.ok) throw new Error('Не удалось загрузить страницу');
        const html = await res.text();
        document.body.innerHTML = html;
        document.body.classList.remove('flex', 'flex-col'); // на всякий случай

        const initFn = this.pageInitializers[this.routeMap[url].pageName];
        if (typeof initFn === 'function') {
          await initFn();
        }

        if (window.applyTranslations) {
          window.applyTranslations(document.body);
        }
      } catch (err) {
        document.body.innerHTML = `<div class="min-h-screen flex items-center justify-center text-red-500 p-4">${err.message}</div>`;
      }
    } else {
      // Восстанавливаем SPA-режим
      document.body.className = originalBodyClassName || 'min-h-screen flex flex-col';

      if (!document.getElementById('page-content')) {
        location.reload(); // fallback
        return;
      }

      let route = this.routeMap[url];
      if (!route && url.startsWith('/conference/')) {
        route = { fragment: '/pages/public/conference.html', pageName: 'conference' };
      }

      if (!route) {
        document.getElementById('page-content').innerHTML = '<div class="alert alert-error">Страница не найдена</div>';
        return;
      }

      try {
        const res = await fetch(route.fragment);
        if (!res.ok) throw new Error('Не удалось загрузить страницу');
        document.getElementById('page-content').innerHTML = await res.text();

        const initFn = this.pageInitializers[route.pageName];
        if (typeof initFn === 'function') {
          await initFn();
        }

        if (window.applyTranslations) {
          window.applyTranslations(document.getElementById('page-content'));
        }
      } catch (err) {
        document.getElementById('page-content').innerHTML = `<div class="alert alert-error">${err.message}</div>`;
      }
    }
  },

  navigate(url) {
    this.loadPage(url);
  },

  setupLinkInterception() {
    document.addEventListener('click', (e) => {
      const link = e.target.closest('a[href]');
      if (!link) return;

      const href = link.getAttribute('href');

      if (
        href.startsWith('http') &&
        !href.startsWith(window.location.origin)
      ) return;

      if (link.hasAttribute('target') || link.hasAttribute('download')) return;
      if (href.startsWith('#')) return;

      e.preventDefault();
      this.loadPage(href);
    });
  },

  handlePopState(event) {
    const url = window.location.pathname + window.location.search;
    this.loadPage(url);
  }
};

// Сохраняем исходное состояние
document.addEventListener('DOMContentLoaded', () => {
  originalBodyClassName = document.body.className;
  const url = window.location.pathname + window.location.search;
  core.loadPage(url);
});

window.core = core;
core.setupLinkInterception();
window.addEventListener('popstate', core.handlePopState.bind(core));