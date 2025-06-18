using System;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

public class Tutorial1
{
    public static void Main()
    {
        string connStr = "server=localhost;user=root;database=GestionaleCarte;port=3306;password=";
        Console.Write($"Inserisci la password del database: ");
        string p = Console.ReadLine();
        connStr += p;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            // 
            Carta a = new Carta();
            a.AggiungiCarta(conn);


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        conn.Close();
        Console.WriteLine("Done.");
    }
}