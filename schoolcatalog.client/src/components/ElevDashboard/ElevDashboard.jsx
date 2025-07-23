import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './ElevDashboard.css';

const ElevDashboard = () => {
  const navigate = useNavigate();

  const [user, setUser] = useState(null);
  const [materii, setMaterii] = useState([]);
  const [note, setNote] = useState([]);
  const [clasa, setClasa] = useState('');
  const [tema, setTema] = useState('');
  const [fisierTema, setFisierTema] = useState('');
  const [loading, setLoading] = useState(true);

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

    fetch('https://localhost:7286/api/elev', {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then(res => {
        if (!res.ok) throw new Error('Eroare la încărcarea datelor elevului');
        return res.json();
      })
      .then(data => {
        setMaterii(data.materii || []);
        setNote(data.note || []);
        setClasa(data.clasa || '');
        setTema(data.tema || '');
        setFisierTema(data.fisierTema || '');
      })
      .catch(err => {
        alert(err.message);
      })
      .finally(() => {
        setLoading(false);
      });
  }, [navigate]);

  if (loading) return <p>Se încarcă datele...</p>;
  if (!user) return null;

  return (
    <div className="elev-dashboard">
      <h2>Bine ai venit, {user.email}</h2>
      <p>Rol: {user.rol}</p>

      <section>
        <h3>Clasa</h3>
        <p>{clasa || 'Nu există informații despre clasă'}</p>
      </section>

      <section>
        <h3>Materii</h3>
        {materii.length > 0 ? (
          <ul>
            {materii.map((m, i) => (
              <li key={i}>{m}</li>
            ))}
          </ul>
        ) : (
          <p>Nu există materii disponibile.</p>
        )}
      </section>

      <section>
        <h3>Note</h3>
        {note.length > 0 ? (
          <ul>
            {note.map((n, i) => (
              <li key={i}>Nota: {n}</li>
            ))}
          </ul>
        ) : (
          <p>Nu există note disponibile.</p>
        )}
      </section>

      <section>
        <h3>Tema</h3>
        <p>{tema || 'Nu există teme disponibile.'}</p>
        {fisierTema && (
          <a href={`/teme/${fisierTema}`} download>
            Descarcă fișierul temei
          </a>
        )}
      </section>
    </div>
  );
};

export default ElevDashboard;
