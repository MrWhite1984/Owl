// /pages/public/conference.js
import { API_ROUTES } from '/js/config/api.js';
export function init() {
  'use strict';

  // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
  // ✅ Удаляем предыдущий обработчик языка (чтобы избежать дублирования)
  if (window._conferenceLanguageHandler) {
    window.removeEventListener('languageChanged', window._conferenceLanguageHandler);
  }
  // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

  const contentEl = document.getElementById('conference-content');
  const loadingEl = document.getElementById('loading');
  const errorEl = document.getElementById('error');

  if (!contentEl) return;

  // Получаем ID из временной глобальной переменной
  const conferenceId = window.__spa_conferenceId;
  delete window.__spa_conferenceId;

  if (!conferenceId) {
    showError('Не указан ID конференции');
    return;
  }

  // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
  // ✅ Переменная для хранения данных (чтобы перерисовать при смене языка)
  let currentConferenceData = null;
  // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

  loadConference(conferenceId);

  async function loadConference(id) {
    contentEl.innerHTML = '';
    errorEl.classList.add('hidden');
    loadingEl.classList.remove('hidden');

    try {
      const response = await fetch(`${API_ROUTES.conferences.getConference}/${id}`, {
        method: 'GET',
        headers: { 'Accept': 'application/json' }
      });

      if (!response.ok) {
        throw new Error(`HTTP ${response.status}`);
      }

      const data = await response.json();

      // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
      // ✅ Сохраняем данные для повторного рендера
      currentConferenceData = data;
      // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

      renderConference(data);

    } catch (err) {
      console.error('Ошибка загрузки конференции:', err);
      showError(`Ошибка: ${err.message}`);
    } finally {
      loadingEl.classList.add('hidden');
    }
  }

  function renderConference(data) {
    contentEl.innerHTML = '';

    const card = document.createElement('div');
    card.className = 'bg-base-100 shadow-md rounded-lg p-6 space-y-6';

    const title = document.createElement('h2');
    title.className = 'text-2xl font-semibold';
    title.textContent = data.title || 'Без названия';

    const statusBadge = document.createElement('span');
    statusBadge.className = `badge ${data.isActive ? 'badge-success' : 'badge-neutral'} mt-2`;
    statusBadge.textContent = data.isActive
      ? (localStorage.getItem('lang') === 'ru' ? 'Активна' : 'Active')
      : (localStorage.getItem('lang') === 'ru' ? 'Завершена' : 'Completed');

    const metaContainer = document.createElement('div');
    metaContainer.className = 'space-y-2 text-sm text-gray-600';

    const startDate = document.createElement('p');
    startDate.innerHTML = `<strong data-i18n="conference.start-date">Дата начала:</strong> ${formatDate(data.startDate)}`;

    let collectionUrlHtml = '';
    if (data.collectionUrl) {
      collectionUrlHtml = `
        <p>
          <strong data-i18n="conference.collection-url">Сборник статей:</strong>
          <a href="${escapeHtml(data.collectionUrl)}" target="_blank" class="link link-primary ml-1">${escapeHtml(data.collectionUrl)}</a>
        </p>
      `;
    }

    const maxArticles = document.createElement('p');
    maxArticles.innerHTML = `<strong data-i18n="conference.max-articles">Макс. статей на автора:</strong> ${data.maxArticlesPerAuthor || 0}`;

    const onlineDefence = document.createElement('p');
    onlineDefence.innerHTML = `<strong data-i18n="conference.online-defence">Онлайн-защита:</strong> ${data.allowOnlineDefence ? 'Да' : 'Нет'}`;

    metaContainer.append(
      startDate,
      ...collectionUrlHtml ? [document.createRange().createContextualFragment(collectionUrlHtml)] : [],
      maxArticles,
      onlineDefence
    );

    const sectionsContainer = document.createElement('div');
    sectionsContainer.className = 'mt-8';

    if (data.sectionItems && data.sectionItems.length > 0) {
      const sectionsTitle = document.createElement('h3');
      sectionsTitle.className = 'text-xl font-medium mb-4';
      sectionsTitle.textContent = localStorage.getItem('lang') === 'ru' ? 'Секции' : 'Sections';
      sectionsContainer.appendChild(sectionsTitle);

      const sectionsList = document.createElement('div');
      sectionsList.className = 'space-y-6';

      data.sectionItems.forEach(section => {
        const sectionCard = document.createElement('div');
        sectionCard.className = 'border-l-4 border-primary pl-4';

        const sectionTitle = document.createElement('h4');
        sectionTitle.className = 'text-lg font-semibold';
        sectionTitle.textContent = section.title || 'Без названия';

        if (section.projectsItems && section.projectsItems.length > 0) {
          const projectsList = document.createElement('div');
          projectsList.className = 'mt-3 space-y-3 ml-2';

          section.projectsItems.forEach(project => {
            const projectCard = document.createElement('div');
            projectCard.className = 'bg-base-200 p-3 rounded';

            const projectTitle = document.createElement('h5');
            projectTitle.className = 'font-medium';
            projectTitle.textContent = project.title || 'Без названия';

            const participantsList = document.createElement('div');
            participantsList.className = 'mt-2 text-sm';

            if (project.projectParticipiants && project.projectParticipiants.length > 0) {
              const participantsTitle = document.createElement('p');
              participantsTitle.className = 'font-medium mb-1';
              participantsTitle.textContent = localStorage.getItem('lang') === 'ru' ? 'Участники:' : 'Participants:';
              participantsList.appendChild(participantsTitle);

              const participants = document.createElement('ul');
              participants.className = 'list-disc list-inside space-y-1';

              project.projectParticipiants.forEach(participant => {
                const person = participant.personShortItem;
                const fullName = [
                  person.surname,
                  person.name.charAt(0) + '.',
                  person.patronymic?.charAt(0) + '.' // ✅ защищаемся от null/undefined
                ].filter(Boolean).join(' ') || '—';

                const role = participant.isScientificSupervisor
                  ? (localStorage.getItem('lang') === 'ru' ? ' (научный руководитель)' : ' (supervisor)')
                  : '';

                const li = document.createElement('li');
                li.textContent = `${fullName}, ${person.jobTitle || '—'}${role}`;
                participants.appendChild(li);
              });

              participantsList.appendChild(participants);
            } else {
              const noParticipants = document.createElement('p');
              noParticipants.className = 'text-gray-500 italic';
              noParticipants.textContent = localStorage.getItem('lang') === 'ru' ? 'Нет участников' : 'No participants';
              participantsList.appendChild(noParticipants);
            }

            projectCard.append(projectTitle, participantsList);
            projectsList.appendChild(projectCard);
          });

          sectionCard.append(sectionTitle, projectsList);
        } else {
          const noProjects = document.createElement('p');
          noProjects.className = 'text-gray-500 italic mt-2 ml-2';
          noProjects.textContent = localStorage.getItem('lang') === 'ru' ? 'Нет проектов' : 'No projects';
          sectionCard.append(sectionTitle, noProjects);
        }

        sectionsList.appendChild(sectionCard);
      });

      sectionsContainer.appendChild(sectionsList);
    } else {
      const noSections = document.createElement('p');
      noSections.className = 'text-gray-500 italic mt-4';
      noSections.textContent = localStorage.getItem('lang') === 'ru' ? 'Нет секций' : 'No sections';
      sectionsContainer.appendChild(noSections);
    }

    card.append(title, statusBadge, metaContainer, sectionsContainer);
    contentEl.appendChild(card);

    if (typeof window.applyTranslations === 'function') {
      window.applyTranslations(contentEl);
    }
  }

  // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
  // ✅ Обработчик смены языка
  const onLanguageChange = () => {
    if (currentConferenceData) {
      renderConference(currentConferenceData); // Перерисовываем с новым языком
    }
  };

  window._conferenceLanguageHandler = onLanguageChange;
  window.addEventListener('languageChanged', onLanguageChange);
  // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

  function formatDate(dateStr) {
    if (!dateStr) return '';
    const date = new Date(dateStr);
    const lang = localStorage.getItem('lang') || 'ru';
    return date.toLocaleDateString(lang, {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  }

  function escapeHtml(text) {
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
  }

  function showError(message) {
    errorEl.textContent = message;
    errorEl.classList.remove('hidden');
    loadingEl.classList.add('hidden');
  }
}