CREATE TABLE vagt (
  vagt_id SERIAL PRIMARY KEY,
  område VARCHAR(255) NOT NULL,
  start_tid TIMESTAMP NOT NULL,
  slut_tid TIMESTAMP NOT NULL,
  beskrivelse VARCHAR(255),
  priotering VARCHAR(255),
  antal_personer INT NOT NULL
);