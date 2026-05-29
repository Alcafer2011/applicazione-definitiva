import express from 'express';
const router = express.Router();

router.get('/', (req, res) => {
  res.json({ module: 'Billing', message: 'Fatture, preventivi e flussi di cassa' });
});

router.get('/invoices', (req, res) => {
  res.json({ invoices: [] });
});

export default router;
