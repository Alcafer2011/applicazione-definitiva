import express from 'express';
const router = express.Router();

router.get('/', (req, res) => {
  res.json({ module: 'Analytics', message: 'Dashboard BI e report operativi' });
});

router.get('/summary', (req, res) => {
  res.json({ summary: { projects: 0, invoices: 0, customers: 0 } });
});

export default router;
