/// <summary>
/// Classe di servizio per la gestione delle operazioni relative agli utenti
/// Fornisce funzionalità di registrazione, login e gestione utenti
/// </summary>
public class ServiziUtente
{
    /// <summary>
    /// Riferimento all'interfaccia per le operazioni di database degli utenti
    /// </summary>
    private readonly IUtenteDb _utenteDb;

    /// <summary>
    /// Costruttore che inizializza il servizio con l'implementazione del database
    /// </summary>
    /// <param name="utenteDb">Implementazione dell'interfaccia IUtenteDb</param>
    public ServiziUtente(IUtenteDb utenteDb)
    {
        _utenteDb = utenteDb;
    }

    /// <summary>
    /// Gestisce il processo di registrazione di un nuovo utente
    /// Raccoglie dati dall'utente, li valida e li inserisce nel database
    /// </summary>
    /// <returns>True se la registrazione ha successo, False altrimenti</returns>
    public bool Registra()
    {
        // Raccolta e validazione nome utente
        Console.Write("Inserisci nome utente: ");
        string? username = Console.ReadLine();
        if (string.IsNullOrEmpty(username))
        {
            Console.WriteLine("Il nome utente non può essere vuoto. Riprovare.");
            return false;
        }

        // Raccolta e validazione email
        Console.Write("Inserisci l'email: ");
        string? email = Console.ReadLine();
        if (string.IsNullOrEmpty(email) || !ValidazioneInput.IsValidEmail(email))
        {
            Console.WriteLine("Email vuota o in formato non valido");
            return false;
        }

        // Raccolta e validazione password
        Console.Write("Inserisci password: ");
        string? password = Console.ReadLine();
        if (string.IsNullOrEmpty(password) || !ValidazioneInput.IsValidPassword(password))
        {
            Console.WriteLine("Password vuota o non valida (Piu di 8 caratteri, almeno una lettera maiuscola e minuscola e un numero)");
            return false;
        }

        // Controllo esistenza utente
        if (_utenteDb.Esiste(username, email))
        {
            Console.WriteLine("Utente già registrato, riprovare.");
            return false;
        }

        // Hash della password e inserimento nel database
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        return _utenteDb.Inserisci(username, email, passwordHash);
    }

    /// <summary>
    /// Gestisce il processo di login di un utente esistente
    /// Verifica le credenziali e restituisce l'oggetto utente se valide
    /// </summary>
    /// <returns>Oggetto Utente se login riuscito, null altrimenti</returns>
    public Utente? Login()
    {
        // Raccolta nome utente
        Console.Write("Inserisci nome utente: ");
        string? username = Console.ReadLine();
        if (string.IsNullOrEmpty(username))        {
            Console.WriteLine("Il nome utente non può essere vuoto. Riprovare.");
            return null;
        }

        // Raccolta password
        Console.Write("Inserisci password: ");
        string? password = Console.ReadLine();
        if (string.IsNullOrEmpty(password) || !ValidazioneInput.IsValidPassword(password))
        {
            Console.WriteLine("Password vuota o non valida");
            return null;
        }

        // Ricerca utente nel database
        var utente = _utenteDb.TrovaPerUsername(username);
        if (utente == null)
        {
            return null;
        }

        // Verifica password con hash memorizzato
        string? passwordHash = _utenteDb.TrovaPasswordHash(utente.UtenteId);

        if (!BCrypt.Net.BCrypt.Verify(password, passwordHash))
        {
            return null;
        }

        return utente;
    }

    /// <summary>
    /// Visualizza tutti gli utenti registrati nel sistema (solo per amministratori)
    /// </summary>
    /// <param name="utente">Utente che richiede la visualizzazione (deve essere admin)</param>
    public void StampaUtenti(Utente utente)
    {
        // Controllo privilegi amministratore
        if (!utente.IsAdmin)
        {
            Console.WriteLine($"Solo gli admin possono visualizzare gli utenti!");
            return;
        }

        // Recupero lista utenti dal database
        List<Utente>? listaUtenti = _utenteDb.TuttiUtenti();

        if (listaUtenti == null)
        {
            Console.WriteLine("Non ci sono utenti nel database o errore di connessione!");
            return;
        }

        // Stampa informazioni di ogni utente
        foreach (Utente u in listaUtenti)
        {
            Console.WriteLine(u);
        }
    }
}
