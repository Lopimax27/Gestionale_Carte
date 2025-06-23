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
VALUES ("Southern Island", '2001-07-01'),
("Detective Pikachu", '2019-03-29');

INSERT INTO Carta (nome_pokemon, tipo, rarita, prezzo, url_img, is_reverse, id_espansione) VALUES
('Butterfree', 'Erba', 'Comune', 18.64, 'https://images.pokemontcg.io/si1/9_hires.png', FALSE, 1),
('Exeggutor', 'Erba', 'Comune', 16.63, 'https://images.pokemontcg.io/si1/13_hires.png', FALSE, 1),
('Ivysaur', 'Erba', 'Comune',22.01, 'https://images.pokemontcg.io/si1/5_hires.png', FALSE, 1),
('Jigglypuff', 'Normale', 'Comune', 21.32, 'https://images.pokemontcg.io/si1/8_hires.png', FALSE, 1),
('Lapras', 'Acqua', 'Comune', 23.32, 'https://images.pokemontcg.io/si1/12_hires.png', FALSE, 1),
('Ledyba', 'Erba', 'Rara Holo', 18.49, 'https://images.pokemontcg.io/si1/7_hires.png', FALSE, 1),
('Lickitung', 'Normale', 'Comune', 14.89, 'https://images.pokemontcg.io/si1/16_hires.png', FALSE, 1),
('Marill', 'Acqua', 'Rara Holo', 25.06, 'https://images.pokemontcg.io/si1/11_hires.png', FALSE, 1),
('Mew', 'Psico', 'Rara Holo', 145.50, 'https://images.pokemontcg.io/si1/1_hires.png', FALSE, 1),
('Onix', 'Lotta', 'Comune', 22.28, 'https://images.pokemontcg.io/si1/3_hires.png', FALSE, 1),
('Pidgeot', 'Normale', 'Comune', 19.91, 'https://images.pokemontcg.io/si1/2_hires.png', FALSE, 1),
('Primeape', 'Lotta', 'Comune', 15.23, 'https://images.pokemontcg.io/si1/18_hires.png', FALSE, 1),
('Raticate', 'Normale', 'Comune', 18.16, 'https://images.pokemontcg.io/si1/6_hires.png', FALSE, 1),
('Slowking', 'Psico', 'Rara Holo', 33.60, 'https://images.pokemontcg.io/si1/14_hires.png', FALSE, 1),
('Tentacruel', 'Acqua', 'Comune', 17.17, 'https://images.pokemontcg.io/si1/10_hires.png', FALSE, 1),
('Togepi', 'Normale', 'Rara Holo', 31.28, 'https://images.pokemontcg.io/si1/4_hires.png', FALSE, 1),
('Vileplume', 'Erba', 'Rara Holo', 27.99, 'https://images.pokemontcg.io/si1/17_hires.png', FALSE, 1),
('Wartortle', 'Acqua', 'Comune', 23.99, 'https://images.pokemontcg.io/si1/15_hires.png', FALSE, 1),
('Bulbasaur', 'Erba', 'Comune', 0.30, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_001_R_EN_LG.png', FALSE, 2),
('Lickitung', 'Normale', 'Comune', 0.15, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_016_R_EN_LG.png', FALSE, 2),
('Psyduck', 'Acqua', 'Comune', 0.20, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_007_R_EN_LG.png', FALSE, 2),
('Charmander', 'Fuoco', 'Comune', 0.40, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_004_R_EN_LG.png', FALSE, 2),
('Charizard','Fuoco','Rara Holo', 1.50, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_005_R_EN_LG.png',FALSE,2),
('Morelull', 'Erba', 'Comune', 0.20, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_003_R_EN_LG.png', FALSE, 2),
('Snubbull', 'Folletto', 'Comune', 0.18, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_0015_R_EN_LG.png', FALSE, 2),
('Magikarp', 'Acqua', 'Comune', 0.20, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_008_R_EN_LG.png', FALSE, 2),
('Machamp', 'Lotta', 'Comune', 0.25, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_013_R_EN_LG.png', FALSE, 2),
('Jigglypuff', 'Folletto', 'Comune', 0.20, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_014_R_EN_LG.png', FALSE, 2),
('Mr. Mime', 'Psico', 'Comune', 0.35, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_011_R_EN_LG.png', FALSE, 2),
('Mewtwo', 'Psico', 'Rara Holo', 1.80, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_012_R_EN_LG.png', FALSE, 2),
('Detective Pikachu', 'Elettro', 'Rara Holo', 1.50, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_010_R_EN_LG.png', FALSE, 2),
('Arcanine', 'Fuoco', 'Rara Holo', 0.30, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_006_R_EN_LG.png', FALSE, 2),
('Ditto', 'Normale', 'Comune', 0.60, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_017_R_EN_LG.png', FALSE, 2),
('Greninja', 'Acqua', 'Rara Holo', 1.20, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_009_R_EN_LG.png', FALSE, 2),
('Slaking', 'Normale', 'Comune', 0.40, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_018_R_EN_LG.png', FALSE, 2),
('Ludicolo', 'Erba', 'Comune', 0.25, 'https://limitlesstcg.nyc3.cdn.digitaloceanspaces.com/tpci/DET/DET_002_R_EN_LG.png', FALSE, 2);


Select*from Utente;
Select*from Carta;
Select*from Album;
Select*from Espansione;
Update utente set is_admin=true where id_utente=1;