public class ServiziUtente
{
    private readonly IUtenteDb _utenteDb;

    public ServiziUtente(IUtenteDb utenteDb)
    {
        _utenteDb = utenteDb;
    }

    public bool Registra()
    {
        Console.Write("Inserisci nome utente: ");
        string? username = Console.ReadLine();
        if (string.IsNullOrEmpty(username))
        {
            Console.WriteLine("Il nome utente non può essere vuoto. Riprovare.");
            return false;
        }

        Console.Write("Inserisci l'email: ");
        string? email = Console.ReadLine();
        if (string.IsNullOrEmpty(email) || !ValidazioneInput.IsValidEmail(email))
        {
            Console.WriteLine("Email vuota o in formato non valido");
            return false;
        }

        Console.Write("Inserisci password: ");
        string? password = Console.ReadLine();
        if (string.IsNullOrEmpty(password) || !ValidazioneInput.IsValidPassword(password))
        {
            Console.WriteLine("Password vuota o non valida (Piu di 8 caratteri, almeno una lettera maiuscola e minuscola e un numero)");
            return false;
        }

        if (_utenteDb.Esiste(username, email))
        {
            Console.WriteLine("Utente già registrato, riprovare.");
            return false;
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        return _utenteDb.Inserisci(username, email, passwordHash);
    }

    public Utente? Login()
    {
        Console.Write("Inserisci nome utente: ");
        string? username = Console.ReadLine();
        if (string.IsNullOrEmpty(username))
        {
            Console.WriteLine("Il nome utente non può essere vuoto. Riprovare.");
            return null;
        }

        Console.Write("Inserisci password: ");
        string? password = Console.ReadLine();
        if (string.IsNullOrEmpty(password) || !ValidazioneInput.IsValidPassword(password))
        {
            Console.WriteLine("Password vuota o non valida");
            return null;
        }

        var utente = _utenteDb.TrovaPerUsername(username);
        if (utente == null)
        {
            return null;
        }

        string? passwordHash = _utenteDb.TrovaPasswordHash(utente.UtenteId);

        if (!BCrypt.Net.BCrypt.Verify(password, passwordHash))
        {
            return null;
        }

        return utente;
    }

    public void StampaUtenti(Utente utente)
    {
        if (!utente.IsAdmin)
        {
            Console.WriteLine($"Solo gli admin possono visualizzare gli utenti!");
            return;
        }

        List<Utente> listaUtenti = _utenteDb.TuttiUtenti();

        if (listaUtenti == null)
        {
            Console.WriteLine("Non ci sono utenti nel database o errore di connessione!");
            return;
        }

        foreach (Utente u in listaUtenti)
        {
            Console.WriteLine(u);
        }
    }
}
