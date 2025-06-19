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
                int menuAction = int.Parse(Console.ReadLine());

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

                        MenuUtente(utente);
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
        utente.CreaAlbum(utente.Connection);
        
    }
}