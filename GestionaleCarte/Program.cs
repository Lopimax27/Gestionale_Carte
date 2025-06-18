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
                        RegistraUtente(conn);
                        break;
                    case 2:
                        LoginUtente(conn);
                        break;
                    case 0:
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

    public static void RegistraUtente(MySqlConnection conn)
    {
        Console.Write("Inserisci nome utente: ");
        string username = Console.ReadLine();
        Console.Write("Inserisci l'email: ");
        string email = Console.ReadLine();
        Console.Write("Inserisci password: ");
        string password = Console.ReadLine();

        string sqlUtente = "Select id_utente from Utente where email=@email or username=@username;";

        MySqlCommand cmdUtente = new MySqlCommand(sqlUtente, conn);
        cmdUtente.Parameters.AddWithValue("@email", email);
        cmdUtente.Parameters.AddWithValue("@username", username);

        MySqlDataReader rdrUser = cmdUtente.ExecuteReader();

        if (rdrUser.Read())
        {
            rdrUser.Close();
            Console.WriteLine("Utente gia esistente, riprovare.");
            return;
        }
        rdrUser.Close();

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        cmdUtente.CommandText = "Insert into Utente(username,email,password_hash) values (@username,@email,@password)";
        cmdUtente.Parameters.AddWithValue("@password", hashedPassword); // Usare la password hashata
        cmdUtente.ExecuteNonQuery();

        Console.WriteLine("Utente creato con successo!");

    }

    public static void LoginUtente(MySqlConnection conn)
    {
        Console.Write("Inserisci nome utente: ");
        string username = Console.ReadLine();
        Console.Write("Inserisci password: ");
        string password = Console.ReadLine();

        Admin admin = new Admin();
        bool isAdmin = AdminLogin(admin, username, password);
        bool isUser = UtenteLogin(conn, username, password, out int userId);
        Utente utente = new Utente(conn, userId);
        if (isAdmin)
        {
            
            //AdminMenu(admin, conn);
        }
        else if (isUser)
        {

            //UtenteMenu(utente, conn);
        }
    }
    public static bool AdminLogin(Admin admin, string username, string password)
    {
        if (username == admin.Username && password == admin.Password)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool UtenteLogin(MySqlConnection conn, string username, string password, out int userId)
    {
        string sqlUser = "Select u.id_utente,u.username, u.password_hash from utente u where u.username=@username;";
        MySqlCommand cmdUser = new MySqlCommand(sqlUser, conn);
        cmdUser.Parameters.AddWithValue("@username", username);
        MySqlDataReader rdrUser = cmdUser.ExecuteReader();

        if (rdrUser.Read())
        {
            userId = (int)rdrUser[0];
            string storedHash = rdrUser["password_hash"].ToString();
            rdrUser.Close();
            
            // Verificare la password con BCrypt
            if (BCrypt.Net.BCrypt.Verify(password, storedHash))
            {
                Console.WriteLine("Utente loggato");
                return true;
            }
            else
            {
                Console.WriteLine("Password sbagliata");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Utente non esistente");
            userId = 0;
            rdrUser.Close();
            return false;
        }
    }
}