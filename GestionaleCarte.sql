create database GestionaleCarte;
use GestionaleCarte;

CREATE TABLE utente(
    id_utente INT PRIMARY KEY AUTO_INCREMENT,
    username varchar(100) UNIQUE NOT NULL,
    email varchar(100) UNIQUE NOT NULL,
    password varchar(100) UNIQUE NOT NULL,
    is_admin BOOLEAN DEFAULT FALSE
);

CREATE TABLE collezione(
    id_collezione INT PRIMARY KEY AUTO_INCREMENT,
    nome_collezione varchar(100) UNIQUE NOT NULL,
    id_utente INT,
    id_carta INT,
    FOREIGN KEY (id_utente) REFERENCES utente(id_utente),
    FOREIGN KEY (id_carta) REFERENCES carta(id_carta)
);

CREATE TABLE carta(
    id_carta INT PRIMARY KEY AUTO_INCREMENT,
    nome_pokemon varchar(100) UNIQUE NOT NULL,
    tipo ENUM("Acqua", "Fuoco", "Erba", "Lampo", "Psico", "Lotta", "Oscurità", "Metallo", "Normale", "Folletto", "Drago"),
    rarita ENUM("Comune", "Non Comune" , "Rara", "Rara-Holo", "Ultra-Rara", "Secret-Rare"),
    prezzo DECIMAL (10,2),
    url_img varchar(255),
    is_reverse BOOLEAN DEFAULT FALSE,
    id_espansione INT,
    id_collezione INT,
    FOREIGN KEY (id_espansione) REFERENCES espansione(id_espansione),
    FOREIGN KEY (id_collezione) REFERENCES collezione(id_collezione)
);

CREATE TABLE espansione(
    id_espansione INT PRIMARY KEY AUTO_INCREMENT,
    nome_espansione varchar(100) UNIQUE NOT NULL,
    anno_espansione DATE
);

CREATE TABLE album(
    id_album INT PRIMARY KEY AUTO_INCREMENT,
    id_collezione INT,
    id_carta INT,
    is_obtained BOOLEAN DEFAULT FALSE,
    is_wanted BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (id_collezione) REFERENCES collezione(id_collezione),
    FOREIGN KEY (id_carta) REFERENCES carta(id_carta)
);