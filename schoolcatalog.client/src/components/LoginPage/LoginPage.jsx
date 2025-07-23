import React, { useState, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import LoginForm from '../LoginForm/LoginForm';
import { useAuth } from '../../context/AuthContext';  
import './LoginPage.css';

const LoginPage = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const queryParams = new URLSea

  const [role, setRole] = useState(null);

  useEffect(() => {
    const roleFromQuery = queryParams.get('role');
    if (roleFromQuery === 'elev' || roleFromQuery === 'profesor') {
      setRole(roleFromQuery);
    }
  }, [location.search]);

  const handleRoleSelect = (selectedRole) => {
    setRole(selectedRole);
    navigate(`/login?role=${selectedRole}`);
  };

  const handleLoginSuccess = (user) => {
    login(user);          
    navigate('/dashboard');  
  };

  return (
    <div className="login-page">
      <h1>Autentificare</h1>

      {!role && (
        <div className="role-selection">
          <button onClick={() => handleRoleSelect('profesor')}>Profesor</button>
          <button onClick={() => handleRoleSelect('elev')}>Elev</button>
        </div>
      )}

      {role && (
        <>
          <LoginForm role={role} onLogin={handleLoginSuccess} />
          <p>
            Nu ai cont?{' '}
            <button
              className="toggle-btn"
              onClick={() => navigate('/register', { state: { role } })}
            >
              Înregistrează-te
            </button>
          </p>
          <button className="back-button" onClick={() => {
            setRole(null);
            navigate('/login');
          }}>
            Înapoi la selecția rolului
          </button>
        </>
      )}
    </div>
  );
};

export default LoginPage;
