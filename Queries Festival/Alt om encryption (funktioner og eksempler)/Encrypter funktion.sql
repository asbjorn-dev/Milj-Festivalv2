CREATE OR REPLACE FUNCTION encrypt_cpr_nummer(cpr_nummer text)
RETURNS text AS
$$
BEGIN
    RETURN pgp_sym_encrypt(cpr_nummer, 'furkan');
END;
$$ LANGUAGE plpgsql;
