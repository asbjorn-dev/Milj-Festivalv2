CREATE OR REPLACE FUNCTION decrypt_cpr(cpr_encrypted text, secret_key text) 
RETURNS text 
AS $$
SELECT pgp_sym_decrypt(cpr_encrypted::bytea, secret_key) 
$$ LANGUAGE SQL;