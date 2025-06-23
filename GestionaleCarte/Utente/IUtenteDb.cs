/// <summary>
/// Interfaccia per la gestione delle operazioni di database relative agli utenti
/// </summary>
public interface IUtenteDb
{
    /// <summary>
    /// Verifica se esiste già un utente con username o email specificati
    /// </summary>
    /// <param name="username">Nome utente da verificare</param>
    /// <param name="email">Email da verificare</param>
    /// <returns>True se l'utente esiste, False altrimenti</returns>
    bool Esiste(string username, string email);
    
    /// <summary>
    /// Inserisce un nuovo utente nel database
    /// </summary>
    /// <param name="username">Nome utente</param>
    /// <param name="email">Email dell'utente</param>
    /// <param name="passwordHash">Hash della password</param>
    /// <returns>True se l'inserimento ha successo, False altrimenti</returns>
    bool Inserisci(string username, string email, string passwordHash);
    
    /// <summary>
    /// Trova un utente tramite il suo nome utente
    /// </summary>
    /// <param name="username">Nome utente da cercare</param>
    /// <returns>Oggetto Utente se trovato, null altrimenti</returns>
    Utente? TrovaPerUsername(string username);
    
    /// <summary>
    /// Recupera l'hash della password per un determinato utente
    /// </summary>
    /// <param name="utenteId">ID dell'utente</param>
    /// <returns>Hash della password se trovato, null altrimenti</returns>
    string? TrovaPasswordHash(int utenteId);
    
    /// <summary>
    /// Recupera tutti gli utenti registrati nel sistema
    /// </summary>
    /// <returns>Lista di tutti gli utenti o null se non presenti</returns>
    List<Utente>? TuttiUtenti();
}