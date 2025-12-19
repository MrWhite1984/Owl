const API_BASE = '/api';

export const API_ROUTES = {
  auth: {
    login: `${API_BASE}/Auth/login`,
    registration: `${API_BASE}/Auth/registration`,
    changeRole: `${API_BASE}/Auth/change-role`
  },
  persons: {
    get: `${API_BASE}/Persons`,
    update: `${API_BASE}/Persons`
  }
};