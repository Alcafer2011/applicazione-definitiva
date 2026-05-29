import express from 'express';
const router = express.Router();

router.get('/', (req, res) => {
  res.json({ module: 'CRM', message: 'Elenco clienti, contatti e opportunità' });
});

router.get('/customers', (req, res) => {
  res.json({ customers: [] });
});

export default router;
