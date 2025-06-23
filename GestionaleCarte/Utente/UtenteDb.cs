using MySql.Data.MySqlClient;

/// <summary>
/// Implementazione concreta dell'interfaccia IUtenteDb per la gestione degli utenti nel database MySQL
/// </summary>
public class UtenteDb : IUtenteDb
{
    /// <summary>
    /// Connessione al database MySQL
    /// </summary>
    private readonly MySqlConnection _conn;

    /// <summary>
    /// Costruttore che inizializza la connessione al database
    /// </summary>
    /// <param name="connection">Connessione MySQL attiva</param>
    public UtenteDb(MySqlConnection connection)
    {
        _conn = connection;
    }

    /// <summary>
    /// Verifica se esiste già un utente con username o email specificati nel database
    /// </summary>
    /// <param name="username">Nome utente da verificare</param>
    /// <param name="email">Email da verificare</param>
    /// <returns>True se l'utente esiste, False altrimenti</returns>
    public bool Esiste(string username, string email)
    {
        try
        {
            // Query per cercare utenti con username o email corrispondenti
            string sqlUtente = "SELECT id_utente FROM utente WHERE email=@email OR username=@username";

            using var cmdUtente = new MySqlCommand(sqlUtente, _conn);
            cmdUtente.Parameters.AddWithValue("@email", email);
            cmdUtente.Parameters.AddWithValue("@username", username);

            using var rdrUser = cmdUtente.ExecuteReader();
            // Se trova almeno un record, l'utente esiste
            return rdrUser.Read();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Inserisce un nuovo utente nel database con le credenziali fornite
    /// </summary>
    /// <param name="username">Nome utente</param>
    /// <param name="email">Email dell'utente</param>
    /// <param name="passwordHash">Hash della password</param>
    /// <returns>True se l'inserimento ha successo, False altrimenti</returns>
    public bool Inserisci(string username, string email, string passwordHash)
    {
        try
        {
            // Query di inserimento nuovo utente (is_admin di default è false)
            string sqlInsert = "INSERT INTO utente (username, email, password_hash) VALUES (@username, @email, @password_hash)";
            using var cmd = new MySqlCommand(sqlInsert, _conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password_hash", passwordHash);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }    /// <summary>
    /// Cerca un utente nel database tramite il suo nome utente
    /// </summary>
    /// <param name="username">Nome utente da cercare</param>
    /// <returns>Oggetto Utente se trovato, null altrimenti</returns>
    public Utente? TrovaPerUsername(string username)
    {
        try
        {
            // Query per recuperare informazioni utente tramite username
            string sqlFind = "SELECT id_utente, is_admin,username,email FROM utente WHERE username = @username";
            using var cmd = new MySqlCommand(sqlFind, _conn);
            cmd.Parameters.AddWithValue("@username", username);

            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                // Creazione oggetto Utente con dati dal database
                int id = rdr.GetInt32("id_utente");
                string user = rdr.GetString("username");
                string email = rdr.GetString("email");
                bool isAdmin = rdr.GetBoolean("is_admin");
                return new Utente(id, user, email, isAdmin, _conn);
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Recupera l'hash della password per un determinato utente
    /// </summary>
    /// <param name="utenteId">ID dell'utente</param>
    /// <returns>Hash della password se trovato, null altrimenti</returns>
    public string? TrovaPasswordHash(int utenteId)
    {
        try
        {
            // Query per recuperare l'hash della password
            string sqlPassword = "SELECT password_hash FROM utente WHERE id_utente = @utenteId";
            using var cmd = new MySqlCommand(sqlPassword, _conn);
            cmd.Parameters.AddWithValue("@utenteId", utenteId);

            using var rdr = cmd.ExecuteReader();
            if (!rdr.Read())
            {
                return null;
            }

            return rdr.GetString("password_hash");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Recupera tutti gli utenti non amministratori registrati nel sistema
    /// </summary>
    /// <returns>Lista di tutti gli utenti normali o null se errore</returns>
    public List<Utente>? TuttiUtenti()
    {
        try
        {
            List<Utente> lista = new List<Utente>();

            // Query per recuperare solo utenti non amministratori
            using var cmd = new MySqlCommand("SELECT id_utente, is_admin,username,email FROM utente WHERE is_admin = false", _conn);
            using var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // Creazione oggetti Utente per ogni record trovato
                int id = rdr.GetInt32("id_utente");
                string user = rdr.GetString("username");
                string email = rdr.GetString("email");
                bool isAdmin = rdr.GetBoolean("is_admin");
                lista.Add(new Utente(id, user, email, isAdmin, _conn));
            }
            return lista;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}
