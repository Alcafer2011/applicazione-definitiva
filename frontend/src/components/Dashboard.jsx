function Dashboard({ summary }) {
  return (
    <section className="dashboard">
      <div className="dashboard-card">
        <h2>Panoramica</h2>
        {summary ? (
          summary.error ? (
            <p className="error">{summary.error}</p>
          ) : (
            <div className="summary-grid">
              <div>
                <strong>Progetti</strong>
                <span>{summary.projects}</span>
              </div>
              <div>
                <strong>Fatture</strong>
                <span>{summary.invoices}</span>
              </div>
              <div>
                <strong>Clienti</strong>
                <span>{summary.customers}</span>
              </div>
            </div>
          )
        ) : (
          <p>Caricamento...</p>
        )}
      </div>
      <div className="dashboard-card">
        <h2>Moduli chiave</h2>
        <ul>
          <li>CRM</li>
          <li>Project Management</li>
          <li>Fatturazione</li>
          <li>Inventario</li>
          <li>Analytics</li>
        </ul>
      </div>
    </section>
  );
}

export default Dashboard;
