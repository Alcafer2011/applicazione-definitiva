import React, { useState, useEffect } from 'react';
import './App.css';

const Icon = ({ name }) => {
  const icons = {
    dashboard: <path d="M3 9l9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z" />,
    brain: <path d="M9.5 2A5 5 0 0 1 12 7v5l-3 3M14.5 2A5 5 0 0 0 12 7v5l3 3M12 22v-5" />,
    graph: <path d="M18 3a3 3 0 0 0-3 3v12a3 3 0 0 0 3 3 3 3 0 0 0 3-3V6a3 3 0 0 0-3-3zM6 3a3 3 0 0 0-3 3v12a3 3 0 0 0 3 3 3 3 0 0 0 3-3V6a3 3 0 0 0-3-3z" />,
    orchestrator: <path d="M12 2L2 7l10 5 10-5-10-5zM2 17l10 5 10-5M2 12l10 5 10-5" />,
    governance: <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z" />,
    analytics: <path d="M18 20V10M12 20V4M6 20v-6" />,
    service: <path d="M21 16V8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16z" />
  };

  return (
    <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" style={{ filter: 'drop-shadow(0 0 5px var(--neon-cyan))' }}>
      {icons[name] || icons.service}
    </svg>
  );
};

function App() {
  const [cpuUsage, setCpuUsage] = useState(12.4);
  const [memUsage, setMemUsage] = useState(4.2);
  const [activeNodes, setActiveNodes] = useState(24);
  const [uptime, setUptime] = useState('00:00:00');

  useEffect(() => {
    const start = Date.now();
    const interval = setInterval(() => {
      const diff = Date.now() - start;
      const hours = Math.floor(diff / 3600000).toString().padStart(2, '0');
      const mins = Math.floor((diff % 3600000) / 60000).toString().padStart(2, '0');
      const secs = Math.floor((diff % 60000) / 1000).toString().padStart(2, '0');
      setUptime(`${hours}:${mins}:${secs}`);
      
      setCpuUsage(prev => +(prev + (Math.random() - 0.5)).toFixed(1));
      setMemUsage(prev => +(prev + (Math.random() - 0.1) * 0.1).toFixed(2));
    }, 1000);
    return () => clearInterval(interval);
  }, []);

  const services = [
    { name: 'Identity.Service', status: 'Healthy', load: '0.2%', icon: 'governance' },
    { name: 'Twin.Service', status: 'Healthy', load: '1.4%', icon: 'service' },
    { name: 'DigitalBrain.Service', status: 'Processing', load: '12.8%', icon: 'brain' },
    { name: 'Orchestrator.Service', status: 'Healthy', load: '0.8%', icon: 'orchestrator' },
    { name: 'MemoryGraph.Service', status: 'Healthy', load: '2.1%', icon: 'graph' },
    { name: 'AnalyticsHub.Service', status: 'Idle', load: '0.0%', icon: 'analytics' }
  ];

  return (
    <div className="app-shell">
      <aside className="sidebar">
        <div className="sidebar-header">
          <div className="sidebar-logo">
            <Icon name="orchestrator" />
            <span>SUPREME OS</span>
          </div>
        </div>
        
        <nav className="sidebar-menu">
          {[
            { label: 'DASHBOARD', icon: 'dashboard', active: true },
            { label: 'DIGITAL BRAIN', icon: 'brain' },
            { label: 'MEMORY GRAPH', icon: 'graph' },
            { label: 'ORCHESTRATOR', icon: 'orchestrator' },
            { label: 'GOVERNANCE', icon: 'governance' },
            { label: 'ANALYTICS', icon: 'analytics' }
          ].map(item => (
            <div key={item.label} className={`sidebar-item ${item.active ? 'active' : ''}`}>
              <Icon name={item.icon} />
              <span className="sidebar-label">{item.label}</span>
            </div>
          ))}
        </nav>

        <div className="cognitive-shell">
          <div className="holographic-input-container">
            <input type="text" placeholder="Access neural interface..." />
          </div>
        </div>
      </aside>

      <header className="topbar">
        <div className="topbar-telemetry">
          <div className="telemetry-item"><span>CPU</span> {cpuUsage}%</div>
          <div className="telemetry-item"><span>MEM</span> {memUsage}GB</div>
          <div className="telemetry-item"><span>NODES</span> {activeNodes}</div>
        </div>
        <div className="topbar-title">CORE INTERFACE :: V4.0.0-SUPREME</div>
        <div className="topbar-telemetry">
          <div className="telemetry-item"><span>UPTIME</span> {uptime}</div>
        </div>
      </header>

      <main className="content">
        <div className="dashboard-grid">
          <div className="crystal-panel">
            <div className="hud-label">Neural Engine Status</div>
            <div className="dashboard-grid">
              <div className="metric-card">
                <div className="metric-value">{cpuUsage}%</div>
                <div className="metric-label">Neural Processing</div>
              </div>
              <div className="metric-card">
                <div className="metric-value">{activeNodes}</div>
                <div className="metric-label">Neural Nodes</div>
              </div>
              <div className="metric-card">
                <div className="metric-value">0.4ms</div>
                <div className="metric-label">Synaptic Latency</div>
              </div>
            </div>
          </div>
        </div>

        <div className="crystal-panel">
          <div className="hud-label">Microservices Federation</div>
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: '15px' }}>
            {services.map(svc => (
              <div key={svc.name} style={{ 
                display: 'flex', 
                alignItems: 'center', 
                gap: '20px',
                padding: '20px', 
                background: 'rgba(255,255,255,0.03)', 
                borderRadius: '12px', 
                border: '1px solid var(--border-crystal)',
                boxShadow: 'inset 0 0 20px rgba(0, 242, 255, 0.05)'
              }}>
                <Icon name={svc.icon} />
                <div style={{ flex: 1 }}>
                  <div style={{ fontFamily: 'var(--font-tech)', fontSize: '0.8rem', color: 'white', marginBottom: '4px' }}>{svc.name}</div>
                  <div style={{ fontSize: '0.7rem', color: 'var(--text-secondary)', textTransform: 'uppercase' }}>Load: {svc.load}</div>
                </div>
                <div style={{ 
                  padding: '4px 12px', 
                  borderRadius: '20px', 
                  fontSize: '0.6rem', 
                  fontFamily: 'var(--font-tech)',
                  border: `1px solid ${svc.status === 'Healthy' ? 'var(--neon-green)' : 'var(--neon-cyan)'}`,
                  color: svc.status === 'Healthy' ? 'var(--neon-green)' : 'var(--neon-cyan)',
                  boxShadow: `0 0 10px ${svc.status === 'Healthy' ? 'rgba(0,255,65,0.2)' : 'rgba(0,242,255,0.2)'}`
                }}>
                  {svc.status}
                </div>
              </div>
            ))}
          </div>
        </div>
      </main>
    </div>
  );
}

export default App;
