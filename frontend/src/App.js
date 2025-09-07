import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Home from './pages/Home';

function App() {
  return (
    <Router>  {/* Оборачиваем всё в Router — чтобы работали ссылки */}
      <div>
        {/* Навигация */}
        <nav>
          <ul>
            <li><Link to="/">Главная</Link></li>
          </ul>
        </nav>

        <hr />

        {/* Здесь отображаются разные страницы */}
        <Routes>
          <Route path="/" element={<Home />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;