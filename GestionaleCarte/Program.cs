using MySql.Data.MySqlClient;

/// <summary>
/// Classe principale che contiene il punto di ingresso dell'applicazione Gestionale Carte
/// </summary>
public class Program
{
    /// <summary>
    /// Punto di ingresso principale dell'applicazione
    /// Gestisce la connessione al database MySQL e avvia l'interfaccia utente
    /// </summary>
    public static void Main()
    {
        // Stringa di connessione al database MySQL locale
        string connStr = $"server=localhost;user=root;database=GestionaleCarte;port=3306;password=1234";
        
        // Creazione della connessione al database
        MySqlConnection conn = new MySqlConnection(connStr);

        // Inizializzazione degli oggetti per gestire menu, database utenti e servizi
        var menu = new Menu();
        var utenteDb = new UtenteDb(conn);
        var serviziUtente = new ServiziUtente(utenteDb);

        try
        {
            // Apertura della connessione al database
            conn.Open();
            
            // Disegna il logo Pokéball
            menu.DrawDetailedPokeBall();
            
            // Avvia il menu principale dell'applicazione
            menu.MostraMenuPrincipale(serviziUtente);
        }
        catch (Exception ex)
        {
            // Gestione degli errori di connessione al database
            Console.WriteLine("Connessione al database utente "+ ex.Message);
        }
        
        // Chiusura della connessione al database
        conn.Close();
    }
}