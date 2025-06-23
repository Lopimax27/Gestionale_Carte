/// <summary>
/// Classe di servizio per la gestione delle operazioni relative alle espansioni di carte Pokémon
/// Fornisce funzionalità per creare, cercare e rimuovere espansioni dal database
/// </summary>
public class ServiziEspansione
{
    /// <summary>
    /// Riferimento all'interfaccia per le operazioni di database delle espansioni
    /// </summary>
    public readonly IEspansioneDb _espansioneDb;

    /// <summary>
    /// Costruttore che inizializza il servizio con l'implementazione del database
    /// </summary>
    /// <param name="espansioneDb">Implementazione dell'interfaccia IEspansioneDb</param>
    public ServiziEspansione(IEspansioneDb espansioneDb)
    {
        _espansioneDb = espansioneDb;
    }

    /// <summary>
    /// Cerca un'espansione nel database tramite il nome inserito dall'utente
    /// </summary>
    /// <returns>Oggetto Espansione se trovata, null altrimenti</returns>
    public Espansione? TrovaEspansione()    {
        // Raccolta nome espansione dall'utente
        Console.Write("Inserisci il nome dell'espansione: ");
        string nome = Console.ReadLine()?.Trim();

        // Ricerca espansione nel database
        var esp = _espansioneDb.TrovaPerNome(nome);

        if (esp != null)
        {
            Console.WriteLine($"Espansione trovata {esp.Nome} ");
        }
        else
        {
            Console.WriteLine("Nessuna espansione trovata con questo nome");
        }
        return esp;
    }

    /// <summary>
    /// Permette agli amministratori di creare una nuova espansione nel database
    /// Raccoglie nome e data di rilascio, validando i dati prima dell'inserimento
    /// </summary>
    /// <param name="utente">Utente che richiede l'operazione (deve essere admin)</param>
    public void CreaEspansioneDb(Utente utente)    {
        // Controllo privilegi amministratore
        if (!utente.IsAdmin)
        {
            Console.WriteLine("Solo gli admin possono creare nuove espansione!");
            return;
        }

        // Raccolta nome espansione
        Console.Write("Inserisci il nome dell'espansione: ");
        string nome = Console.ReadLine()?.Trim();

        // Raccolta e validazione data di rilascio
        Console.Write("Inserisci anno di uscita (formato yyyy-mm-gg): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime anno))
        {
            Console.Write("Data non valida");
            return;
        }

        // Inserimento espansione nel database
        bool riuscito = _espansioneDb.InserisciEspansione(nome, anno);

        if (riuscito)
        {
            Console.WriteLine($"Espansione {nome} creata con successo");
        }
        else
        {
            Console.WriteLine("Errore durante la creazione dell'espansione");
        }
    }

    /// <summary>
    /// Permette agli amministratori di rimuovere un'espansione dal database
    /// Prima cerca l'espansione e poi procede con l'eliminazione se trovata
    /// </summary>
    /// <param name="utente">Utente che richiede l'operazione (deve essere admin)</param>
    public void RimuoviEspansioneDb(Utente utente)    {
        // Controllo privilegi amministratore
        if (!utente.IsAdmin)
        {
            Console.WriteLine("Solo gli admin possono eliminare un espansione!");
            return;
        }

        // Ricerca espansione da eliminare tramite nome
        Espansione esp = TrovaEspansione();

        if (esp == null)
        {
            return;
        }

        // Eliminazione espansione dal database
        bool eliminato = _espansioneDb.RimuoviEspansione(esp.Id);
        if (eliminato)
        {
            Console.WriteLine($"Espansione {esp.Nome} eliminata dal database");
        }
        else
        {
            Console.WriteLine($"Eliminazione fallita, Riprovare");
        }
    }
}