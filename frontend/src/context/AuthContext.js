import React, { createContext, useContext, useState, useEffect } from 'react';

const AuthContext = createContext();

export function useAuth() {
  return useContext(AuthContext);
}

export function AuthProvider({ children }) {
    const [currentUser, setCurrentUser] = useState(null);
    const [loading, setLoading] = useState(true);
    useEffect(() => {
        const savedUser = localStorage.getItem('user');
        if (savedUser) {
            setCurrentUser(JSON.parse(savedUser));
    }
    setLoading(false);
    }, []);

    const login = (email, password) => {
        return new Promise((resolve, reject) => {
        if (email && password) {
            const mockUser = { email, id: Date.now() };
            setCurrentUser(mockUser);
            localStorage.setItem('user', JSON.stringify(mockUser));
            resolve(mockUser);
        } 
        else {
            reject(new Error("Неверный email или пароль"));
        }
    });
  };

  const logout = () => {
    setCurrentUser(null);
    localStorage.removeItem('user');
  };

  const value = {
    currentUser,
    login,
    logout
  };

  return (
    <AuthContext.Provider value={value}>
      {!loading && children}
    </AuthContext.Provider>
  );
}
