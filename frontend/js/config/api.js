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
  },
  news: {
    getPart: `${API_BASE}/News/get-part-news`,
    create: `${API_BASE}/News/create-news`
  },
  conferences: {
    getConferences: `${API_BASE}/Conferences/get-conferences-list`,
    getConference: `${API_BASE}/Conferences/get-full-conference-data/`
  }
};