using MySql.Data.MySqlClient;

/// <summary>
/// Rappresenta un utente del sistema con le sue informazioni e privilegi
/// </summary>
public class Utente
{
    /// <summary>
    /// Identificatore univoco dell'utente nel database
    /// </summary>
    public int UtenteId { get; }
    
    /// <summary>
    /// Indica se l'utente ha privilegi di amministratore
    /// </summary>
    public bool IsAdmin { get; }
    
    /// <summary>
    /// Indirizzo email dell'utente
    /// </summary>
    public string Email { get; }
    
    /// <summary>
    /// Nome utente scelto dall'utente per il login
    /// </summary>
    public string Username { get; }
    
    /// <summary>
    /// Connessione al database MySQL associata a questo utente
    /// </summary>
    public MySqlConnection Connection { get; private set; }

    /// <summary>
    /// Costruttore per creare un nuovo oggetto Utente
    /// </summary>
    /// <param name="utenteId">ID univoco dell'utente</param>
    /// <param name="username">Nome utente</param>
    /// <param name="email">Email dell'utente</param>
    /// <param name="isAdmin">Flag che indica se è amministratore</param>
    /// <param name="connection">Connessione al database</param>
    public Utente(int utenteId, string username, string email, bool isAdmin, MySqlConnection connection)
    {
        UtenteId = utenteId;
        Email = email;
        Username = username;
        IsAdmin = isAdmin;
        Connection = connection;
    }

    /// <summary>
    /// Restituisce una rappresentazione testuale dell'utente
    /// </summary>
    /// <returns>Stringa con le informazioni principali dell'utente</returns>
    public override string ToString()
    {
        return $"UtenteId: {UtenteId} | Username: {Username} | Email: {Email}";
    }
}
