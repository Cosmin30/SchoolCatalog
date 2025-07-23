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
          {!user && (
            <li><Link to="/login">Autentificare</Link></li>
          )}
          {user && (
            <>
              <li>Bun venit, {user.email}</li>
              <li><button onClick={handleLogout} className="logout-btn">Ieșire</button></li>
            </>
          )}
        </ul>
      </nav>
    </header>
  );
};

export default Header;
