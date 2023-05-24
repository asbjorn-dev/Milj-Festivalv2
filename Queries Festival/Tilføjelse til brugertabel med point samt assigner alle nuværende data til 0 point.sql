ALTER TABLE bruger
ADD dine_point INT NOT NULL DEFAULT 0;

UPDATE bruger
SET dine_point = 0;