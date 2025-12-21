import { API_ROUTES } from '/js/config/api.js';
document.getElementById('loginForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const errorEl = document.getElementById('error');
    errorEl.textContent = '';

    try {
        const res = await fetch(API_ROUTES.auth.login, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password })
        });

        if (!res.ok) {
            const err = await res.json().catch(() => ({}));
            throw new Error(err.message || 'Неверный email или пароль');
        }

        const { token, role, roles } = await res.json();

        // Сохраняем токен всегда
        localStorage.setItem('authToken', token);

        // Определяем итоговый массив ролей
        let userRoles = [];
        if (Array.isArray(roles)) {
            userRoles = roles;
        } else if (typeof role === 'string') {
            userRoles = [role];
        } else {
            // Никаких ролей не передано — можно обработать как ошибку или оставить пустым
            userRoles = [];
        }

        localStorage.setItem('roles', JSON.stringify(userRoles));

        // Если одна роль — сразу выбираем её
        if (userRoles.length === 1) {
            localStorage.setItem('selectedRole', userRoles[0]);
            window.location.href = '/';
        }
        else {
            window.location.href = 'role-select.html'
        }
    } catch (err) {
        errorEl.textContent = err.message || 'Ошибка подключения';
    }
});
switchLanguage(localStorage.getItem("lang"));