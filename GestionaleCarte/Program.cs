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

        var menu = new Menu();
        var utenteDb = new UtenteDb(conn);
        var serviziUtente = new ServiziUtente(utenteDb);

        try
        {
            conn.Open();
            menu.MostraMenuPrincipale(serviziUtente);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Connessione al database utente "+ ex.Message);
        }
        conn.Close();
    }
}