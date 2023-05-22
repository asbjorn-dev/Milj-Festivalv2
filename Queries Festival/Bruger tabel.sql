CREATE TABLE bruger (
  bruger_id SERIAL PRIMARY KEY,
  fulde_navn VARCHAR(255) NOT NULL,
  rolle VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL,
  telefon_nummer INT NOT NULL,
  fødselsdag DATE NOT NULL CHECK (DATE_PART('year', CURRENT_DATE) - DATE_PART('year', fødselsdag) >= 18),
  brugernavn VARCHAR(20) NOT NULL,
  password VARCHAR(255) NOT NULL CHECK (password ~ '\d'),
  er_aktiv BOOL NOT NULL,
  er_blacklistet BOOL NOT NULL,
  cpr_nummer VARCHAR(255) NOT NULL,
  CONSTRAINT cpr_nummer_encrypted CHECK (pgp_sym_decrypt(pgp_sym_encrypt(cpr_nummer, 'furkan'), 'furkan') = cpr_nummer)
);