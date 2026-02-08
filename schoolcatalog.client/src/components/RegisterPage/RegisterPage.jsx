import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import './RegisterPage.css';

const RegisterPage = () => {
  const location = useLocation();
  const navigate = useNavigate();

  const [role, setRole] = useState(null);
  const [form, setForm] = useState({
    nume: '',
    prenume: '',
    email: '',
    password: '',
    confirmPassword: '',
  });

  useEffect(() => {
    if (location.state?.role) {
      setRole(location.state.role);
    }
  }, [location.state]);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (form.password !== form.confirmPassword) {
      alert("Parolele nu coincid!");
      return;
    }

    try {
      const response = await fetch('/api/auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email: form.email,
          parola: form.password,
          rol: role,
          nume: form.nume,
          prenume: form.prenume
        }),
      });

      if (!response.ok) {
        const error = await response.json().catch(() => ({ message: 'Eroare la înregistrare' }));
        alert("Eroare: " + (error?.message || 'Nu s-a putut realiza înregistrarea'));
        return;
      }

      alert('Înregistrare reușită!');
      navigate('/login');
    } catch (err) {
      alert('Eroare server: ' + err.message);
      console.error('Register error:', err);
    }
  };

  return (
    <div className="register-page">
      <h1>Înregistrare {role ? `- ${role.charAt(0).toUpperCase() + role.slice(1)}` : ''}</h1>

      <form className="register-form" onSubmit={handleSubmit}>
        <input
          type="text"
          name="nume"
          placeholder="Nume"
          value={form.nume}
          onChange={handleChange}
          required
        />
        <input
          type="text"
          name="prenume"
          placeholder="Prenume"
          value={form.prenume}
          onChange={handleChange}
          required
        />
        <input
          type="email"
          name="email"
          placeholder="Email"
          value={form.email}
          onChange={handleChange}
          required
          autoFocus
        />
        <input
          type="password"
          name="password"
          placeholder="Parolă"
          value={form.password}
          onChange={handleChange}
          required
          minLength={6}
        />
        <input
          type="password"
          name="confirmPassword"
          placeholder="Confirmă parola"
          value={form.confirmPassword}
          onChange={handleChange}
          required
          minLength={6}
        />

        <button type="submit">Înregistrează-te</button>
      </form>

      <p>
        Ai deja cont?{' '}
        <button className="toggle-btn" onClick={() => navigate('/login')}>
          Autentifică-te
        </button>
      </p>
    </div>
  );
};

export default RegisterPage;
