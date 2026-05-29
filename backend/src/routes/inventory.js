import express from 'express';
const router = express.Router();

router.get('/', (req, res) => {
  res.json({ module: 'Inventory', message: 'Gestione magazzino, lotti e materiali' });
});

router.get('/stock', (req, res) => {
  res.json({ stock: [] });
});

export default router;
