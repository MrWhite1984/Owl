function getMenuItems(role) {
  const items = [];

  items.push({ textKey: 'menu.news', href: '/pages/public/news.html' });
  items.push({ textKey: 'menu.conferences-list', href: '/pages/public/conferences-list.html' });

  if (role) {

    if (['ADMIN', 'MODERATOR', 'SECTION_ADMIN'].includes(role)) {

    }

    if (role === 'ADMIN') {
      items.push({ textKey: 'menu.admin.users', href: '/pages/admin/users.html' });
    }

  } else {
  }

  return items;
}

function renderMenu(role) {
  const menuEl = document.getElementById('sidebar-menu');
  if (!menuEl) return;

  const items = getMenuItems(role);
  menuEl.innerHTML = '';

  items.forEach(item => {
    const btn = document.createElement('button');
    btn.className = 'btn btn-ghost w-full justify-start px-4 py-3 text-left';
    btn.dataset.i18n = item.textKey;
    btn.textContent = item.textKey;

    if (item.action === 'logout') {
      btn.addEventListener('click', handleLogout);
    } else {
      btn.addEventListener('click', () => {
        closeSidebar();
        if (window.core?.loadPage) {
          window.core.loadPage(item.href);
        }
      });
    }

    menuEl.appendChild(btn);
  });

  if (typeof window.applyTranslations === 'function') {
    window.applyTranslations(menuEl);
  }
}

export function openSidebar() {
  document.getElementById('sidebar')?.classList.remove('sidebar-closed');
  document.getElementById('sidebar')?.classList.add('sidebar-open');
  document.getElementById('sidebar-overlay')?.classList.remove('overlay-hidden');
}

export function closeSidebar() {
  document.getElementById('sidebar')?.classList.remove('sidebar-open');
  document.getElementById('sidebar')?.classList.add('sidebar-closed');
  document.getElementById('sidebar-overlay')?.classList.add('overlay-hidden');
}

export function updateSidebar(role) {
  renderMenu(role);
}

function handleLogout() {
  localStorage.removeItem('token');
  localStorage.removeItem('userRole');
  updateSidebar(null);
  closeSidebar();
  if (window.core?.loadPage) {
    window.core.loadPage('/pages/public/news.html');
  }
}

document.getElementById('open-sidebar-btn')?.addEventListener('click', openSidebar);
document.getElementById('close-sidebar-btn')?.addEventListener('click', closeSidebar);
document.getElementById('sidebar-overlay')?.addEventListener('click', closeSidebar);

window.sidebar = { openSidebar, closeSidebar, updateSidebar };