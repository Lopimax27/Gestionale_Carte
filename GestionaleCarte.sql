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
id_album INT,
FOREIGN KEY (id_utente) REFERENCES utente(id_utente),
FOREIGN KEY (id_album) REFERENCES album(id_album)
);

CREATE TABLE Carta(
id_carta INT PRIMARY KEY AUTO_INCREMENT,
nome_pokemon varchar(100) NOT NULL,
tipo ENUM("Acqua", "Fuoco", "Erba", "Elettro", "Psico", "Lotta", "Buio", "Metallo", "Normale", "Folletto", "Drago"),
rarita ENUM("Comune", "Non Comune" , "Rara", "RaraHolo", "UltraRara", "RaraSegreta"),
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
FOREIGN KEY (id_collezione) REFERENCES Collezione(id_collezione)
);

CREATE TABLE Album_Carta(
id_album_carta int primary key auto_increment,
id_album int,
id_carta int,
is_obtained bool default false,
is_wanted bool default false,
foreign key (id_album) references Album(id_album),
foreign key (id_carta) references Carta(id_carta) 
)

INSERT INTO Carta (nome_pokemon, tipo, rarita, prezzo, url_img, is_reverse, id_espansione) VALUES
('Ivysaur', 'Erba', 'RaraHolo',22.01, 'https://images.pokemontcg.io/si1/5_hires.png', FALSE, 1),
('Lickitung', 'Normale', 'Non Comune', 14.89, 'https://images.pokemontcg.io/si1/16_hires.png', FALSE, 1),
('Exeggutor', 'Erba', 'Non Comune', 16.63, 'https://images.pokemontcg.io/si1/13_hires.png', FALSE, 1),
('Vileplume', 'Erba', 'RaraHolo', 27.99, 'https://images.pokemontcg.io/si1/17_hires.png', FALSE, 1),
('Marill', 'Acqua', 'Comune', 25.06, 'https://images.pokemontcg.io/si1/11_hires.png', FALSE, 1),
('Lapras', 'Acqua', 'RaraHolo', 23.32, 'https://images.pokemontcg.io/si1/12_hires.png', FALSE, 1),
('Jigglypuff', 'Normale', 'Comune', 21.32, 'https://images.pokemontcg.io/si1/8_hires.png', FALSE, 1),
('Butterfree', 'Erba', 'Non Comune', 18.64, 'https://images.pokemontcg.io/si1/9_hires.png', FALSE, 1),
('Pidgeot', 'Normale', 'RaraHolo', 19.91, 'https://images.pokemontcg.io/si1/2_hires.png', FALSE, 1),
('Onix', 'Lotta', 'Comune', 22.28, 'https://images.pokemontcg.io/si1/3_hires.png', FALSE, 1),
('Mew', 'Psico', 'RaraHolo', 145.50, 'https://images.pokemontcg.io/si1/1_hires.png', FALSE, 1),
('Togepi', 'Normale', 'Comune', 31.28, 'https://images.pokemontcg.io/si1/4_hires.png', FALSE, 1),
('Ledyba', 'Erba', 'Comune', 18.49, 'https://images.pokemontcg.io/si1/7_hires.png', FALSE, 1),
('Slowking', 'Psico', 'RaraHolo', 30.60, 'https://images.pokemontcg.io/si1/14_hires.png', FALSE, 1);

INSERT INTO Espansione (nome_espansione, anno_espansione)
VALUES ("Southern Island", '2001-07-01');

Select*from Carta;