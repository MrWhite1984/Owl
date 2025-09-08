import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Home from './pages/Home';
import About from './pages/About';
import Auth from './pages/Auth/Auth';
import { AuthProvider, useAuth } from './context/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';

function Navbar() {
  const context = useAuth();

  if (!context) {
    return (
      <nav>
        <ul>
          <li><Link to="/">Вход</Link></li>
        </ul>
      </nav>
    );
  }

  const { currentUser, logout } = context;

  const handleLogout = () => {
    logout();
  };

  return (
    <nav>
      <ul style={{ listStyle: 'none', display: 'flex', gap: '20px', padding: 0 }}>
        <li><Link to="/">Вход</Link></li>

        {currentUser && (
          <>
            <li><Link to="/Home">Главная</Link></li>
            <li><Link to="/About">О себе</Link></li>
            <li>
              <button
                onClick={handleLogout}
                style={{
                  background: 'none',
                  border: 'none',
                  padding: 0,
                  font: 'inherit',
                  cursor: 'pointer',
                  color: '#007bff',
                  textDecoration: 'underline'
                }}
              >
                Выйти ({currentUser.email})
              </button>
            </li>
          </>
        )}
      </ul>
    </nav>
  );
}

function App() {
  return (
    <Router>
      <AuthProvider>
        <div style={{ padding: '20px' }}>
          <Navbar />

          <hr />

          <Routes>
            <Route path="/" element={<Auth />} />
            <Route
              path="/Home"
              element={
                <ProtectedRoute>
                  <Home />
                </ProtectedRoute>
              }
            />
            <Route
              path="/About"
              element={
                <ProtectedRoute>
                  <About />
                </ProtectedRoute>
              }
            />
          </Routes>
        </div>
      </AuthProvider>
    </Router>
  );
}

export default App;