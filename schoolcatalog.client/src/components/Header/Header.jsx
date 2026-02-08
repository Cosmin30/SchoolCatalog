import { Link, useNavigate } from "react-router-dom";
import { useAuth } from '../../context/AuthContext';
import './Header.css';

const Header = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/');  
  };

  return (
    <header className="app-header">
      <div className="logo">
        <Link to="/">Catalog Școlar</Link>
      </div>
      <nav>
        <ul className="nav-list">
          <li><Link to="/">Acasă</Link></li>
          <li><Link to="/despre">Despre noi</Link></li>
          <li><Link to="/contact">Contact</Link></li>
          
          {/* Link-uri pentru utilizatori autentificați */}
          {user && (
            <>
              {/* Link-uri specifice pentru ELEV */}
              {user.rol && user.rol.toLowerCase() === 'elev' && (
                <>
                  <li><Link to="/dashboard">Dashboard</Link></li>
                  <li><Link to="/elev/note">📚 Notele Mele</Link></li>
                </>
              )}
              
              {/* Link-uri specifice pentru PROFESOR */}
              {user.rol && user.rol.toLowerCase() === 'profesor' && (
                <>
                  <li><Link to="/profesor/note">📝 Gestiune Note</Link></li>
                </>
              )}
              
              <li className="user-info">
                👤 {user.numeElev || user.numeProfesor || user.email}
              </li>
              <li>
                <button onClick={handleLogout} className="logout-btn">
                  Deconectare
                </button>
              </li>
            </>
          )}
          
          {/* Link pentru login dacă nu e autentificat */}
          {!user && (
            <li><Link to="/login">Autentificare</Link></li>
          )}
        </ul>
      </nav>
    </header>
  );
};

export default Header;
