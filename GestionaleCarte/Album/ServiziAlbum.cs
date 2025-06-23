using MySql.Data.MySqlClient;

/// <summary>
/// Classe di servizio per la gestione delle operazioni relative agli album di carte
/// Fornisce funzionalità per aggiungere, rimuovere e visualizzare carte negli album
/// </summary>
public class ServiziAlbum
{
    /// <summary>
    /// Riferimento all'interfaccia per le operazioni di database degli album
    /// </summary>
    private readonly IAlbumDb _albumDb;

    /// <summary>
    /// Riferimento all'interfaccia per le operazioni di database delle espansioni
    /// </summary>
    private readonly IEspansioneDb _espansioneDb;

    /// <summary>
    /// Riferimento all'interfaccia per le operazioni di database delle collezioni
    /// </summary>
    private readonly ICollezioneDb _collezioneDb;

    /// <summary>
    /// Costruttore che inizializza il servizio con le implementazioni dei database
    /// </summary>
    /// <param name="albumDb">Implementazione dell'interfaccia IAlbumDb</param>
    /// <param name="espansioneDb">Implementazione dell'interfaccia IEspansioneDb</param>
    /// <param name="collezioneDb">Implementazione dell'interfaccia ICollezioneDb</param>
    public ServiziAlbum(IAlbumDb albumDb, IEspansioneDb espansioneDb, ICollezioneDb collezioneDb)
    {
        _albumDb = albumDb;
        _espansioneDb = espansioneDb;
        _collezioneDb = collezioneDb;
    }

    /// <summary>
    /// Permette all'utente di aggiungere una carta a un album specifico
    /// Raccoglie i dati necessari e gestisce lo stato della carta (posseduta/desiderata)
    /// </summary>
    /// <param name="utenteId">ID dell'utente che vuole aggiungere la carta</param>
    public void AggiungiCarta(int utenteId)
    {
        try
        {
            // Recupero della collezione dell'utente
            var collezione = _collezioneDb.TrovaPerUtenteId(utenteId);

            if (collezione == null)
            {
                Console.WriteLine("Collezione non trovata, assicurati di creare il tuo primo album, per creare la tua collezione");
                return;
            }

            // Raccolta nome album di destinazione
            Console.Write("Inserisci il nome dell'album in cui vuoi aggiungere una carta: ");
            string nomeAlbum = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nomeAlbum))
            {
                Console.WriteLine("Il nome dell'album non può essere vuoto. Riprova");
                return;
            }

            // Ricerca album nella collezione
            var album = _collezioneDb.TrovaPerNomeCollezioneId(collezione.Id, nomeAlbum);

            if (album == null)
            {
                Console.WriteLine("Album non trovato");
                return;
            }

            // Raccolta nome Pokémon
            Console.Write("Inserisci il nome della carta Pokémon che vuoi aggiungere al tuo Album: ");
            string nomePokemon = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(nomePokemon))
            {
                Console.WriteLine("Il nome del Pokémon non può essere vuoto. Riprova");
                return;
            }

            // Raccolta nome espansione
            Console.Write("Inserisci il nome dell'espansione: ");
            string nomeEspansione = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nomeEspansione))
            {
                Console.WriteLine("Il nome del Pokémon non può essere vuoto. Riprova");
                return;
            }

            // Validazione espansione
            var espansione = _espansioneDb.TrovaPerNome(nomeEspansione);

            if (espansione == null)
            {
                Console.WriteLine("Espansione non trovata");
                return;
            }

            // Ricerca ID carta nel database
            var idCarta = _albumDb.TrovaIdCarta(nomePokemon, espansione.Id);
            if (idCarta == null)
            {
                return;
            }
            bool isObtained, isWanted;

            // Determinazione stato della carta (posseduta o desiderata)
            Console.WriteLine("Possiedi già la carta ?\n[1] Si\n[2] No");
            if (!int.TryParse(Console.ReadLine(), out int scelta) || (scelta != 1 && scelta != 2))
            {
                Console.WriteLine("Scelta non valida");
                return;
            }
            if (scelta == 1)
            {
                isObtained = true;  // Carta posseduta
                isWanted = false;
            }
            else
            {
                isObtained = false; // Carta non posseduta
                isWanted = true;    // Carta desiderata
            }

            // Aggiunta carta all'album con stati appropriati
            bool cartaAggiunta = _albumDb.AggiungiCarta(album.Id, idCarta.Value, nomePokemon, espansione.Nome, isObtained, isWanted);

        }
        catch (MySqlException ex)
        {
            // Gestione errori specifici del database MySQL
            Console.WriteLine("Errore di connessione al database: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Gestione errori generici
            Console.WriteLine("Si è verificato un errore: " + ex.Message);
        }
    }

    /// <summary>
    /// Permette all'utente di rimuovere una carta da un album specifico
    /// </summary>
    /// <param name="utenteId">ID dell'utente che vuole rimuovere la carta</param>
    public void RimuoviCarta(int utenteId)
    {
        try
        {
            // Recupero della collezione dell'utente
            var collezione = _collezioneDb.TrovaPerUtenteId(utenteId);

            if (collezione == null)
            {
                Console.WriteLine("Collezione non trovata, assicurati di creare il tuo primo album, per creare la tua collezione");
                return;
            }

            // Raccolta nome album di origine
            Console.Write("Inserisci il nome dell'album in cui vuoi aggiungere una carta: ");
            string nomeAlbum = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nomeAlbum))
            {
                Console.WriteLine("Il nome dell'album non può essere vuoto. Riprova");
                return;
            }

            // Ricerca album nella collezione
            var album = _collezioneDb.TrovaPerNomeCollezioneId(collezione.Id, nomeAlbum);

            if (album == null)
            {
                Console.WriteLine("Album non trovato");
                return;
            }

            // Raccolta nome Pokémon da rimuovere
            Console.Write("Inserisci il nome della carta Pokémon che vuoi rimuovere dall'Album: ");
            string nomePokemon = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(nomePokemon))
            {
                Console.WriteLine("Il nome del Pokémon non può essere vuoto.");
                return;
            }

            // Raccolta nome espansione
            Console.Write("Inserisci il nome dell'espansione: ");
            string nomeEspansione = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nomeEspansione))
            {
                Console.WriteLine("Il nome del Pokémon non può essere vuoto. Riprova");
                return;
            }

            // Validazione espansione
            var espansione = _espansioneDb.TrovaPerNome(nomeEspansione);

            if (espansione == null)
            {
                Console.WriteLine("Espansione non trovata");
                return;
            }

            // Ricerca ID carta nel database
            var idCarta = _albumDb.TrovaIdCarta(nomePokemon, espansione.Id);
            if (idCarta == null)
            {
                return;
            }

            // Rimozione carta dall'album
            bool cartaRimossa = _albumDb.RimuoviCarta(album.Id, idCarta.Value, nomePokemon, espansione.Nome);

        }
        catch (MySqlException ex)
        {
            // Gestione errori specifici del database MySQL
            Console.WriteLine("Errore di connessione al database: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Gestione errori generici
            Console.WriteLine("Si è verificato un errore: " + ex.Message);
        }
    }

    /// <summary>
    /// Visualizza tutte le carte presenti in un album specifico dell'utente
    /// </summary>
    /// <param name="utenteId">ID dell'utente proprietario dell'album</param>
    public void VisualizzaCarte(int utenteId)
    {
        try
        {
            // Recupero della collezione dell'utente
            var collezione = _collezioneDb.TrovaPerUtenteId(utenteId);

            if (collezione == null)
            {
                Console.WriteLine("Collezione non trovata, assicurati di creare il tuo primo album, per creare la tua collezione");
                return;
            }

            // Raccolta nome album da visualizzare
            Console.Write("Inserisci il nome dell'album di cui vuoi visualizzare le carte: ");
            string nomeAlbum = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nomeAlbum))
            {
                Console.WriteLine("Il nome dell'album non può essere vuoto. Riprova");
                return;
            }

            // Ricerca album nella collezione
            var album = _collezioneDb.TrovaPerNomeCollezioneId(collezione.Id, nomeAlbum);

            if (album == null)
            {
                Console.WriteLine("Album non trovato");
                return;
            }

            // Recupero e visualizzazione di tutte le carte dell'album
            var carte = _albumDb.ListaCarte(album.Id);

            foreach (Carta c in carte)
            {
                Console.WriteLine(c);
            }

        }
        catch (MySqlException ex)
        {
            // Gestione errori specifici del database MySQL
            Console.WriteLine("Errore di connessione al database: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Gestione errori generici
            Console.WriteLine("Si è verificato un errore: " + ex.Message);
        }
    }

    /// <summary>
    /// Calcola e visualizza il valore monetario totale di tutte le carte in un album
    /// </summary>
    /// <param name="utenteId">ID dell'utente proprietario dell'album</param>
    public void ValoreAlbum(int utenteId)
    {
        try
        {
            // Recupero della collezione dell'utente
            var collezione = _collezioneDb.TrovaPerUtenteId(utenteId);

            if (collezione == null)
            {
                Console.WriteLine("Collezione non trovata, assicurati di creare il tuo primo album, per creare la tua collezione");
                return;
            }

            // Raccolta nome album per calcolo valore
            Console.Write("Inserisci il nome dell'album di cui vuoi calcolare il valore delle carte: ");
            string nomeAlbum = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nomeAlbum))
            {
                Console.WriteLine("Il nome dell'album non può essere vuoto. Riprova");
                return;
            }

            // Ricerca album nella collezione
            var album = _collezioneDb.TrovaPerNomeCollezioneId(collezione.Id, nomeAlbum);

            if (album == null)
            {
                Console.WriteLine("Album non trovato");
                return;
            }

            // Calcolo del valore totale dell'album
            decimal valore = _albumDb.ValoreAlbum(album.Id);

            // Visualizzazione del risultato
            Console.WriteLine($"Il tuo album {album.Nome} vale {valore}€");

        }
        catch (MySqlException ex)
        {
            // Gestione errori specifici del database MySQL
            Console.WriteLine("Errore di connessione al database: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Gestione errori generici
            Console.WriteLine("Si è verificato un errore: " + ex.Message);
        }
    }
}
