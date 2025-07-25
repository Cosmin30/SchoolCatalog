import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './ElevDashboard.css';

const ElevDashboard = () => {
  const navigate = useNavigate();

  const [user, setUser] = useState(null);
  const [materii, setMaterii] = useState([]);
  const [loading, setLoading] = useState(true);

  const dateFictive = [
    { nume: 'Matematică', note: [9, 10, 8] },
    { nume: 'Română', note: [7, 8] },
    { nume: 'Istorie', note: [10] },
    { nume: 'Informatica', note: [9, 10, 10, 9] },
  ];

  useEffect(() => {
    const storedUserStr = localStorage.getItem('user');
    const token = localStorage.getItem('token');

    if (!storedUserStr || !token) {
      alert('Trebuie să fii autentificat!');
      navigate('/login');
      return;
    }

    const storedUser = JSON.parse(storedUserStr);

    if (!storedUser.rol || storedUser.rol.toLowerCase() !== 'elev') {
      alert('Acces restricționat doar pentru elevi.');
      navigate('/login');
      return;
    }

    setUser(storedUser);

    // Simulare încărcare date
    setTimeout(() => {
      setMaterii(dateFictive);
      setLoading(false);
    }, 500);
  }, [navigate]);

  const calculeazaMediaMaterie = (note) => {
    if (!note.length) return '-';
    const suma = note.reduce((acc, n) => acc + n, 0);
    return Math.round(suma / note.length);
  };

  const calculeazaMediaGenerala = () => {
  if (!materii.length) return '-';

  const sumaMediiRotunjite = materii.reduce((acc, m) => {
    if (!m.note.length) return acc;
    const sumaNote = m.note.reduce((a, n) => a + n, 0);
    const mediaRotunjita = Math.round(sumaNote / m.note.length);
    return acc + mediaRotunjita;
  }, 0);

  const nrMateriiCuNote = materii.filter(m => m.note.length > 0).length;
  if (nrMateriiCuNote === 0) return '-';

  return (sumaMediiRotunjite / nrMateriiCuNote).toFixed(2);
};

  const handleLogout = () => {
    localStorage.clear();
    navigate('/login');
  };

  if (loading) return <p>Se încarcă datele...</p>;
  if (!user) return null;

  return (
    <div className="elev-dashboard">
      <h2>👨‍🎓 Bun venit, {user.email}</h2>
      <p><strong>Rol:</strong> {user.rol}</p>

      <section className="overview">
        <h3>📚 Materii și note</h3>
        <div className="materii-list">
          {materii.map((materie, i) => (
            <div key={i} className="materie-row">
              <span className="materie-nume">{materie.nume}</span>
              <div className="note-badge-container">
                {materie.note.map((n, j) => (
                  <span key={j} className="note-badge">{n}</span>
                ))}
              </div>
              <span className="media-materie">📈 {calculeazaMediaMaterie(materie.note)}</span>
            </div>
          ))}
        </div>
      </section>

      <section className="media-generala">
        <h3>📊 Media generală</h3>
        <p>{calculeazaMediaGenerala()}</p>
      </section>

      <button className="logout-button" onClick={handleLogout}>
        Deconectează-te
      </button>
    </div>
  );
};

export default ElevDashboard;
