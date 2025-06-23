/// <summary>
/// Classe che gestisce l'interfaccia utente e i menu dell'applicazione
/// Fornisce tutti i menu necessari per la navigazione nell'applicazione di gestione carte Pokémon.
/// Include menu per utenti normali, amministratori, e sottomenu specializzati per diverse funzionalità.
/// 
/// Struttura dei menu:
/// - Menu Principale: Registrazione e Login
/// - Menu Utente: Gestione collezioni e album personali
/// - Menu Amministratore: Gestione database espansioni e carte, visualizzazione utenti
/// - Sottomenu specializzati: Espansioni, Carte, Album, Collezioni
/// 
/// La classe utilizza il pattern di navigazione a menu con loop e switch-case per una UX intuitiva.
/// </summary>
public class Menu
{
    /// <summary>
    /// Mostra il menu principale per registrazione e login degli utenti
    /// </summary>
    /// <param name="serviziUtente">Servizio per la gestione degli utenti</param>
    public void MostraMenuPrincipale(ServiziUtente serviziUtente)
    {
        // Variabile che gestisce il loop del menu di registrazione e login
        bool exit = false;
        while (!exit)
        {
            // Visualizzazione delle opzioni del menu principale
            Console.WriteLine("\n=== Menù ===");

            Console.WriteLine("[1] Registrazione");
            Console.WriteLine("[2] Login");
            Console.WriteLine("[0] Esci");
            Console.Write("Scelta: ");

            // Parsing e validazione dell'input utente
            if (!int.TryParse(Console.ReadLine(), out int menuAction))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (menuAction)
            {
                case 1:
                    // Gestione registrazione nuovo utente
                    bool registrato = serviziUtente.Registra();
                    if (registrato)
                    {
                        Console.WriteLine("Registrazione completata!");
                    }
                    else
                    {
                        Console.WriteLine("Registrazione fallita! Riprova");
                    }
                    break;
                case 2:
                    // Gestione login utente esistente
                    var utente = serviziUtente.Login();

                    if (utente == null)
                    {
                        Console.WriteLine("Login fallito!");
                        continue;
                    }
                    Console.WriteLine("Login effettuato, Benvenuto!");

                    // Redirect al menu appropriato in base ai privilegi
                    if (utente.IsAdmin)
                    {
                        MenuAdmin(utente, serviziUtente);
                    }
                    else
                    {
                        MenuUtente(utente);
                    }
                    break;
                case 0:
                    // Uscita dall'applicazione
                    Console.WriteLine("Arrivederci!");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }
    }    /// <summary>
         /// Disegna una Pokéball dettagliata nella console utilizzando caratteri Unicode e colori
         /// Crea una rappresentazione ASCII art della iconica Pokéball con:
         /// - Parte superiore rossa
         /// - Banda centrale nera con pulsante bianco centrale
         /// - Parte inferiore bianca
         /// Utilizza i colori della console per un effetto visivo accattivante
         /// </summary>
    public void DrawDetailedPokeBall()
    {
        // Parte superiore rossa della Pokéball
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("    ████████    ");
        Console.WriteLine("  ████████████  ");
        Console.WriteLine(" ██████████████ ");
        Console.WriteLine("████████████████");

        // Banda centrale nera con pulsante bianco al centro
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("████████████████");
        Console.Write("██████");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("████"); // Pulsante centrale bianco
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("██████");
        Console.WriteLine("████████████████");

        // Parte inferiore bianca della Pokéball
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("████████████████");
        Console.WriteLine(" ██████████████ ");
        Console.WriteLine("  ████████████  ");
        Console.WriteLine("    ████████    ");

        // Reset del colore della console al valore predefinito
        Console.ResetColor();
        Console.WriteLine();
    }    /// <summary>
         /// Mostra il menu specifico per gli utenti normali (non amministratori)
         /// Fornisce accesso alla gestione delle collezioni personali e degli album
         /// </summary>
         /// <param name="utente">L'utente autenticato per cui mostrare il menu</param>
    public void MenuUtente(Utente utente)
    {
        // Inizializzazione dei servizi necessari per l'utente
        // Creazione delle istanze dei database con la connessione dell'utente
        var eDb = new EspansioneDb(utente.Connection);
        var cDb = new CollezioneDb(utente.Connection);
        var aDb = new AlbumDb(utente.Connection);

        // Inizializzazione dei servizi di business logic
        var serviziColl = new ServiziCollezione(cDb);
        var serviziAlbum = new ServiziAlbum(aDb, eDb, cDb);

        // Loop principale del menu utente
        bool exitUtente = false;
        while (!exitUtente)
        {
            // Visualizzazione delle opzioni disponibili per l'utente normale
            Console.WriteLine("\n=== MENU UTENTE ===");
            Console.WriteLine("[1] Gestisci Collezione");
            Console.WriteLine("[2] Gestisci Album");
            Console.WriteLine("[0] Logout");
            Console.Write("Scelta: ");

            // Parsing e validazione dell'input utente
            if (!int.TryParse(Console.ReadLine(), out int sceltaUtente))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (sceltaUtente)
            {
                case 1:
                    // Accesso al sottomenu per la gestione delle collezioni
                    MenuCollezione(utente, serviziColl);
                    break;
                case 2:
                    // Accesso al sottomenu per la gestione degli album
                    MenuAlbum(utente, serviziAlbum);
                    break;
                case 0:
                    // Logout dall'area utente
                    Console.WriteLine("Logout effettuato!");
                    exitUtente = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }
    }

    /// <summary>
    /// Mostra il menu specifico per gli amministratori con opzioni avanzate di gestione
    /// Permette la gestione di espansioni, carte nel database e visualizzazione utenti
    /// </summary>
    /// <param name="utente">L'utente amministratore autenticato</param>
    /// <param name="serviziUtente">Servizio per la gestione degli utenti</param>
    public void MenuAdmin(Utente utente, ServiziUtente serviziUtente)
    {
        // Inizializzazione dei servizi necessari per l'amministratore
        var eDb = new EspansioneDb(utente.Connection);
        var serviziEsp = new ServiziEspansione(eDb);
        var cDb = new CartaDB(utente.Connection);
        var serviziCarta = new ServiziCarta(cDb, eDb);

        // Loop principale del menu amministratore
        bool exitAdmin = false;
        while (!exitAdmin)
        {
            // Visualizzazione delle opzioni disponibili per l'amministratore
            Console.WriteLine("\n=== MENU AMMINISTRATORE ===");
            Console.WriteLine("[1] Gestisci Espansioni");
            Console.WriteLine("[2] Gestisci Database Carte");
            Console.WriteLine("[3] Visualizza Tutti Gli Utenti");
            Console.WriteLine("[0] Logout");
            Console.Write("Scelta: ");

            // Parsing e validazione dell'input amministratore
            if (!int.TryParse(Console.ReadLine(), out int sceltaAdmin))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (sceltaAdmin)
            {
                case 1:
                    // Accesso al sottomenu per la gestione delle espansioni
                    MenuEspansioni(utente, serviziEsp);
                    break;
                case 2:
                    // Accesso al sottomenu per la gestione delle carte nel database
                    MenuCarteAdmin(utente, serviziCarta);
                    break;
                case 3:
                    // Visualizzazione di tutti gli utenti registrati nel sistema
                    serviziUtente.StampaUtenti(utente);
                    break;
                case 0:
                    // Logout dall'area amministratore
                    Console.WriteLine("Logout effettuato!");
                    exitAdmin = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }
    }

    /// <summary>
    /// Sottomenu dedicato alla gestione delle espansioni Pokémon
    /// Permette agli amministratori di aggiungere, rimuovere e cercare espansioni nel database
    /// </summary>
    /// <param name="utente">L'utente amministratore che sta operando</param>
    /// <param name="serviziEsp">Servizio per le operazioni sulle espansioni</param>
    public void MenuEspansioni(Utente utente, ServiziEspansione serviziEsp)
    {
        // Loop per la gestione del menu espansioni
        bool uscita = false;
        while (!uscita)
        {
            // Visualizzazione delle opzioni per la gestione delle espansioni
            Console.WriteLine("\n=== MENU ESPANSIONI ===");
            Console.WriteLine("[1] Aggiungi un espansione al Database");
            Console.WriteLine("[2] Rimuovi un espansione dal Database");
            Console.WriteLine("[3] Trova un espansione nel Database");
            Console.WriteLine("[0] Ritorna al menu Admin");
            Console.Write("Scelta: ");

            // Validazione dell'input utente
            if (!int.TryParse(Console.ReadLine(), out int scelta))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (scelta)
            {
                case 1:
                    // Aggiunta di una nuova espansione al database
                    serviziEsp.CreaEspansioneDb(utente);
                    break;
                case 2:
                    // Rimozione di un'espansione esistente dal database
                    serviziEsp.RimuoviEspansioneDb(utente);
                    break;
                case 3:
                    // Ricerca di un'espansione specifica nel database
                    serviziEsp.TrovaEspansione();
                    break;
                case 0:
                    // Ritorno al menu amministratore principale
                    Console.WriteLine("Ritorno al menu admin!");
                    uscita = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }
    }

    /// <summary>
    /// Sottomenu per la gestione delle carte Pokémon nel database (accesso amministratore)
    /// Permette di aggiungere, rimuovere e visualizzare carte organizzate per espansione
    /// </summary>
    /// <param name="utente">L'utente amministratore che sta operando</param>
    /// <param name="serviziCarta">Servizio per le operazioni sulle carte</param>
    public void MenuCarteAdmin(Utente utente, ServiziCarta serviziCarta)
    {
        // Loop per la gestione del menu carte amministratore
        bool uscita = false;
        while (!uscita)
        {
            // Visualizzazione delle opzioni per la gestione delle carte
            Console.WriteLine("\n=== MENU CARTE ===");
            Console.WriteLine("[1] Aggiungi carta al Database");
            Console.WriteLine("[2] Rimuovi carta dal Database");
            Console.WriteLine("[3] Visualizza tutte le carte di un espansione in Database");
            Console.WriteLine("[0] Ritorna al menu Admin");
            Console.Write("Scelta: ");

            // Validazione dell'input utente
            if (!int.TryParse(Console.ReadLine(), out int scelta))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (scelta)
            {
                case 1:
                    // Aggiunta di una nuova carta al database
                    serviziCarta.AggiungiCartaDB(utente);
                    break;
                case 2:
                    // Rimozione di una carta esistente dal database
                    serviziCarta.RimuoviCartaDB(utente);
                    break;
                case 3:
                    // Visualizzazione di tutte le carte di una specifica espansione
                    serviziCarta.MostraCartePerEspansione();
                    break;
                case 0:
                    // Ritorno al menu amministratore principale
                    Console.WriteLine("Ritorno al menu admin!");
                    uscita = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }
    }

    /// <summary>
    /// Menu per la gestione degli album personali dell'utente
    /// Permette di aggiungere/rimuovere carte, visualizzare contenuto e calcolare il valore dell'album
    /// </summary>
    /// <param name="utente">L'utente che sta gestendo i propri album</param>
    /// <param name="serviziAlbum">Servizio per le operazioni sugli album</param>
    public void MenuAlbum(Utente utente, ServiziAlbum serviziAlbum)
    {
        // Loop per la gestione del menu album utente
        bool uscita = false;
        while (!uscita)
        {
            // Visualizzazione del menu personalizzato con il nome utente
            Console.WriteLine($"\n=== ALBUM {utente.Username.ToUpper()} ===");
            Console.WriteLine("[1] Aggiungi carte ad un album");
            Console.WriteLine("[2] Rimuovi carte da un album");
            Console.WriteLine("[3] Visualizza Carte di un album");
            Console.WriteLine("[4] Calcola il valore di un album");
            Console.WriteLine("[0] Torna al Menu Utente");
            Console.Write("Scelta: ");

            // Validazione dell'input utente
            if (!int.TryParse(Console.ReadLine(), out int sceltaUtente))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (sceltaUtente)
            {
                case 1:
                    // Aggiunta di carte all'album selezionato
                    serviziAlbum.AggiungiCarta(utente.UtenteId);
                    break;
                case 2:
                    // Rimozione di carte dall'album selezionato
                    serviziAlbum.RimuoviCarta(utente.UtenteId);
                    break;
                case 3:
                    // Visualizzazione di tutte le carte contenute nell'album
                    serviziAlbum.VisualizzaCarte(utente.UtenteId);
                    break;
                case 4:
                    // Calcolo e visualizzazione del valore totale dell'album
                    serviziAlbum.ValoreAlbum(utente.UtenteId);
                    break;
                case 0:
                    // Ritorno al menu utente principale
                    Console.WriteLine("Ritorno al menu Utente!");
                    uscita = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }

        }
    }

    /// <summary>
    /// Menu per la gestione delle collezioni dell'utente
    /// Permette di creare la prima collezione, gestire album (creazione/eliminazione/visualizzazione)
    /// </summary>
    /// <param name="utente">L'utente che sta gestendo le proprie collezioni</param>
    /// <param name="serviziColl">Servizio per le operazioni sulle collezioni</param>
    public void MenuCollezione(Utente utente, ServiziCollezione serviziColl)
    {
        // Loop per la gestione del menu collezione utente
        bool uscita = false;
        while (!uscita)
        {
            // Visualizzazione del menu personalizzato con il nome utente
            Console.WriteLine($"\n=== Collezione {utente.Username.ToUpper()} ===");
            Console.WriteLine("[1] Crea la tua prima collezione");
            Console.WriteLine("[2] Crea un Album");
            Console.WriteLine("[3] Elimina un Album");
            Console.WriteLine("[4] Visualizza tutti i tuoi Album");
            Console.WriteLine("[0] Torna al Menu Utente");
            Console.Write("Scelta: ");

            // Validazione dell'input utente
            if (!int.TryParse(Console.ReadLine(), out int sceltaUtente))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (sceltaUtente)
            {
                case 1:
                    // Creazione della prima collezione per l'utente o ottenimento di quella esistente
                    serviziColl.OttieniCreaCollezione(utente.UtenteId);
                    break;
                case 2:
                    // Creazione di un nuovo album all'interno della collezione
                    serviziColl.CreaAlbum(utente.UtenteId);
                    break;
                case 3:
                    // Eliminazione di un album esistente dalla collezione
                    serviziColl.EliminaALbum(utente.UtenteId);
                    break;
                case 4:
                    // Visualizzazione di tutti gli album appartenenti all'utente
                    serviziColl.VisualizzaAlbum(utente.UtenteId);
                    break;
                case 0:
                    // Ritorno al menu utente principale
                    Console.WriteLine("Ritorno al Menu Utente!");
                    uscita = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }

        }
    }

}
