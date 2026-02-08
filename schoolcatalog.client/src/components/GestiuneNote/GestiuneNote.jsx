import React, { useState, useEffect } from 'react';
import { API_ENDPOINTS, getAuthHeaders } from '../../config/api';
import './GestiuneNote.css';

const GestiuneNote = () => {
  const [note, setNote] = useState([]);
  const [elevi, setElevi] = useState([]);
  const [materii, setMaterii] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showModal, setShowModal] = useState(false);
  const [editMode, setEditMode] = useState(false);
  const [currentNota, setCurrentNota] = useState({
    idNota: 0,
    valoare: '',
    dataNotei: new Date().toISOString().split('T')[0],
    idElev: '',
    idMaterie: ''
  });

  const [filters, setFilters] = useState({
    idElev: '',
    idMaterie: ''
  });

  useEffect(() => {
    fetchData();
  }, []);

  useEffect(() => {
    applyFilters();
  }, [filters]);

  const fetchData = async () => {
    try {
      setLoading(true);
      const [noteRes, eleviRes, materiiRes] = await Promise.all([
        fetch(API_ENDPOINTS.NOTE, { headers: getAuthHeaders() }),
        fetch(API_ENDPOINTS.ELEVI, { headers: getAuthHeaders() }),
        fetch(API_ENDPOINTS.MATERII, { headers: getAuthHeaders() })
      ]);

      if (!noteRes.ok || !eleviRes.ok || !materiiRes.ok) {
        throw new Error('Eroare la încărcarea datelor');
      }

      const [noteData, eleviData, materiiData] = await Promise.all([
        noteRes.json(),
        eleviRes.json(),
        materiiRes.json()
      ]);

      setNote(noteData);
      setElevi(eleviData);
      setMaterii(materiiData);
      setError(null);
    } catch (err) {
      setError(err.message);
      console.error('Error fetching data:', err);
    } finally {
      setLoading(false);
    }
  };

  const applyFilters = async () => {
    if (!filters.idElev && !filters.idMaterie) {
      fetchData();
      return;
    }

    try {
      setLoading(true);
      let url = API_ENDPOINTS.NOTE;

      if (filters.idElev && filters.idMaterie) {
        url = API_ENDPOINTS.NOTE_BY_ELEV_MATERIE(filters.idElev, filters.idMaterie);
      } else if (filters.idElev) {
        url = API_ENDPOINTS.NOTE_BY_ELEV(filters.idElev);
      } else if (filters.idMaterie) {
        url = API_ENDPOINTS.NOTE_BY_MATERIE(filters.idMaterie);
      }

      const response = await fetch(url, { headers: getAuthHeaders() });
      if (!response.ok) throw new Error('Eroare la filtrare');
      
      const data = await response.json();
      setNote(data);
      setError(null);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenModal = (nota = null) => {
    if (nota) {
      setEditMode(true);
      setCurrentNota({
        idNota: nota.idNota,
        valoare: nota.valoare,
        dataNotei: nota.dataNotei.split('T')[0],
        idElev: nota.idElev,
        idMaterie: nota.idMaterie
      });
    } else {
      setEditMode(false);
      setCurrentNota({
        idNota: 0,
        valoare: '',
        dataNotei: new Date().toISOString().split('T')[0],
        idElev: '',
        idMaterie: ''
      });
    }
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setCurrentNota({
      idNota: 0,
      valoare: '',
      dataNotei: new Date().toISOString().split('T')[0],
      idElev: '',
      idMaterie: ''
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const url = editMode
        ? API_ENDPOINTS.NOTE_BY_ID(currentNota.idNota)
        : API_ENDPOINTS.NOTE;

      const method = editMode ? 'PUT' : 'POST';

      const body = editMode
        ? {
            valoare: parseInt(currentNota.valoare),
            dataNotei: currentNota.dataNotei,
            esteAnulata: false,
            idElev: parseInt(currentNota.idElev),
            idMaterie: parseInt(currentNota.idMaterie)
          }
        : {
            valoare: parseInt(currentNota.valoare),
            dataNotei: currentNota.dataNotei,
            idElev: parseInt(currentNota.idElev),
            idMaterie: parseInt(currentNota.idMaterie)
          };

      const response = await fetch(url, {
        method,
        headers: getAuthHeaders(),
        body: JSON.stringify(body)
      });

      if (!response.ok) {
        const errorData = await response.text();
        throw new Error(errorData || 'Eroare la salvarea notei');
      }

      alert(editMode ? 'Nota a fost actualizată!' : 'Nota a fost adăugată!');
      handleCloseModal();
      fetchData();
    } catch (err) {
      alert('Eroare: ' + err.message);
      console.error('Error:', err);
    }
  };

  const handleDelete = async (id) => {
    if (!confirm('Sigur doriți să ștergeți această notă?')) return;

    try {
      const response = await fetch(API_ENDPOINTS.NOTE_BY_ID(id), {
        method: 'DELETE',
        headers: getAuthHeaders()
      });

      if (!response.ok) throw new Error('Eroare la ștergerea notei');

      alert('Nota a fost ștearsă!');
      fetchData();
    } catch (err) {
      alert('Eroare: ' + err.message);
    }
  };

  const handleToggleAnulata = async (id) => {
    try {
      const response = await fetch(API_ENDPOINTS.TOGGLE_NOTA_ANULATA(id), {
        method: 'PATCH',
        headers: getAuthHeaders()
      });

      if (!response.ok) throw new Error('Eroare la schimbarea stării notei');

      alert('Starea notei a fost schimbată!');
      fetchData();
    } catch (err) {
      alert('Eroare: ' + err.message);
    }
  };

  if (loading) return <div className="loading">Se încarcă notele...</div>;
  if (error) return <div className="error">Eroare: {error}</div>;

  return (
    <div className="gestiune-note">
      <div className="header">
        <h1>Gestiune Note</h1>
        <button className="btn-add" onClick={() => handleOpenModal()}>
          + Adaugă Notă
        </button>
      </div>

      {/* Filters */}
      <div className="filters">
        <div className="filter-group">
          <label>Filtrează după elev:</label>
          <select
            value={filters.idElev}
            onChange={(e) => setFilters({ ...filters, idElev: e.target.value })}
          >
            <option value="">Toți elevii</option>
            {elevi.map((elev) => (
              <option key={elev.idElev} value={elev.idElev}>
                {elev.numeElev} {elev.prenumeElev}
              </option>
            ))}
          </select>
        </div>

        <div className="filter-group">
          <label>Filtrează după materie:</label>
          <select
            value={filters.idMaterie}
            onChange={(e) => setFilters({ ...filters, idMaterie: e.target.value })}
          >
            <option value="">Toate materiile</option>
            {materii.map((materie) => (
              <option key={materie.idMaterie} value={materie.idMaterie}>
                {materie.numeMaterie}
              </option>
            ))}
          </select>
        </div>

        <button
          className="btn-clear-filters"
          onClick={() => setFilters({ idElev: '', idMaterie: '' })}
        >
          Resetează filtre
        </button>
      </div>

      {/* Table */}
      <div className="table-container">
        <table className="note-table">
          <thead>
            <tr>
              <th>Elev</th>
              <th>Materie</th>
              <th>Notă</th>
              <th>Data</th>
              <th>Status</th>
              <th>Acțiuni</th>
            </tr>
          </thead>
          <tbody>
            {note.length === 0 ? (
              <tr>
                <td colSpan="6" className="no-data">
                  Nu există note înregistrate.
                </td>
              </tr>
            ) : (
              note.map((nota) => (
                <tr key={nota.idNota} className={nota.esteAnulata ? 'anulata' : ''}>
                  <td>
                    {nota.numeElev} {nota.prenumeElev}
                  </td>
                  <td>{nota.numeMaterie}</td>
                  <td className="nota-valoare">{nota.valoare}</td>
                  <td>{new Date(nota.dataNotei).toLocaleDateString('ro-RO')}</td>
                  <td>
                    <span className={`status ${nota.esteAnulata ? 'anulata' : 'activa'}`}>
                      {nota.esteAnulata ? 'Anulată' : 'Activă'}
                    </span>
                  </td>
                  <td className="actions">
                    <button
                      className="btn-edit"
                      onClick={() => handleOpenModal(nota)}
                      title="Editează"
                    >
                      ✏️
                    </button>
                    <button
                      className="btn-toggle"
                      onClick={() => handleToggleAnulata(nota.idNota)}
                      title={nota.esteAnulata ? 'Reactivează' : 'Anulează'}
                    >
                      {nota.esteAnulata ? '✓' : '✕'}
                    </button>
                    <button
                      className="btn-delete"
                      onClick={() => handleDelete(nota.idNota)}
                      title="Șterge"
                    >
                      🗑️
                    </button>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      {/* Modal */}
      {showModal && (
        <div className="modal-overlay" onClick={handleCloseModal}>
          <div className="modal-content" onClick={(e) => e.stopPropagation()}>
            <div className="modal-header">
              <h2>{editMode ? 'Editează Nota' : 'Adaugă Notă Nouă'}</h2>
              <button className="btn-close" onClick={handleCloseModal}>
                ✕
              </button>
            </div>

            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label>Elev *</label>
                <select
                  value={currentNota.idElev}
                  onChange={(e) =>
                    setCurrentNota({ ...currentNota, idElev: e.target.value })
                  }
                  required
                >
                  <option value="">Selectează elevul</option>
                  {elevi.map((elev) => (
                    <option key={elev.idElev} value={elev.idElev}>
                      {elev.numeElev} {elev.prenumeElev}
                      {elev.numeClasa && ` - Clasa ${elev.numeClasa}`}
                    </option>
                  ))}
                </select>
              </div>

              <div className="form-group">
                <label>Materie *</label>
                <select
                  value={currentNota.idMaterie}
                  onChange={(e) =>
                    setCurrentNota({ ...currentNota, idMaterie: e.target.value })
                  }
                  required
                >
                  <option value="">Selectează materia</option>
                  {materii.map((materie) => (
                    <option key={materie.idMaterie} value={materie.idMaterie}>
                      {materie.numeMaterie}
                    </option>
                  ))}
                </select>
              </div>

              <div className="form-group">
                <label>Notă * (1-10)</label>
                <input
                  type="number"
                  min="1"
                  max="10"
                  value={currentNota.valoare}
                  onChange={(e) =>
                    setCurrentNota({ ...currentNota, valoare: e.target.value })
                  }
                  required
                />
              </div>

              <div className="form-group">
                <label>Data *</label>
                <input
                  type="date"
                  value={currentNota.dataNotei}
                  onChange={(e) =>
                    setCurrentNota({ ...currentNota, dataNotei: e.target.value })
                  }
                  required
                />
              </div>

              <div className="modal-actions">
                <button type="button" className="btn-cancel" onClick={handleCloseModal}>
                  Anulează
                </button>
                <button type="submit" className="btn-save">
                  {editMode ? 'Actualizează' : 'Adaugă'}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default GestiuneNote;
