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
id_utente INT UNIQUE,
FOREIGN KEY (id_utente) REFERENCES Utente(id_utente) ON DELETE CASCADE
);

CREATE TABLE Espansione(
id_espansione INT PRIMARY KEY AUTO_INCREMENT,
nome_espansione varchar(100) UNIQUE NOT NULL,
anno_espansione DATE
);

CREATE TABLE Carta(
id_carta INT PRIMARY KEY AUTO_INCREMENT,
nome_pokemon varchar(100) NOT NULL,
tipo ENUM("Acqua", "Fuoco", "Erba", "Elettro", "Psico", "Lotta", "Buio", "Metallo", "Normale", "Folletto", "Drago"),
rarita ENUM("Comune", "Non Comune" , "Rara", "Rara Holo", "Ultra Rara", "Rara Segreta"),
prezzo DECIMAL (10,2),
url_img varchar(255),
is_reverse BOOL DEFAULT FALSE,
id_espansione INT,
FOREIGN KEY (id_espansione) REFERENCES espansione(id_espansione) ON DELETE CASCADE
);

CREATE TABLE Album(
id_album INT PRIMARY KEY AUTO_INCREMENT,
nome_album varchar(100) NOT NULL,
id_collezione INT,
FOREIGN KEY (id_collezione) REFERENCES Collezione(id_collezione) ON DELETE CASCADE
);

CREATE TABLE Album_Carta(
id_album_carta INT PRIMARY KEY AUTO_INCREMENT,
id_album INT,
id_carta INT,
is_obtained BOOL DEFAULT FALSE,
is_wanted BOOL DEFAULT FALSE,
FOREIGN KEY (id_album) REFERENCES Album(id_album) ON DELETE CASCADE,
FOREIGN KEY (id_carta) REFERENCES Carta(id_carta) ON DELETE CASCADE
);

INSERT INTO Espansione (nome_espansione, anno_espansione)
VALUES ("Southern Island", '2001-07-01');

INSERT INTO Carta (nome_pokemon, tipo, rarita, prezzo, url_img, is_reverse, id_espansione) VALUES
('Ivysaur', 'Erba', 'Rara Holo',22.01, 'https://images.pokemontcg.io/si1/5_hires.png', FALSE, 1),
('Lickitung', 'Normale', 'Non Comune', 14.89, 'https://images.pokemontcg.io/si1/16_hires.png', FALSE, 1),
('Exeggutor', 'Erba', 'Non Comune', 16.63, 'https://images.pokemontcg.io/si1/13_hires.png', FALSE, 1),
('Vileplume', 'Erba', 'Rara Holo', 27.99, 'https://images.pokemontcg.io/si1/17_hires.png', FALSE, 1),
('Marill', 'Acqua', 'Comune', 25.06, 'https://images.pokemontcg.io/si1/11_hires.png', FALSE, 1),
('Lapras', 'Acqua', 'Rara Holo', 23.32, 'https://images.pokemontcg.io/si1/12_hires.png', FALSE, 1),
('Jigglypuff', 'Normale', 'Comune', 21.32, 'https://images.pokemontcg.io/si1/8_hires.png', FALSE, 1),
('Butterfree', 'Erba', 'Non Comune', 18.64, 'https://images.pokemontcg.io/si1/9_hires.png', FALSE, 1),
('Pidgeot', 'Normale', 'Rara Holo', 19.91, 'https://images.pokemontcg.io/si1/2_hires.png', FALSE, 1),
('Onix', 'Lotta', 'Comune', 22.28, 'https://images.pokemontcg.io/si1/3_hires.png', FALSE, 1),
('Mew', 'Psico', 'Rara Holo', 145.50, 'https://images.pokemontcg.io/si1/1_hires.png', FALSE, 1),
('Togepi', 'Normale', 'Comune', 31.28, 'https://images.pokemontcg.io/si1/4_hires.png', FALSE, 1),
('Ledyba', 'Erba', 'Comune', 18.49, 'https://images.pokemontcg.io/si1/7_hires.png', FALSE, 1),
('Slowking', 'Psico', 'Rara Holo', 30.60, 'https://images.pokemontcg.io/si1/14_hires.png', FALSE, 1);
