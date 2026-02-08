
const API_BASE_URL = '/api';

export const API_ENDPOINTS = {
  // Authentication
  LOGIN: `${API_BASE_URL}/auth/login`,
  REGISTER: `${API_BASE_URL}/auth/register`,
  
  // Teme
  TEME: `${API_BASE_URL}/tema`,
  TEME_BY_ID: (id) => `${API_BASE_URL}/tema/${id}`,
  TEME_BY_CLASA: (idClasa) => `${API_BASE_URL}/tema/Clasa/${idClasa}`,
  TEME_BY_MATERIE: (idMaterie) => `${API_BASE_URL}/tema/Materie/${idMaterie}`,
  
  // Elevi
  ELEVI: `${API_BASE_URL}/elev`,
  ELEVI_BY_ID: (id) => `${API_BASE_URL}/elev/${id}`,
  
  // Profesori
  PROFESORI: `${API_BASE_URL}/profesor`,
  PROFESORI_BY_ID: (id) => `${API_BASE_URL}/profesor/${id}`,
  
  // Clase
  CLASE: `${API_BASE_URL}/clasa`,
  CLASE_BY_ID: (id) => `${API_BASE_URL}/clasa/${id}`,
  
  // Materii
  MATERII: `${API_BASE_URL}/materie`,
  MATERII_BY_ID: (id) => `${API_BASE_URL}/materie/${id}`,
  
  // Note
  NOTE: `${API_BASE_URL}/nota`,
  NOTE_BY_ID: (id) => `${API_BASE_URL}/nota/${id}`,
  NOTE_BY_ELEV: (idElev) => `${API_BASE_URL}/nota/elev/${idElev}`,
  NOTE_BY_MATERIE: (idMaterie) => `${API_BASE_URL}/nota/materie/${idMaterie}`,
  NOTE_BY_ELEV_MATERIE: (idElev, idMaterie) => `${API_BASE_URL}/nota/elev/${idElev}/materie/${idMaterie}`,
  TOGGLE_NOTA_ANULATA: (id) => `${API_BASE_URL}/nota/${id}/toggle-anulata`,
  
  // Medii
  MEDII: `${API_BASE_URL}/media`,
  MEDII_BY_ID: (id) => `${API_BASE_URL}/media/${id}`,
  
  // Orar
  ORARE: `${API_BASE_URL}/orar`,
  ORARE_BY_ID: (id) => `${API_BASE_URL}/orar/${id}`,
  
  // Orar Items
  ORAR_ITEMS: `${API_BASE_URL}/oraritem`,
  ORAR_ITEMS_BY_ID: (id) => `${API_BASE_URL}/oraritem/${id}`,
  
  // Fisiere Tema
  FISIERE_TEMA: `${API_BASE_URL}/fisiertema`,
  FISIERE_TEMA_BY_ID: (id) => `${API_BASE_URL}/fisiertema/${id}`,
};

// Helper function pentru a construi headers cu authentication token
export const getAuthHeaders = () => {
  const token = localStorage.getItem('token');
  return {
    'Content-Type': 'application/json',
    ...(token && { 'Authorization': `Bearer ${token}` })
  };
};

// Helper function pentru fetch cu error handling
export const apiFetch = async (url, options = {}) => {
  try {
    const response = await fetch(url, {
      ...options,
      headers: {
        ...getAuthHeaders(),
        ...options.headers
      }
    });
    
    if (!response.ok) {
      const error = await response.json().catch(() => ({ message: 'Eroare server' }));
      throw new Error(error.message || `HTTP Error: ${response.status}`);
    }
    
    return await response.json();
  } catch (error) {
    console.error('API Error:', error);
    throw error;
  }
};

export default API_ENDPOINTS;
