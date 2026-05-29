import express from 'express';
const router = express.Router();

router.get('/', (req, res) => {
  res.json({ module: 'Project Management', message: 'Task, commesse, timeline e Gantt' });
});

router.get('/tasks', (req, res) => {
  res.json({ tasks: [] });
});

export default router;
