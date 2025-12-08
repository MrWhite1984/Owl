const core = {
  pageInitializers: {
    news: async () => {
      const module = await import('../pages/public/news.js');
      module.init?.();
    },
    create_news: async () =>{
      const module = await import('../pages/news/create-news.js');
      module.init?.();
    }
  },

  async loadPage(url) {
    const contentEl = document.getElementById('page-content');
    try {
      const res = await fetch(url);
      if (!res.ok) throw new Error('Страница не найдена');
      contentEl.innerHTML = await res.text();

      const pageEl = contentEl.querySelector('[data-page]');
      if (pageEl) {
        const pageName = pageEl.dataset.page;
        const initFn = this.pageInitializers[pageName];
        if (typeof initFn === 'function') {
          await initFn();
        }
      }

      if (window.applyTranslations) applyTranslations();
    } catch (err) {
      contentEl.innerHTML = `<div class="alert alert-error">${err.message}</div>`;
    }
  }
};

window.core = core;