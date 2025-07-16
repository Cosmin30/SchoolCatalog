import React, { useState } from 'react';
import './LoginForm.css';

const LoginForm = ({ role, onLogin }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
      e.preventDefault();
      alert(`Logare ${role} : ${email}`)
      if (onLogin) onLogin(role);
  };

    return (
        <form className="login-form" onSubmit={handleSubmit}>
            <h2>Logare {role}</h2>
            <input
                type="email"
                placeholder="Email"
                value={email}
                required
                onChange={e => setEmail(e.target.value)}
            />
            <input
                type="password"
                placeholder="Parolă"
                value={password}
                required
                onChange={e => setPassword(e.target.value)}
            />
            <button type="submit">Logare</button>
        </form>
    );
};
export default LoginForm;