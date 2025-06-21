using System;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

using BCrypt.Net;

public class Program
{
    public static void Main()
    {
        string connStr = $"server=localhost;user=root;database=GestionaleCarte;port=3306;password=1234";
        MySqlConnection conn = new MySqlConnection(connStr);

        var utenteDb = new UtenteDb(conn);
        var serviziUtente = new ServiziUtente(utenteDb);

        try
        {
            conn.Open();

            bool exit = false; // Variabile che gestisce il menu di registrazione e login
            while (!exit)
            {
                Console.WriteLine("\nMenù");

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
                        Console.Write("Inserisci nome utente: ");
                        string username = Console.ReadLine();
                        Console.Write("Inserisci l'email: ");
                        string email = Console.ReadLine();
                        Console.Write("Inserisci password: ");
                        string password = Console.ReadLine();

                        bool registrato = serviziUtente.Registra(username, email, password);
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
                        Console.Write("Inserisci nome utente: ");
                        username = Console.ReadLine();
                        Console.Write("Inserisci password: ");
                        password = Console.ReadLine();

                        var utente = serviziUtente.Login(username, password);

                        if (utente == null)
                        {
                            Console.WriteLine("Login fallito!");
                            continue;
                        }
                        Console.WriteLine("Login effettuato, Benvenuto!");
                        if (utente.IsAdmin)
                        {
                            MenuAdmin(utente);
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        conn.Close();
    }


    public static void MenuUtente(Utente utente)
    {
        var eDb = new EspansioneDb(utente.Connection);
        var cDb = new CollezioneDb(utente.Connection);
        var aDb = new AlbumDb(utente.Connection);
        var serviziColl = new ServiziCollezione(cDb);
        var serviziAlbum = new ServiziAlbum(aDb, eDb, cDb);
        var serviziCarta = new ServiziCarta(new CartaDB(utente.Connection), eDb);

        bool exitUtente = false;
        while (!exitUtente)
        {
            Console.WriteLine("\n=== MENU UTENTE ===");
            Console.WriteLine("[1] Crea Album");
            Console.WriteLine("[2] Aggiungi carte ad un album");
            Console.WriteLine("[3] Rimuovi carte da un album");
            Console.WriteLine("[4] Visualizza Carte di un album");
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
                    serviziColl.CreaAlbum(utente.UtenteId);
                    break;
                case 2:
                    serviziAlbum.AggiungiCarta(utente.UtenteId);
                    break;
                case 3:
                    serviziAlbum.RimuoviCarta(utente.UtenteId);
                    break;
                case 4:
                    serviziAlbum.VisualizzaCarte(utente.UtenteId);
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

public static void MenuAdmin(Utente utente)
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
        Console.WriteLine("[2] Gestisci Carte Database");
        Console.WriteLine("[3] ");
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

                break;
            case 2:

                break;
            case 3:

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
}