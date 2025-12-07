let currentLang = localStorage.getItem('lang') ||
  (navigator.language.startsWith('ru') ? 'ru' : 'en');

const translations = {};

async function loadTranslations() {
  try {
    const res = await fetch(`/locales/${currentLang}.json`);
    translations[currentLang] = await res.json();
  } catch (e) {
    console.error('Failed to load translations');
  }
}

function t(key) {
  return translations[currentLang]?.[key] || key;
}

function applyTranslations() {
  document.querySelectorAll('[data-i18n]').forEach(el => {
    const key = el.getAttribute('data-i18n');
    if (key) el.textContent = t(key);
  });

  document.querySelectorAll('[data-i18n-ph]').forEach(el => {
    const key = el.getAttribute('data-i18n-ph');
    if (key) el.placeholder = t(key);
  });
}

window.t = t;
window.switchLanguage = (lang) => {
  localStorage.setItem('lang', lang);
  currentLang = lang;
  loadTranslations().then(applyTranslations);
  if (document.getElementById('current-lang')) {
    document.getElementById('current-lang').textContent = lang.toUpperCase();
  }
};

// ✅ Выполняем переводы ТОЛЬКО после полной загрузки DOM
document.addEventListener('DOMContentLoaded', () => {
  loadTranslations().then(applyTranslations);
});