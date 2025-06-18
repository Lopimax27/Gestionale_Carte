create database GestionaleCarte;
use GestionaleCarte;

CREATE TABLE Utente(
id_utente INT PRIMARY KEY AUTO_INCREMENT,
username varchar(100) UNIQUE NOT NULL,
email varchar(100) UNIQUE NOT NULL,
password_hash varchar(100) NOT NULL,
is_admin BOOL DEFAULT FALSE
);

CREATE TABLE Collezione(
id_collezione INT PRIMARY KEY AUTO_INCREMENT,
nome_collezione varchar(100) NOT NULL,
id_utente INT unique,
id_carta INT,
FOREIGN KEY (id_utente) REFERENCES utente(id_utente),
FOREIGN KEY (id_carta) REFERENCES carta(id_carta)
);

CREATE TABLE Carta(
id_carta INT PRIMARY KEY AUTO_INCREMENT,
nome_pokemon varchar(100) NOT NULL,
tipo ENUM("Acqua", "Fuoco", "Erba", "Elettro", "Psico", "Lotta", "Buio", "Metallo", "Normale", "Folletto", "Drago"),
rarita ENUM("Comune", "Non Comune" , "Rara", "Rara-Holo", "Ultra-Rara", "Rara-Segreta"),
prezzo DECIMAL (10,2),
url_img varchar(255),
is_reverse BOOL DEFAULT FALSE,
id_espansione INT,
FOREIGN KEY (id_espansione) REFERENCES espansione(id_espansione)
);

CREATE TABLE Espansione(
id_espansione INT PRIMARY KEY AUTO_INCREMENT,
nome_espansione varchar(100) UNIQUE NOT NULL,
anno_espansione DATE
);

CREATE TABLE Album(
id_album INT PRIMARY KEY AUTO_INCREMENT,
nome_album varchar(100) not null,
id_collezione INT,
id_carta INT,
is_obtained BOOL DEFAULT FALSE,
is_wanted BOOL DEFAULT FALSE,
FOREIGN KEY (id_collezione) REFERENCES collezione(id_collezione),
FOREIGN KEY (id_carta) REFERENCES carta(id_carta)
);