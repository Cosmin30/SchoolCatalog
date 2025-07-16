import React from 'react';
import { useNavigate } from 'react-router-dom';
import './WelcomeSection.css';
const WelcomeSection = () => {
  const navigate= useNavigate();
  const handleLogin=(role)=>{
    navigate(`/login?role=${role}`)
  }
  return (
    <section className="welcome-section">
      <h1>Catalog școlar</h1>
      <p>Bun venit în catalogul online de note  </p>
      <div className="btn-welcome"> 
      <button className='btn-profesor' onClick={()=>handleLogin('profesor')}> Loghează-te ca profesor  </button>
      <button className='btn-elev'onClick={()=> handleLogin('elev')}> Loghează-te ca elev </button>
      </div>
    </section>
  );
};

export default WelcomeSection;