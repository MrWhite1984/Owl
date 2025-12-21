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

        localStorage.setItem('authToken', token);

        let userRoles = [];
        if (Array.isArray(roles)) {
            userRoles = roles;
        } else if (typeof role === 'string') {
            userRoles = [role];
        } else {
            userRoles = [];
        }

        localStorage.setItem('roles', JSON.stringify(userRoles));

        if (userRoles.length === 1) {
            localStorage.setItem('selectedRole', userRoles[0]);
            window.location.href = '/';
        }
        else {
            window.location.href = '/role-select'
        }
    } catch (err) {
        errorEl.textContent = err.message || 'Ошибка подключения';
    }
});
switchLanguage(localStorage.getItem("lang"));