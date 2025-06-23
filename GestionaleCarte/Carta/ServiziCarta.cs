using static Carta; // Per gli enumeratori

/// <summary>
/// Classe di servizio per la gestione delle operazioni relative alle carte Pokémon
/// Fornisce funzionalità per aggiungere, rimuovere e visualizzare carte nel database
/// </summary>
public class ServiziCarta
{
    /// <summary>
    /// Riferimento all'interfaccia per le operazioni di database delle carte
    /// </summary>
    public readonly ICartaDB _cartaDB;

    /// <summary>
    /// Riferimento al database delle espansioni per validazione
    /// </summary>
    private EspansioneDb _espansioneDb;

    /// <summary>
    /// Costruttore che inizializza il servizio con le implementazioni dei database
    /// </summary>
    /// <param name="cartaDB">Implementazione dell'interfaccia ICartaDB</param>
    /// <param name="espansioneDb">Database delle espansioni</param>
    public ServiziCarta(ICartaDB cartaDB, EspansioneDb espansioneDb)
    {
        _cartaDB = cartaDB;
        _espansioneDb = espansioneDb;
    }

    /// <summary>
    /// Permette agli amministratori di aggiungere una nuova carta al database
    /// Raccoglie tutti i dati necessari dall'utente e li valida prima dell'inserimento
    /// </summary>
    /// <param name="u">Utente che richiede l'operazione (deve essere admin)</param>
    public void AggiungiCartaDB(Utente u)
    {
        // Controllo privilegi amministratore
        if (!u.IsAdmin)
        {
            Console.WriteLine($"Solo gli admin possono aggiungere carte al database!");
            return;
        }

        // Raccolta nome carta
        Console.Write($"Inserisci il nome della carta: ");
        string? nomeCarta = Console.ReadLine();
        if (string.IsNullOrEmpty(nomeCarta))
        {
            Console.WriteLine("Il nome non può essere vuoto");
            return;
        }

        // Raccolta e validazione tipo carta
        Console.Write($"Inserisci il tipo della carta: ");
        Tipo tipoCarta;
        while (!Enum.TryParse(Console.ReadLine(), out tipoCarta))
        {
            Console.WriteLine($"Valore non valido. Inserisci un tipo valido: ");
        }

        // Raccolta e validazione espansione
        Console.Write($"Inserisci l'espansione della carta: ");
        string? espansioneCarta = Console.ReadLine();

        if (string.IsNullOrEmpty(espansioneCarta))
        {
            Console.WriteLine("Il nome non può essere vuoto");
            return;
        }
        int espansioneID = 0;
        do
        {
            var e = _espansioneDb.TrovaPerNome(espansioneCarta);

            if (e != null)
            {
                espansioneID = e.Id;
                break;
            }
            else
            {
                Console.WriteLine($"Espansione non valida. Inserisci un espansione valida.");
                return;
            }
        } while (true);


        // Raccolta e validazione rarità carta
        Console.Write($"Inserisci la rarita della carta: ");
        Rarita raritaCarta;
        while (!Enum.TryParse(Console.ReadLine(), out raritaCarta))
        {
            Console.Write("Valore non valido. Inserisci una rarità valida: ");
        }

        // Raccolta e validazione prezzo carta
        Console.Write($"Inserisci prezzo della carta: ");
        decimal prezzoCarta;
        while (!decimal.TryParse(Console.ReadLine(), out prezzoCarta))
        {
            Console.Write("Valore non valido. Inserisci un numero decimale per il prezzo della carta: ");
        }


        // Raccolta URL immagine carta
        Console.Write($"Inserisci url dell'immagine della carta: ");
        string? urlImgCarta = Console.ReadLine();
        if (string.IsNullOrEmpty(urlImgCarta))
        {
            Console.WriteLine("L'url può essere vuoto");
            return;
        }

        // Raccolta e validazione opzione reverse
        bool isReverse;
        do
        {
            Console.Write($"Reverse (true/false): ");
            string? reverse = Console.ReadLine();
            if (string.IsNullOrEmpty(reverse))
            {
                Console.WriteLine("Il reverse non può essere vuoto");
                return;
            }

            if (reverse!.ToLower() == "true")
            {
                isReverse = true;
                break;
            }
            else if (reverse!.ToLower()! == "false")
            {
                isReverse = false;
                break;
            }
        }
        while (true);


        // Inserimento carta nel database
        bool cartaAggiuntaDB = _cartaDB.InserisciCarta(nomeCarta, tipoCarta, raritaCarta, prezzoCarta, isReverse, espansioneID, urlImgCarta);

        if (cartaAggiuntaDB)
        {
            Console.WriteLine($"Carta {nomeCarta} , {espansioneCarta} creata con successo");
        }
        else
        {
            Console.WriteLine("Errore durante la creazione della carta");
        }
    }

    /// <summary>
    /// Mostra all'utente tutte le carte presenti in un'espansione specificata
    /// </summary>
    public void MostraCartePerEspansione()
    {
        Console.Write("Inserisci il nome dell'espansione: ");
        string? nomeEspansione = Console.ReadLine();

        if (string.IsNullOrEmpty(nomeEspansione))
        {
            Console.WriteLine("Il nome non può essere vuoto");
            return;
        }

        var carte = _cartaDB.TrovaPerEspansione(nomeEspansione!.Trim());

        if (carte.Count == 0)
        {
            Console.WriteLine("Nessuna carta trovata per questa espansione.");
            return;
        }

        Console.WriteLine($"\nCarte trovate per l'espansione '{nomeEspansione}':\n");

        foreach (var carta in carte)
        {
            Console.WriteLine($"ID: {carta.Id} | Nome: {carta.NomePokemon} | Tipo: {carta.TipoCarta} | Rarità: {carta.RaritaCarta} | Prezzo: €{carta.Prezzo}");
        }
    }

    /// <summary>
    /// Permette agli amministratori di rimuovere una carta dal database
    /// Richiede conferma dell'utente prima di procedere con la rimozione
    /// </summary>
    /// <param name="u">Utente che richiede l'operazione (deve essere admin)</param>
    public void RimuoviCartaDB(Utente u)
    {
        // Controllo privilegi amministratore
        if (!u.IsAdmin)
        {
            Console.WriteLine($"Solo gli admin possono rimuovere carte dal database!");
            return;
        }

        // Raccolta nome carta
        Console.Write("Inserisci nome: ");
        string? nomeCarta = Console.ReadLine();

        if (string.IsNullOrEmpty(nomeCarta))
        {
            Console.WriteLine("Il nome non può essere vuoto");
            return;
        }

        // Raccolta e validazione espansione
        Console.Write($"Inserisci l'espansione della carta: ");
        string? espansioneCarta = Console.ReadLine();
        if (string.IsNullOrEmpty(espansioneCarta))
        {
            Console.WriteLine("Il nome non può essere vuoto");
            return;
        }
        int espansioneID = 0;
        do
        {
            var e = _espansioneDb.TrovaPerNome(espansioneCarta);

            if (e != null)
            {
                espansioneID = e.Id;
                break;
            }
            else
            {
                Console.WriteLine($"Espansione non valida. Inserisci un espansione valida.");
                return;
            }
        } while (true);


        // Ricerca carta da eliminare
        var cartaDaEliminare = _cartaDB.TrovaCarta(nomeCarta, espansioneID);

        if (cartaDaEliminare == null)
        {
            Console.WriteLine("Carta da eliminare non trovata");
            return;
        }

        // Rimozione carta dal database
        bool cartaRimossaDB = _cartaDB.RimuoviCarta(cartaDaEliminare.Id, cartaDaEliminare.NomePokemon, espansioneCarta);

        if (cartaRimossaDB)
        {
            Console.WriteLine($"Carta {nomeCarta} , {espansioneCarta} rimossa con successo");
        }
        else
        {
            Console.WriteLine("Errore durante la rimozione della carta");
        }
    }

}
