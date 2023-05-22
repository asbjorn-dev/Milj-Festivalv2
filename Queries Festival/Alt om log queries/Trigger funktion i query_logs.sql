CREATE OR REPLACE FUNCTION log_query()
RETURNS TRIGGER AS $$
BEGIN
  IF TG_OP = 'INSERT' THEN
    INSERT INTO query_logs (table_name, operation, data)
    VALUES (TG_TABLE_NAME, TG_OP, row_to_json(NEW));
  ELSIF TG_OP = 'UPDATE' THEN
    INSERT INTO query_logs (table_name, operation, data)
    VALUES (TG_TABLE_NAME, TG_OP, row_to_json(NEW));
  ELSIF TG_OP = 'DELETE' THEN
    INSERT INTO query_logs (table_name, operation, data)
    VALUES (TG_TABLE_NAME, TG_OP, row_to_json(OLD));
  END IF;
  RETURN NULL; -- Return NULL since there is no NEW or OLD row in DELETE operation
END;
$$
LANGUAGE plpgsql;
