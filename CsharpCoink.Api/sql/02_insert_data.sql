-- ===========================================
-- 02_insert_data.sql
-- Datos iniciales para pruebas
-- ===========================================

-- País
INSERT INTO pais (nombre) VALUES ('Colombia');

-- Departamentos de ejemplo
INSERT INTO departamento (pais_id, nombre)
VALUES 
(1, 'Valle del Cauca'),
(1, 'Antioquia');

-- Municipios de ejemplo
INSERT INTO municipio (departamento_id, nombre)
VALUES
(1, 'Tuluá'),
(1, 'Cali'),
(2, 'Medellín');
