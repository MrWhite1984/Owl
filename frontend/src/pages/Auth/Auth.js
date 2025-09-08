import React, { useState } from "react";
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';

export default function Auth(){
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);

    const { login, currentUser } = useAuth();
    const navigate = useNavigate();

    React.useEffect(() => {
        if (currentUser) {
            navigate("/Home", { replace: true });
        }
    }, [currentUser, navigate]);

    async function handleSubmit(e) {
        e.preventDefault();

        if (!email || !password) {
            setError("Заполните все поля");
            return;
        }

        try {
            setError("");
            setLoading(true);
            await login(email, password);
            navigate("/Home");
        } catch {
            setError("Не удалось войти");
        }
    setLoading(false);
  }

    return (<div>
      <h2>Вход</h2>
      {error && <div style={{ color: "red" }}>{error}</div>}
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="email">Email</label>
          <input
            type="text"
            placeholder="Email"
            name="email"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div>
          <label htmlFor="password">Пароль</label>
          <input
            type="password"
            placeholder="Пароль"
            name="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <button id="submitBtn" type="submit" disabled={loading}>
          {loading ? "Вход..." : "Отправить"}
        </button>
      </form>
      <a href="">Восстановить пароль</a>
      <a href="">Регистрация</a>
    </div>
  );
}