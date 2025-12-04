-- ===========================================
-- 03_fn_registrar_usuario.sql
-- Función para registrar los usuarios
-- ===========================================

CREATE OR REPLACE FUNCTION fn_registrar_usuario(
    p_nombre VARCHAR,
    p_telefono VARCHAR,
    p_direccion VARCHAR,
    p_pais_id INT,
    p_departamento_id INT,
    p_municipio_id INT
)
RETURNS INT AS $$
DECLARE
    v_exist_pais INT;
    v_exist_depto INT;
    v_exist_mpio INT;
    v_id_usuario INT;
BEGIN
    -- Validar país
    SELECT id INTO v_exist_pais FROM pais WHERE id = p_pais_id;
    IF v_exist_pais IS NULL THEN
        RAISE EXCEPTION 'El país con id % no existe.', p_pais_id;
    END IF;

    -- Validar si departamento está asociado al país
    SELECT id INTO v_exist_depto
    FROM departamento 
    WHERE id = p_departamento_id
      AND pais_id = p_pais_id;

    IF v_exist_depto IS NULL THEN
        RAISE EXCEPTION 'El departamento % no pertenece al país %.', 
            p_departamento_id, p_pais_id;
    END IF;

    -- Validar si municipio está asociado al departamento
    SELECT id INTO v_exist_mpio
    FROM municipio 
    WHERE id = p_municipio_id
      AND departamento_id = p_departamento_id;

    IF v_exist_mpio IS NULL THEN
        RAISE EXCEPTION 'El municipio % no pertenece al departamento %.',
            p_municipio_id, p_departamento_id;
    END IF;

    -- Insertar el usuario
    INSERT INTO usuario (nombre, telefono, direccion, pais_id, departamento_id, municipio_id)
    VALUES (p_nombre, p_telefono, p_direccion, p_pais_id, p_departamento_id, p_municipio_id)
    RETURNING id INTO v_id_usuario;

    RETURN v_id_usuario;
END;
$$ LANGUAGE plpgsql;
