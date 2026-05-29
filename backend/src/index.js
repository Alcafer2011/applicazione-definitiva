import express from 'express';
import cors from 'cors';
import crmRoutes from './routes/crm.js';
import projectRoutes from './routes/projects.js';
import billingRoutes from './routes/billing.js';
import inventoryRoutes from './routes/inventory.js';
import analyticsRoutes from './routes/analytics.js';

const app = express();
app.use(cors());
app.use(express.json());

app.get('/health', (req, res) => res.json({ status: 'ok', service: 'workflowos-backend' }));
app.use('/api/crm', crmRoutes);
app.use('/api/projects', projectRoutes);
app.use('/api/billing', billingRoutes);
app.use('/api/inventory', inventoryRoutes);
app.use('/api/analytics', analyticsRoutes);

const port = process.env.PORT || 4000;
app.listen(port, () => {
  console.log(`WorkflowOS backend listening on http://localhost:${port}`);
});
