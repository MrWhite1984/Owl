const Auth = {
    getToken() {
        return localStorage.getItem('authToken');
    },
    getRoles() {
        try {
            const token = this.getToken();
            if (!token) return [];

            // JWT: header.payload.signature → берем payload
            const payloadBase64 = token.split('.')[1];
            if (!payloadBase64) return [];

            // base64url → base64 (для atob())
            const base64 = payloadBase64.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = atob(base64);
            const payload = JSON.parse(jsonPayload);

            return Array.isArray(payload.roles) ? payload.roles : [];
        } catch (e) {
            console.warn('Failed to parse JWT roles:', e);
            return [];
        }
    },

    getSelectedRole() {
        const selected = localStorage.getItem('selectedRole');
        const roles = this.getRoles();
        return selected && roles.includes(selected) ? selected : (roles[0] || null);
    },

    isAuthenticated() {
        return !!this.getToken();
    },

    hasRole(role) {
        const roles = this.getRoles();
        const selected = this.getSelectedRole();
        return roles.includes(role) && selected === role;
    },

    logout() {
        localStorage.removeItem('authToken');
        localStorage.removeItem('selectedRole');
        // Обновляем UI (вызывается из `index.html`)
        if (typeof updateHeader === 'function') {
            updateHeader();
        }
        // Перенаправление без перезагрузки страницы
        if (typeof core !== 'undefined' && core.loadPage) {
            core.loadPage('/pages/public/news.html');
        } else {
            window.location.href = '/pages/public/news.html';
        }
    },

    loginSuccess(token) {
        localStorage.setItem('authToken', token);
        const roles = this.getRoles();
        if (roles.length === 1) {
            localStorage.setItem('selectedRole', roles[0]);
        }
        // Обновляем UI
        if (typeof updateHeader === 'function') {
            updateHeader();
        }
    },

    selectRole(role) {
        if (this.getRoles().includes(role)) {
            localStorage.setItem('selectedRole', role);
            if (typeof updateHeader === 'function') {
                updateHeader();
            }
        } else {
            console.warn('Role not available:', role);
        }
    }
};

window.Auth = Auth;