using MySql.Data.MySqlClient;

public class ServiziAlbum
{
    private readonly IAlbumDb _albumDb;
    private readonly IEspansioneDb _espansioneDb;
    private readonly ICollezioneDb _collezioneDb;

    public ServiziAlbum(IAlbumDb albumDb, IEspansioneDb espansioneDb, ICollezioneDb collezioneDb)
    {
        _albumDb = albumDb;
        _espansioneDb = espansioneDb;
        _collezioneDb = collezioneDb;
    }

    public void AggiungiCarta(int utenteId)
    {
        try
        {
            var collezione = _collezioneDb.TrovaPerUtenteId(utenteId);

            if (collezione == null)
            {
                Console.WriteLine("Collezione non trovata, assicurati di creare il tuo primo album, per creare la tua collezione");
                return;
            }

            Console.Write("Inserisci il nome dell'album in cui vuoi aggiungere una carta: ");
            string nomeAlbum = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nomeAlbum))
            {
                Console.WriteLine("Il nome dell'album non può essere vuoto. Riprova");
                return;
            }

            var album = _collezioneDb.TrovaPerNomeCollezioneId(collezione.Id, nomeAlbum);

            if (album == null)
            {
                Console.WriteLine("Album non trovato");
                return;
            }

            Console.Write("Inserisci il nome della carta Pokémon che vuoi aggiungere al tuo Album: ");
            string nomePokemon = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(nomePokemon))
            {
                Console.WriteLine("Il nome del Pokémon non può essere vuoto. Riprova");
                return;
            }

            Console.Write("Inserisci il nome dell'espansione: ");
            string nomeEspansione = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nomeEspansione))
            {
                Console.WriteLine("Il nome del Pokémon non può essere vuoto. Riprova");
                return;
            }

            var espansione = _espansioneDb.TrovaPerNome(nomeEspansione);

            if (espansione == null)
            {
                Console.WriteLine("Espansione non trovata");
                return;
            }

            var idCarta = _albumDb.TrovaIdCarta(nomePokemon, espansione.Id);
            if (idCarta == null)
            {
                return;
            }
            bool isObtained, isWanted;

            Console.WriteLine("Possiedi già la carta ?\n[1] Si\n[2] No");
            if (!int.TryParse(Console.ReadLine(), out int scelta) || (scelta != 1 && scelta != 2))
            {
                Console.WriteLine("Scelta non valida");
                return;
            }
            if (scelta == 1)
            {
                isObtained = true;
                isWanted = false;
            }
            else
            {
                isObtained = false;
                isWanted = true;
            }

            bool cartaAggiunta = _albumDb.AggiungiCarta(album.Id, idCarta.Value, nomePokemon, espansione.Nome, isObtained, isWanted);

        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Errore di connessione al database: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Si è verificato un errore: " + ex.Message);
        }
    }

    public void RimuoviCarta(int utenteId)
    {
        try
        {
            var collezione = _collezioneDb.TrovaPerUtenteId(utenteId);

            if (collezione == null)
            {
                Console.WriteLine("Collezione non trovata, assicurati di creare il tuo primo album, per creare la tua collezione");
                return;
            }

            Console.Write("Inserisci il nome dell'album in cui vuoi aggiungere una carta: ");
            string nomeAlbum = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nomeAlbum))
            {
                Console.WriteLine("Il nome dell'album non può essere vuoto. Riprova");
                return;
            }

            var album = _collezioneDb.TrovaPerNomeCollezioneId(collezione.Id, nomeAlbum);

            if (album == null)
            {
                Console.WriteLine("Album non trovato");
                return;
            }

            Console.Write("Inserisci il nome della carta Pokémon che vuoi rimuovere dall'Album: ");
            string nomePokemon = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(nomePokemon))
            {
                Console.WriteLine("Il nome del Pokémon non può essere vuoto.");
                return;
            }

            Console.Write("Inserisci il nome dell'espansione: ");
            string nomeEspansione = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nomeEspansione))
            {
                Console.WriteLine("Il nome del Pokémon non può essere vuoto. Riprova");
                return;
            }

            var espansione = _espansioneDb.TrovaPerNome(nomeEspansione);

            if (espansione == null)
            {
                Console.WriteLine("Espansione non trovata");
                return;
            }

            var idCarta = _albumDb.TrovaIdCarta(nomePokemon, espansione.Id);
            if (idCarta == null)
            {
                return;
            }
            bool cartaRimossa = _albumDb.RimuoviCarta(album.Id, idCarta.Value, nomePokemon, espansione.Nome);

        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Errore di connessione al database: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Si è verificato un errore: " + ex.Message);
        }
    }

    public void VisualizzaCarte(int utenteId)
    {
        try
        {
            var collezione = _collezioneDb.TrovaPerUtenteId(utenteId);

            if (collezione == null)
            {
                Console.WriteLine("Collezione non trovata, assicurati di creare il tuo primo album, per creare la tua collezione");
                return;
            }

            Console.Write("Inserisci il nome dell'album di cui vuoi visualizzare le carte: ");
            string nomeAlbum = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nomeAlbum))
            {
                Console.WriteLine("Il nome dell'album non può essere vuoto. Riprova");
                return;
            }

            var album = _collezioneDb.TrovaPerNomeCollezioneId(collezione.Id, nomeAlbum);

            if (album == null)
            {
                Console.WriteLine("Album non trovato");
                return;
            }

            var carte = _albumDb.ListaCarte(album.Id);

            foreach (Carta c in carte)
            {
                Console.WriteLine(c);
            }

        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Errore di connessione al database: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Si è verificato un errore: " + ex.Message);
        }
    }

    public void ValoreAlbum(int utenteId)
    {
        try
        {
            var collezione = _collezioneDb.TrovaPerUtenteId(utenteId);

            if (collezione == null)
            {
                Console.WriteLine("Collezione non trovata, assicurati di creare il tuo primo album, per creare la tua collezione");
                return;
            }

            Console.Write("Inserisci il nome dell'album di cui vuoi calcolare il valore delle carte: ");
            string nomeAlbum = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nomeAlbum))
            {
                Console.WriteLine("Il nome dell'album non può essere vuoto. Riprova");
                return;
            }

            var album = _collezioneDb.TrovaPerNomeCollezioneId(collezione.Id, nomeAlbum);

            if (album == null)
            {
                Console.WriteLine("Album non trovato");
                return;
            }

            decimal valore = _albumDb.ValoreAlbum(album.Id);

            Console.WriteLine($"Il tuo album {album.Nome} vale {valore}€");

        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Errore di connessione al database: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Si è verificato un errore: " + ex.Message);
        }
    }
}
