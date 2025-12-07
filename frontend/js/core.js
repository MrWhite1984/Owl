const core = {
  async loadPage(url) {
    const contentEl = document.getElementById('page-content');
    try {
      const res = await fetch(url);
      if (!res.ok) throw new Error('Страница не найдена');
      contentEl.innerHTML = await res.text();
      // Применить переводы, тему, активные кнопки
      window.t && window.applyTranslations && applyTranslations();
    } catch (err) {
      contentEl.innerHTML = `<div class="alert alert-error">${err.message}</div>`;
    }
  }
};

window.core = core;