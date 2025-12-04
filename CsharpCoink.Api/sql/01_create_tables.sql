-- ===========================================
-- 01_create_tables.sql
-- Creación de tablas base para la prueba
-- ===========================================

-- (Opcional para el manejo de acentos)
-- CREATE DATABASE csharp_coink
--     WITH ENCODING 'UTF8';

-- Conectarse a la base de datos 
-- \c csharp_coink

-- Tabla de países
CREATE TABLE pais (
    id          SERIAL PRIMARY KEY,
    nombre      VARCHAR(100) NOT NULL UNIQUE
);

-- Tabla de departamentos
CREATE TABLE departamento (
    id          SERIAL PRIMARY KEY,
    pais_id     INT NOT NULL,
    nombre      VARCHAR(100) NOT NULL,
    CONSTRAINT fk_departamento_pais
        FOREIGN KEY (pais_id)
        REFERENCES pais (id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    CONSTRAINT uq_departamento_pais_nombre
        UNIQUE (pais_id, nombre)
);

-- Tabla de municipios
CREATE TABLE municipio (
    id              SERIAL PRIMARY KEY,
    departamento_id INT NOT NULL,
    nombre          VARCHAR(150) NOT NULL,
    CONSTRAINT fk_municipio_departamento
        FOREIGN KEY (departamento_id)
        REFERENCES departamento (id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    CONSTRAINT uq_municipio_departamento_nombre
        UNIQUE (departamento_id, nombre)
);

-- Tabla de usuarios
CREATE TABLE usuario (
    id              SERIAL PRIMARY KEY,
    nombre          VARCHAR(150) NOT NULL,
    telefono        VARCHAR(20)  NOT NULL,
    direccion       VARCHAR(200) NOT NULL,
    pais_id         INT NOT NULL,
    departamento_id INT NOT NULL,
    municipio_id    INT NOT NULL,
    CONSTRAINT fk_usuario_pais
        FOREIGN KEY (pais_id)
        REFERENCES pais (id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    CONSTRAINT fk_usuario_departamento
        FOREIGN KEY (departamento_id)
        REFERENCES departamento (id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    CONSTRAINT fk_usuario_municipio
        FOREIGN KEY (municipio_id)
        REFERENCES municipio (id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
);
