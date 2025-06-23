public class Menu
{
    public void MostraMenuPrincipale(ServiziUtente serviziUtente)
    {
        bool exit = false; // Variabile che gestisce il menu di registrazione e login
        while (!exit)
        {
            Console.WriteLine("\n=== Menù ===");

            Console.WriteLine("[1] Registrazione");
            Console.WriteLine("[2] Login");
            Console.WriteLine("[0] Esci");
            Console.Write("Scelta: ");


            if (!int.TryParse(Console.ReadLine(), out int menuAction))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (menuAction)
            {
                case 1:
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
                    var utente = serviziUtente.Login();

                    if (utente == null)
                    {
                        Console.WriteLine("Login fallito!");
                        continue;
                    }
                    Console.WriteLine("Login effettuato, Benvenuto!");
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
                    Console.WriteLine("Arrivederci, alla prossima!");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }
    }

    public void DrawDetailedPokeBall()
    {
        // Parte superiore rossa
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("    ████████    ");
        Console.WriteLine("  ████████████  ");
        Console.WriteLine(" ██████████████ ");
        Console.WriteLine("████████████████");
        
        // Banda centrale nera con pulsante
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("████████████████");
        Console.Write("██████");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("████");
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("██████");
        Console.WriteLine("████████████████");
        
        // Parte inferiore bianca
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("████████████████");
        Console.WriteLine(" ██████████████ ");
        Console.WriteLine("  ████████████  ");
        Console.WriteLine("    ████████    ");
        
        Console.ResetColor();
        Console.WriteLine();
    }
    public void MenuUtente(Utente utente)
    {
        var eDb = new EspansioneDb(utente.Connection);
        var cDb = new CollezioneDb(utente.Connection);
        var aDb = new AlbumDb(utente.Connection);
        var serviziColl = new ServiziCollezione(cDb);
        var serviziAlbum = new ServiziAlbum(aDb, eDb, cDb);

        bool exitUtente = false;
        while (!exitUtente)
        {
            Console.WriteLine("\n=== MENU UTENTE ===");
            Console.WriteLine("[1] Gestisci Collezione");
            Console.WriteLine("[2] Gestisci Album");
            Console.WriteLine("[0] Logout");
            Console.Write("Scelta: ");

            if (!int.TryParse(Console.ReadLine(), out int sceltaUtente))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (sceltaUtente)
            {
                case 1:
                    MenuCollezione(utente, serviziColl);
                    break;
                case 2:
                    MenuAlbum(utente, serviziAlbum);
                    break;
                case 0:
                    Console.WriteLine("Logout effettuato!");
                    exitUtente = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }
    }

    public void MenuAdmin(Utente utente, ServiziUtente serviziUtente)
    {
        var eDb = new EspansioneDb(utente.Connection);
        var serviziEsp = new ServiziEspansione(eDb);
        var cDb = new CartaDB(utente.Connection);
        var serviziCarta = new ServiziCarta(cDb, eDb);

        bool exitAdmin = false;
        while (!exitAdmin)
        {
            Console.WriteLine("\n=== MENU AMMINISTRATORE ===");
            Console.WriteLine("[1] Gestisci Espansioni");
            Console.WriteLine("[2] Gestisci Database Carte");
            Console.WriteLine("[3] Visualizza Tutti Gli Utenti");
            Console.WriteLine("[0] Logout");
            Console.Write("Scelta: ");

            if (!int.TryParse(Console.ReadLine(), out int sceltaAdmin))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (sceltaAdmin)
            {
                case 1:
                    MenuEspansioni(utente, serviziEsp);
                    break;
                case 2:
                    MenuCarteAdmin(utente, serviziCarta);
                    break;
                case 3:
                    serviziUtente.StampaUtenti(utente);
                    break;
                case 0:
                    Console.WriteLine("Logout effettuato!");
                    exitAdmin = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }
    }

    public void MenuEspansioni(Utente utente, ServiziEspansione serviziEsp)
    {
        bool uscita = false;
        while (!uscita)
        {
            Console.WriteLine("\n=== MENU ESPANSIONI ===");
            Console.WriteLine("[1] Aggiungi un espansione al Database");
            Console.WriteLine("[2] Rimuovi un espansione dal Database");
            Console.WriteLine("[3] Trova un espansione nel Database");
            Console.WriteLine("[0] Ritorna al menu Admin");
            Console.Write("Scelta: ");

            if (!int.TryParse(Console.ReadLine(), out int scelta))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (scelta)
            {
                case 1:
                    serviziEsp.CreaEspansioneDb(utente);
                    break;
                case 2:
                    serviziEsp.RimuoviEspansioneDb(utente);
                    break;
                case 3:
                    serviziEsp.TrovaEspansione();
                    break;
                case 0:
                    Console.WriteLine("Ritorno al menu admin!");
                    uscita = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }
    }

    public void MenuCarteAdmin(Utente utente, ServiziCarta serviziCarta)
    {
        bool uscita = false;
        while (!uscita)
        {
            Console.WriteLine("\n=== MENU CARTE ===");
            Console.WriteLine("[1] Aggiungi carta al Database");
            Console.WriteLine("[2] Rimuovi carta dal Database");
            Console.WriteLine("[3] Visualizza tutte le carte di un espansione in Database");
            Console.WriteLine("[0] Ritorna al menu Admin");
            Console.Write("Scelta: ");

            if (!int.TryParse(Console.ReadLine(), out int scelta))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (scelta)
            {
                case 1:
                    serviziCarta.AggiungiCartaDB(utente);
                    break;
                case 2:
                    serviziCarta.RimuoviCartaDB(utente);
                    break;
                case 3:
                    serviziCarta.MostraCartePerEspansione();
                    break;
                case 0:
                    Console.WriteLine("Ritorno al menu admin!");
                    uscita = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }
    }

    public void MenuAlbum(Utente utente, ServiziAlbum serviziAlbum)
    {
        bool uscita = false;
        while (!uscita)
        {
            Console.WriteLine($"\n=== ALBUM {utente.Username.ToUpper()} ===");
            Console.WriteLine("[1] Aggiungi carte ad un album");
            Console.WriteLine("[2] Rimuovi carte da un album");
            Console.WriteLine("[3] Visualizza Carte di un album");
            Console.WriteLine("[4] Calcola il valore di un album");
            Console.WriteLine("[0] Torna al Menu Utente");
            Console.Write("Scelta: ");

            if (!int.TryParse(Console.ReadLine(), out int sceltaUtente))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (sceltaUtente)
            {
                case 1:
                    serviziAlbum.AggiungiCarta(utente.UtenteId);
                    break;
                case 2:
                    serviziAlbum.RimuoviCarta(utente.UtenteId);
                    break;
                case 3:
                    serviziAlbum.VisualizzaCarte(utente.UtenteId);
                    break;
                case 4:
                    serviziAlbum.ValoreAlbum(utente.UtenteId);
                    break;
                case 0:
                    Console.WriteLine("Ritorno al menu Utente!");
                    uscita = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }

        }
    }

    public void MenuCollezione(Utente utente, ServiziCollezione serviziColl)
    {
        bool uscita = false;
        while (!uscita)
        {
            Console.WriteLine($"\n=== Collezione {utente.Username.ToUpper()} ===");
            Console.WriteLine("[1] Crea la tua prima collezione");
            Console.WriteLine("[2] Crea un Album");
            Console.WriteLine("[3] Elimina un Album");
            Console.WriteLine("[4] Visualizza tutti i tuoi Album");
            Console.WriteLine("[0] Torna al Menu Utente");
            Console.Write("Scelta: ");

            if (!int.TryParse(Console.ReadLine(), out int sceltaUtente))
            {
                Console.WriteLine("Scelta non valida");
                continue;
            }

            switch (sceltaUtente)
            {
                case 1:
                    serviziColl.OttieniCreaCollezione(utente.UtenteId);
                    break;
                case 2:
                    serviziColl.CreaAlbum(utente.UtenteId);
                    break;
                case 3:
                    serviziColl.EliminaALbum(utente.UtenteId);
                    break;
                case 4:
                    serviziColl.VisualizzaAlbum(utente.UtenteId);
                    break;
                case 0:
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
