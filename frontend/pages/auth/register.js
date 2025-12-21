import { API_ROUTES } from '/js/config/api.js';
document.getElementById('registrationForm').addEventListener('submit', async (e) => {
    e.preventDefault();

    const formData = {
        surname: document.getElementById('surname').value.trim(),
        name: document.getElementById('name').value.trim(),
        patronymic: document.getElementById('patronymic').value.trim(),
        educationalInstitution: document.getElementById('educationalInstitution').value.trim(),
        jobTitle: document.getElementById('jobTitle').value.trim(),
        city: document.getElementById('city').value.trim(),
        phone: document.getElementById('phone').value.trim(),
        email: document.getElementById('email').value.trim(),
        isVerified: false,
        elibraryProfileUrl: document.getElementById('elibraryProfileUrl').value.trim() || null,
        password: document.getElementById('password').value
    };

    const errorEl = document.getElementById('error');
    errorEl.textContent = '';

    try {
        const res = await fetch(API_ROUTES.auth.registration, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(formData)
        });

        if (!res.ok) {
            const errorData = await res.json().catch(() => ({}));
            throw new Error(errorData?.message || 'Ошибка при регистрации');
        }

        const { token, role } = await res.json();
        localStorage.setItem('authToken', token);
        localStorage.setItem('roles', JSON.stringify([role]));
        localStorage.setItem('selectedRole', role);

        window.location.href = '/news';
    } catch (err) {
        errorEl.textContent = err.message || 'Не удалось зарегистрироваться';
    }
});