import React, { useState, useEffect } from 'react';
import { API_ENDPOINTS, getAuthHeaders } from '../../config/api';
import { useAuth } from '../../context/AuthContext';
import './VizualizareNote.css';

const VizualizareNote = () => {
  const { user } = useAuth();
  const [note, setNote] = useState([]);
  const [materii, setMaterii] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [filterMaterie, setFilterMaterie] = useState('');
  const [statistics, setStatistics] = useState({
    total: 0,
    medieGenerala: 0,
    celmaiMare: 0,
    celmaiMic: 0
  });

  useEffect(() => {
    if (user && user.idElev) {
      fetchNote();
    }
  }, [user]);

  useEffect(() => {
    calculateStatistics();
  }, [note, filterMaterie]);

  const fetchNote = async () => {
    try {
      setLoading(true);
      const response = await fetch(API_ENDPOINTS.NOTE_BY_ELEV(user.idElev), {
        headers: getAuthHeaders()
      });

      if (!response.ok) {
        throw new Error('Eroare la încărcarea notelor');
      }

      const data = await response.json();
      setNote(data);

      // Extract unique materii
      const uniqueMaterii = [...new Set(data.map((nota) => nota.numeMaterie))];
      setMaterii(uniqueMaterii);

      setError(null);
    } catch (err) {
      setError(err.message);
      console.error('Error fetching note:', err);
    } finally {
      setLoading(false);
    }
  };

  const calculateStatistics = () => {
    let filteredNote = note.filter((n) => !n.esteAnulata);

    if (filterMaterie) {
      filteredNote = filteredNote.filter((n) => n.numeMaterie === filterMaterie);
    }

    if (filteredNote.length === 0) {
      setStatistics({ total: 0, medieGenerala: 0, celmaiMare: 0, celmaiMic: 0 });
      return;
    }

    const valori = filteredNote.map((n) => n.valoare);
    const suma = valori.reduce((acc, val) => acc + val, 0);
    const medie = (suma / valori.length).toFixed(2);

    setStatistics({
      total: filteredNote.length,
      medieGenerala: medie,
      celmaiMare: Math.max(...valori),
      celmaiMic: Math.min(...valori)
    });
  };

  const getNoteByMaterie = () => {
    const noteActive = note.filter((n) => !n.esteAnulata);

    if (filterMaterie) {
      return { [filterMaterie]: noteActive.filter((n) => n.numeMaterie === filterMaterie) };
    }

    return noteActive.reduce((acc, nota) => {
      if (!acc[nota.numeMaterie]) {
        acc[nota.numeMaterie] = [];
      }
      acc[nota.numeMaterie].push(nota);
      return acc;
    }, {});
  };

  const calculateMedieMaterie = (noteMaterie) => {
    if (noteMaterie.length === 0) return '-';
    const suma = noteMaterie.reduce((acc, nota) => acc + nota.valoare, 0);
    return (suma / noteMaterie.length).toFixed(2);
  };

  const getNotaColor = (valoare) => {
    if (valoare >= 9) return 'nota-excelent';
    if (valoare >= 7) return 'nota-bine';
    if (valoare >= 5) return 'nota-suficient';
    return 'nota-insuficient';
  };

  if (loading) return <div className="loading">Se încarcă notele...</div>;
  if (error) return <div className="error">Eroare: {error}</div>;

  const noteByMaterie = getNoteByMaterie();

  return (
    <div className="vizualizare-note">
      <div className="header">
        <h1>Notele Mele</h1>
        <p className="subtitle">
          {user?.numeElev} {user?.prenumeElev}
          {user?.numeClasa && ` - Clasa ${user.numeClasa}`}
        </p>
      </div>

      {/* Statistics Cards */}
      <div className="statistics-cards">
        <div className="stat-card">
          <div className="stat-icon">📚</div>
          <div className="stat-info">
            <div className="stat-value">{statistics.total}</div>
            <div className="stat-label">Total Note</div>
          </div>
        </div>

        <div className="stat-card">
          <div className="stat-icon">📊</div>
          <div className="stat-info">
            <div className="stat-value">{statistics.medieGenerala}</div>
            <div className="stat-label">Media Generală</div>
          </div>
        </div>

        <div className="stat-card">
          <div className="stat-icon">🔝</div>
          <div className="stat-info">
            <div className="stat-value">{statistics.celmaiMare || '-'}</div>
            <div className="stat-label">Cea Mai Mare Notă</div>
          </div>
        </div>

        <div className="stat-card">
          <div className="stat-icon">📉</div>
          <div className="stat-info">
            <div className="stat-value">{statistics.celmaiMic || '-'}</div>
            <div className="stat-label">Cea Mai Mică Notă</div>
          </div>
        </div>
      </div>

      {/* Filter */}
      <div className="filter-section">
        <label>Filtrează după materie:</label>
        <select value={filterMaterie} onChange={(e) => setFilterMaterie(e.target.value)}>
          <option value="">Toate materiile</option>
          {materii.map((materie, index) => (
            <option key={index} value={materie}>
              {materie}
            </option>
          ))}
        </select>
      </div>

      {/* Note by Materie */}
      <div className="note-container">
        {Object.keys(noteByMaterie).length === 0 ? (
          <div className="no-note">
            <p>Nu ai încă note înregistrate.</p>
          </div>
        ) : (
          Object.entries(noteByMaterie).map(([materie, noteMaterie]) => (
            <div key={materie} className="materie-card">
              <div className="materie-header">
                <h3>{materie}</h3>
                <div className="medie-materie">
                  Media: <span>{calculateMedieMaterie(noteMaterie)}</span>
                </div>
              </div>

              <div className="note-list">
                {noteMaterie.length === 0 ? (
                  <p className="no-note-materie">Nu există note pentru această materie.</p>
                ) : (
                  noteMaterie.map((nota) => (
                    <div key={nota.idNota} className="nota-item">
                      <div className={`nota-badge ${getNotaColor(nota.valoare)}`}>
                        {nota.valoare}
                      </div>
                      <div className="nota-details">
                        <div className="nota-date">
                          {new Date(nota.dataNotei).toLocaleDateString('ro-RO', {
                            day: '2-digit',
                            month: 'long',
                            year: 'numeric'
                          })}
                        </div>
                      </div>
                    </div>
                  ))
                )}
              </div>
            </div>
          ))
        )}
      </div>
    </div>
  );
};

export default VizualizareNote;
