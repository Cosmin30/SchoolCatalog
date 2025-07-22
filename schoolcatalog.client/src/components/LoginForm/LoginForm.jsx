import React, { useState } from 'react';
import './LoginForm.css';

const LoginForm = ({ role, onLogin }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch('https://localhost:7286/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email,
          parola: password,
          rol: role
        }),
      });

      if (!response.ok) {
        const errorData = await response.json();
        alert('Eroare autentificare: ' + (errorData || 'Date incorecte'));
        return;
      }

      const data = await response.json();
      localStorage.setItem('token', data.token);
      localStorage.setItem('user', JSON.stringify(data.user));

      alert(`Bun venit, ${data.user.email}!`);
      if (onLogin) onLogin(role);
    } catch (error) {
      alert('Eroare server: ' + error.message);
    }
  };

  return (
    <form className="login-form" onSubmit={handleSubmit}>
      <h2>Autentificare {role}</h2>
      <input
        type="email"
        placeholder="Email"
        value={email}
        required
        onChange={e => setEmail(e.target.value)}
        autoFocus
      />
      <input
        type="password"
        placeholder="Parolă"
        value={password}
        required
        onChange={e => setPassword(e.target.value)}
      />
      <button type="submit">Autentificare</button>
    </form>
  );
};

export default LoginForm;
