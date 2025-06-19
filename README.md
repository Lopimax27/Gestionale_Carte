# 🃏 Gestionale Carte Collezionabili (Progetto Amatoriale)

Questo progetto amatoriale è un sistema completo per la gestione di collezioni di carte, sviluppato come progetto finale del corso di C#. L'applicazione permette agli utenti di organizzare, catalogare e tenere traccia delle proprie carte collezionabili, con funzionalità dettagliate per la gestione di carte, espansioni, album e profili utente.

## 🚀 Funzionalità Principali

-   **Gestione Utenti**: Sistema di registrazione e login sicuro con hashing delle password (BCrypt.Net).
-   **Gestione Carte**: Aggiunta, modifica, eliminazione e visualizzazione dettagliata delle singole carte, inclusi tipo, rarità, prezzo e immagine.
-   **Gestione Espansioni**: Cataloga le carte per set di espansione, facilitando la ricerca e l'organizzazione.
-   **Gestione Collezione**: Ogni utente ha una propria collezione che può contenere più album.
-   **Gestione Album**: Creazione, eliminazione e visualizzazione di album personalizzati per organizzare le carte. Gli album possono contenere carte "possedute" o "desiderate".
-   **Interazione con Database**: Tutte le operazioni sono persistite in un database MySQL, garantendo la coerenza e la durabilità dei dati.

## 🛠️ Tecnologie Utilizzate

-   **C#**: Linguaggio di programmazione principale per la logica applicativa e l'interfaccia a riga di comando.
-   **MySQL**: Database relazionale utilizzato per la memorizzazione di tutti i dati del gestionale.
-   **MySql.Data**: Driver ADO.NET per la connessione e l'interazione con il database MySQL.
-   **BCrypt.Net-Next**: Libreria per l'hashing e la verifica delle password, garantendo la sicurezza degli account utente.
-   **.NET 9.0**: Framework di riferimento per lo sviluppo dell'applicazione.

## 📊 Dettagli del Database

Il database `GestionaleCarte` è il cuore del sistema, progettato per memorizzare tutte le informazioni relative a carte, espansioni, album e utenti. Lo script <mcfile name="GestionaleCarte.sql" path="d:\github\Gestionale_Carte\GestionaleCarte.sql"></mcfile> definisce le seguenti tabelle principali:

-   **Utente**: Gestisce le informazioni degli utenti (id, username, email, password_hash, is_admin).
-   **Collezione**: Rappresenta la collezione di carte di un utente (id, nome_collezione, id_utente).
-   **Carta**: Contiene i dettagli di ogni singola carta (id, nome_pokemon, tipo, rarità, prezzo, url_img, is_reverse, id_espansione).
-   **Espansione**: Raggruppa le carte per set di espansione (id, nome_espansione, anno_espansione).
-   **Album**: Permette agli utenti di creare collezioni personalizzate di carte (id, nome_album, id_collezione).
-   **Album_Carta**: Tabella di collegamento per gestire le relazioni molte-a-molte tra `Album` e `Carta`, indicando se una carta è posseduta (`is_obtained`) o desiderata (`is_wanted`).

Ogni entità ha un proprio set di campi per garantire l'integrità e la completezza dei dati, con chiavi primarie e secondarie per mantenere le relazioni tra le tabelle.

## 🚶 Workflow Utente Tipico

Un utente che utilizza il gestionale di carte collezionabili seguirà generalmente il seguente flusso di lavoro:

1.  **Accesso/Registrazione**: L'utente si registra o effettua il login al sistema tramite la console.
2.  **Esplorazione Carte/Espansioni**: L'utente può visualizzare l'elenco di tutte le carte disponibili nel database o navigare tra le diverse espansioni per scoprire nuove carte.
3.  **Gestione della Propria Collezione (Album)**:
    *   **Creazione Collezione**: Ogni utente ha una collezione associata al proprio profilo.
    *   **Creazione Album**: L'utente può creare nuovi album all'interno della propria collezione per organizzare le carte (es. "Carte Preferite", "Set Completi", "Carte da Scambiare").
    *   **Aggiunta Carte all'Album**: L'utente seleziona le carte che possiede e le aggiunge ai propri album, specificando se sono state ottenute o sono desiderate.
    *   **Rimozione/Modifica Carte nell'Album**: L'utente può rimuovere carte dagli album o aggiornare le informazioni relative (es. quantità, condizione).
4.  **Ricerca e Filtro**: L'utente può cercare carte specifiche per nome, espansione, rarità o altri attributi, e filtrare i risultati per trovare rapidamente ciò che cerca.
5.  **Visualizzazione Dettagli**: Cliccando su una carta, l'utente può visualizzare tutti i dettagli specifici di quella carta.

Questo workflow è supportato da un'architettura modulare con classi di servizio (<mcsymbol name="ServiziUtente" filename="ServiziUtente.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Utente\ServiziUtente.cs" startline="4" type="class"></mcsymbol>, <mcsymbol name="ServiziCarta" filename="ServiziCarta.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Carta\ServiziCarta.cs" startline="3" type="class"></mcsymbol>, <mcsymbol name="ServiziAlbum" filename="ServiziAlbum.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Album\ServiziAlbum.cs" startline="2" type="class"></mcsymbol>, <mcsymbol name="ServiziCollezione" filename="ServiziCollezione.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Collezione\ServiziCollezione.cs" startline="3" type="class"></mcsymbol>) e classi di accesso ai dati (es. <mcsymbol name="UtenteDb" filename="UtenteDb.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Utente\UtenteDb.cs" startline="3" type="class"></mcsymbol>, <mcsymbol name="CartaDB" filename="CartaDB.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Carta\CartaDB.cs" startline="3" type="class"></mcsymbol>, <mcsymbol name="AlbumDb" filename="AlbumDb.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Album\AlbumDb.cs" startline="2" type="class"></mcsymbol>, <mcsymbol name="CollezioneDb" filename="CollezioneDb.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Collezione\CollezioneDb.cs" startline="3" type="class"></mcsymbol>, <mcsymbol name="EspansioneDb" filename="EspansioneDb.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Espansione\EspansioneDb.cs" startline="3" type="class"></mcsymbol>) che gestiscono la logica di business e l'interazione con il database MySQL.

## 📂 Struttura del Progetto

```
Gestionale_Carte/
├── 📜 .gitignore
├── 🗄️ GestionaleCarte.sql
├── 📁 GestionaleCarte/
│   ├── 👤 Admin.cs
│   ├── 💽 Album/
│   │   ├── 📄 Album.cs
│   │   ├── 💾 AlbumDb.cs
│   │   ├── 🧩 IAlbumDb.cs
│   │   └── ⚙️ ServiziAlbum.cs
│   ├── 🃏 Carta/
│   │   ├── 📄 Carta.cs
│   │   ├── 💾 CartaDB.cs
│   │   ├── 🧩 ICartaDb.cs
│   │   └── ⚙️ ServiziCarta.cs
│   ├── 📚 Collezione/
│   │   ├── 📄 Collezione.cs
│   │   ├── 💾 CollezioneDb.cs
│   │   ├── 🧩 ICollezioneDB.cs
│   │   └── ⚙️ ServiziCollezione.cs
│   ├── 📦 Espansione/
│   │   ├── 📄 Espansione.cs
│   │   ├── 💾 EspansioneDb.cs
│   │   └── 🧩 IEspansione.cs
│   ├── ⚙️ GestionaleCarte.csproj
│   ├── 🚀 Program.cs
│   └── 👥 Utente/
│       ├── 🧩 IUtenteDb.cs
│       ├── ⚙️ ServiziUtente.cs
│       ├── 📄 Utente.cs
│       └── 💾 UtenteDb.cs
├── 🌐 Gestionale_Carte.sln
└── 📝 README.md
```

### Descrizione Dettagliata dei File e delle Cartelle Chiave:

-   <mcfile name="GestionaleCarte.sql" path="d:\github\Gestionale_Carte\GestionaleCarte.sql"></mcfile>: Script SQL per la creazione del database `GestionaleCarte` e di tutte le tabelle necessarie, inclusi i dati di esempio per le carte e le espansioni.
-   <mcfolder name="GestionaleCarte" path="d:\github\Gestionale_Carte\GestionaleCarte"></mcfolder>/: Cartella principale del progetto C#, contenente il codice sorgente dell'applicazione.
    -   <mcfile name="Admin.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Admin.cs"></mcfile>: Classe che definisce un utente amministratore con credenziali predefinite. (Potrebbe essere un placeholder per future funzionalità di gestione amministrativa).
    -   <mcfile name="Carlo.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Carlo.cs"></mcfile>: Contiene la classe `UtenteProva` con metodi per la creazione e visualizzazione di collezioni e carte. Sembra essere un file di test o di esempio per funzionalità utente.
    -   <mcfile name="Program.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Program.cs"></mcfile>: Il punto di ingresso dell'applicazione. Gestisce il menu principale per la registrazione e il login degli utenti e delega le operazioni successive ai servizi appropriati.
    -   <mcfile name="GestionaleCarte.csproj" path="d:\github\Gestionale_Carte\GestionaleCarte\GestionaleCarte.csproj"></mcfile>: File di progetto C# che definisce le dipendenze (MySql.Data, BCrypt.Net-Next) e il framework di destinazione (.NET 9.0).
    -   **Sottocartelle (es. <mcfolder name="Album" path="d:\github\Gestionale_Carte\GestionaleCarte\Album"></mcfolder>, <mcfolder name="Carta" path="d:\github\Gestionale_Carte\GestionaleCarte\Carta"></mcfolder>, <mcfolder name="Collezione" path="d:\github\Gestionale_Carte\GestionaleCarte\Collezione"></mcfolder>, <mcfolder name="Espansione" path="d:\github\Gestionale_Carte\GestionaleCarte\Espansione"></mcfolder>, <mcfolder name="Utente" path="d:\github\Gestionale_Carte\GestionaleCarte\Utente"></mcfolder>)**: Ogni sottocartella rappresenta un modulo o un'entità principale del sistema e segue un pattern comune:
        -   `[NomeEntità].cs`: Definizione della classe modello (POCO - Plain Old C# Object) per l'entità (es. <mcsymbol name="Carta" filename="Carta.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Carta\Carta.cs" startline="3" type="class"></mcsymbol>, <mcsymbol name="Album" filename="Album.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Album\Album.cs" startline="3" type="class"></mcsymbol>, <mcsymbol name="Utente" filename="Utente.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Utente\Utente.cs" startline="3" type="class"></mcsymbol>).
        -   `[NomeEntità]Db.cs`: Implementazione delle operazioni di accesso ai dati (CRUD - Create, Read, Update, Delete) per l'entità, interagendo direttamente con il database (es. <mcsymbol name="CartaDB" filename="CartaDB.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Carta\CartaDB.cs" startline="3" type="class"></mcsymbol>, <mcsymbol name="UtenteDb" filename="UtenteDb.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Utente\UtenteDb.cs" startline="3" type="class"></mcsymbol>).
        -   `I[NomeEntità]Db.cs`: Interfaccia che definisce il contratto per l'accesso ai dati dell'entità, promuovendo la disaccoppiamento e la testabilità (es. <mcsymbol name="ICartaDb" filename="ICartaDb.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Carta\ICartaDb.cs" startline="2" type="class"></mcsymbol>, <mcsymbol name="IUtenteDb" filename="IUtenteDb.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Utente\IUtenteDb.cs" startline="2" type="class"></mcsymbol>).
        -   `Servizi[NomeEntità].cs`: Classe che implementa la logica di business per l'entità, utilizzando l'interfaccia del database (es. <mcsymbol name="ServiziCarta" filename="ServiziCarta.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Carta\ServiziCarta.cs" startline="3" type="class"></mcsymbol>, <mcsymbol name="ServiziUtente" filename="ServiziUtente.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Utente\ServiziUtente.cs" startline="4" type="class"></mcsymbol>).
-   <mcfile name="Gestionale_Carte.sln" path="d:\github\Gestionale_Carte\Gestionale_Carte.sln"></mcfile>: File della soluzione Visual Studio che organizza il progetto C#.
-   <mcfile name="README.md" path="d:\github\Gestionale_Carte\README.md"></mcfile>: Questo file di documentazione.

## ⚙️ Configurazione e Avvio

Per configurare ed eseguire il progetto, segui questi passaggi:

1.  **Configurazione Database**: Assicurati di avere un server MySQL installato e funzionante. Esegui lo script <mcfile name="GestionaleCarte.sql" path="d:\github\Gestionale_Carte\GestionaleCarte.sql"></mcfile> nel tuo ambiente MySQL per creare il database `GestionaleCarte` e tutte le tabelle necessarie. Puoi usare strumenti come MySQL Workbench o la riga di comando.
2.  **Stringa di Connessione**: Apri il file <mcfile name="Program.cs" path="d:\github\Gestionale_Carte\GestionaleCarte\Program.cs"></mcfile> e aggiorna la stringa di connessione `connStr` con le credenziali e l'indirizzo del tuo server MySQL. Attualmente è impostata su `server=localhost;user=root;database=GestionaleCarte;port=3306;password=1234`.
3.  **Ripristino Dipendenze**: Apri il terminale nella directory principale del progetto (`Gestionale_Carte`) ed esegui `dotnet restore` per scaricare tutte le dipendenze NuGet necessarie (MySql.Data, BCrypt.Net-Next).
4.  **Compilazione**: Compila il progetto eseguendo `dotnet build` nel terminale o aprendo la soluzione <mcfile name="Gestionale_Carte.sln" path="d:\github\Gestionale_Carte\Gestionale_Carte.sln"></mcfile> con Visual Studio e compilando da lì.
5.  **Esecuzione**: Avvia l'applicazione eseguendo `dotnet run --project GestionaleCarte` dalla directory principale del progetto, oppure esegui il file `.exe` compilato (solitamente in `Gestionale_Carte\GestionaleCarte\bin\Debug\net9.0\`).

## 🤝 Collaboratori

Questo progetto è stato sviluppato con il contributo di:

-   Carlo Condello
-   Andrea Fabbri
-   Alessio Macrì
-   Alessandro Lopardo

## 📝 Licenza

Questo progetto è rilasciato sotto licenza MIT. Per maggiori dettagli, consulta il file `LICENSE` (se presente) o considera che sei libero di usare, modificare e distribuire il codice, a condizione di includere l'attribuzione originale.

## ✨ Contributi

I contributi sono benvenuti! Se desideri migliorare questo progetto, sentiti libero di fare un fork del repository, implementare le tue modifiche e inviare una pull request. Per modifiche significative, si prega di aprire prima un'issue per discutere cosa si desidera cambiare.
