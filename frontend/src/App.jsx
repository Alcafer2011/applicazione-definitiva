import { useState, useEffect } from 'react';
import Dashboard from './components/Dashboard.jsx';

function App() {
  const [summary, setSummary] = useState(null);

  useEffect(() => {
    fetch('http://localhost:4000/api/analytics/summary')
      .then((res) => res.json())
      .then(setSummary)
      .catch(() => setSummary({ error: 'Impossibile contattare il backend' }));
  }, []);

  return (
    <div className="app-shell">
      <header className="app-header">
        <h1>WorkflowOS</h1>
        <p>Piattaforma modulare per gestire CRM, progetti, fatturazione e magazzino.</p>
      </header>
      <main>
        <Dashboard summary={summary} />
      </main>
    </div>
  );
}

export default App;
