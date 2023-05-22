CREATE TABLE query_logs (
  id SERIAL PRIMARY KEY,
  timestamp TIMESTAMP DEFAULT NOW(),
  table_name TEXT,
  operation TEXT,
  data JSONB
);
