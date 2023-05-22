SELECT bruger_id, fulde_navn, email, telefon_nummer, fødselsdag, brugernavn, decrypt_cpr(cpr_nummer, 'furkan') AS cpr_nummer FROM bruger;
