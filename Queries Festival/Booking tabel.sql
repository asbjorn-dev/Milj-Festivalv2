CREATE TABLE booking (
  booking_id SERIAL PRIMARY KEY,
  er_låst BOOLEAN,
  bruger_id INT NOT NULL,	
  vagt_id INT NOT NULL,
  FOREIGN KEY (bruger_id) REFERENCES bruger (bruger_id),
  FOREIGN KEY (vagt_id) REFERENCES vagt (vagt_id)
);