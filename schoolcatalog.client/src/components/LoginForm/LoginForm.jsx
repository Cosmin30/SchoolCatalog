import React, { useState } from 'react';
import './LoginForm.css';

const LoginForm = ({ role, onLogin }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    try {
      const res = await fetch('https://localhost:7286/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email,
          parola: password,
          rol: role,
        }),
      });

      if (!res.ok) {
        const errData = await res.json();
        alert('Eroare autentificare: ' + (errData?.message || 'Date incorecte'));
        setLoading(false);
        return;
      }

      const data = await res.json();

      if (!data.user || typeof data.user !== 'object') {
        alert('Răspuns neașteptat de la server.');
        setLoading(false);
        return;
      }

      localStorage.setItem('token', data.token);
      localStorage.setItem('user', JSON.stringify(data.user));

      alert(`Bun venit, ${data.user.email}!`);

      if (onLogin) onLogin(data.user);

    } catch (error) {
      alert('Eroare server: ' + error.message);
    } finally {
      setLoading(false);
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
        onChange={(e) => setEmail(e.target.value)}
        autoFocus
      />

      <input
        type="password"
        placeholder="Parolă"
        value={password}
        required
        onChange={(e) => setPassword(e.target.value)}
      />

      <button type="submit" disabled={loading}>
        {loading ? 'Se autentifică...' : 'Autentificare'}
      </button>
    </form>
  );
};

export default LoginForm;
