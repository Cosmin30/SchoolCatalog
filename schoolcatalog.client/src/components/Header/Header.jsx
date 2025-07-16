import { Link } from "react-router-dom";
import './Header.css';

const Header=()=>{
    return (
        <header className="app-header">
        <div className="logo">
            <Link to="/">Catalog Scolar</Link>
        </div>
        <nav>
            <ul className="nav-list">
                <li><Link to='/'>Acasa</Link></li>
                <li><Link to='/login?role=profesor'>Profesor</Link></li>
                <li><Link to='/login?role=elev'>Elev</Link></li>
                <li><Link to='/despre'>Despre noi</Link></li>
                <li><Link to="/contact">Contact</Link></li>
            </ul>
        </nav>
        </header>
    );
};
export default Header;