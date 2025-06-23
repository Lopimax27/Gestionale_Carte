/// <summary>
/// Classe di servizio per la gestione delle operazioni relative alle collezioni di carte
/// Fornisce funzionalità per creare, gestire e visualizzare collezioni e album
/// </summary>
public class ServiziCollezione
{
    /// <summary>
    /// Riferimento all'interfaccia per le operazioni di database delle collezioni
    /// </summary>
    private readonly ICollezioneDb _collezioneDb;

    /// <summary>
    /// Costruttore che inizializza il servizio con l'implementazione del database
    /// </summary>
    /// <param name="collezioneDb">Implementazione dell'interfaccia ICollezioneDb</param>
    public ServiziCollezione(ICollezioneDb collezioneDb)
    {
        _collezioneDb = collezioneDb;
    }

    /// <summary>
    /// Ottiene la collezione dell'utente o ne crea una nuova se non esiste
    /// Questo metodo assicura che ogni utente abbia sempre una collezione attiva
    /// </summary>
    /// <param name="utenteId">ID dell'utente</param>
    /// <returns>Oggetto Collezione esistente o appena creato</returns>
    /// <exception cref="InvalidOperationException">Lanciata se la creazione della collezione fallisce</exception>
    public Collezione OttieniCreaCollezione(int utenteId)    {
        // Cerca collezione esistente per l'utente
        var collezione = _collezioneDb.TrovaPerUtenteId(utenteId);

        if (collezione == null)
        {
            // Se non esiste, richiede nome per nuova collezione
            Console.Write("Ops! Ci siamo resi conto che non hai ancora una collezione!\nInserisci il nome per la tua collezione:");
            string? nomeCollezione = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nomeCollezione))
            {
                Console.WriteLine("Nome collezione non valido!");
                throw new InvalidOperationException("Nome collezione richiesto");
            }

            // Creazione nuova collezione nel database
            bool fatto = _collezioneDb.CreaCollezione(utenteId, nomeCollezione);
            collezione = _collezioneDb.TrovaPerUtenteId(utenteId)!;//Punto esclamativo dice fidati capo non è nullo

            if (fatto)
            {
                Console.WriteLine("Collezione creata con successo!");
            }
            else
            {
                Console.WriteLine("Non siamo riusciti a creare la collezione,verifica di aver inserito un nome valido");
                throw new InvalidOperationException("Creazione collezione fallita");
            }
            return collezione;
        }
        else
        {
            // Collezione già esistente trovata
            Console.WriteLine("Abbiamo trovato la tua collezione");
            return collezione;
        }
    }

    /// <summary>
    /// Crea un nuovo album all'interno della collezione dell'utente
    /// </summary>
    /// <param name="utenteId">ID dell'utente che vuole creare l'album</param>
    /// <returns>Oggetto Album creato se successo, null se fallimento</returns>
    public Album? CreaAlbum(int utenteId)    {
        // Raccolta nome album dall'utente
        Console.Write("Inserisci il nome del tuo nuovo album: ");
        string? nomeAlbum = Console.ReadLine();

        if (string.IsNullOrEmpty(nomeAlbum))
        {
            Console.WriteLine("Nome Album vuoto. Riprovare");
            return null;
        }

        // Ottenimento/creazione collezione se necessario
        var collezione = OttieniCreaCollezione(utenteId);
        
        // Creazione album nel database
        bool creato = _collezioneDb.CreaAlbum(collezione.Id, nomeAlbum);

        if (creato)
        {
            Console.WriteLine("Album creato con successo");
        }
        else
        {
            Console.WriteLine("Errore nella creazione dell'Album riprovare");
        }

        // Recupero dell'album appena creato per restituirlo
        var album = _collezioneDb.TrovaPerNomeCollezioneId(collezione.Id, nomeAlbum);

        return album;
    }

    /// <summary>
    /// Elimina un album specifico dalla collezione dell'utente
    /// </summary>
    /// <param name="utenteId">ID dell'utente proprietario dell'album</param>
    public void EliminaALbum(int utenteId)    {
        // Ottenimento collezione dell'utente
        var collezione = OttieniCreaCollezione(utenteId);

        // Raccolta nome album da eliminare
        Console.Write("Inserisci il nome del album da eliminare: ");
        string? nomeAlbum = Console.ReadLine();

        if (string.IsNullOrEmpty(nomeAlbum))
        {
            Console.WriteLine("Nome Album vuoto. Riprovare");
            return;
        }

        // Ricerca album nella collezione
        var album = _collezioneDb.TrovaPerNomeCollezioneId(collezione.Id, nomeAlbum);

        if (album == null)
        {
            Console.WriteLine("Non è stato trovato un album con questo nome");
            return;
        }

        // Eliminazione album dal database
        bool eliminato = _collezioneDb.EliminaALbum(collezione.Id, album.Id);

        if (eliminato)
        {
            Console.WriteLine("Album eliminato con successo");
        }
        else
        {
            Console.WriteLine("Eliminazione fallita riprovare");
        }
        
    }

    /// <summary>
    /// Visualizza tutti gli album presenti nella collezione dell'utente
    /// </summary>
    /// <param name="utenteId">ID dell'utente di cui visualizzare gli album</param>
    public void VisualizzaAlbum(int utenteId)    {
        // Ottenimento collezione dell'utente
        var collezione = OttieniCreaCollezione(utenteId);

        // Recupero lista di tutti gli album nella collezione
        List<Album>? albums = _collezioneDb.VisualizzaCollezione(collezione.Id);

        if (albums == null || albums.Count == 0)
        {
            Console.WriteLine("Non hai ancora degli album! Torna al menu e creane uno per visualizzarlo.");
            return;
        }

        // Visualizzazione di ogni album nella collezione
        foreach (Album a in albums)
        {
            Console.WriteLine(a);
        }
    }
}