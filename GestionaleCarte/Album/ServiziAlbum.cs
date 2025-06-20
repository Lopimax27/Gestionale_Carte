using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;

public class ServiziAlbum
{
    public readonly IAlbumDb _albumDb;

    public ServiziAlbum(IAlbumDb albumDb)
    {
        _albumDb = albumDb;
    }

    public void AggiungiCarta()
    {
        try
        {
            Console.Write("Inserisci l'ID dell'album su cui vuoi operare: ");
            if (!int.TryParse(Console.ReadLine(), out int idAlbum))
            {
                Console.WriteLine("ID album non valido.");
                return;
            }

            Console.Write("Inserisci il nome della carta Pokémon che vuoi aggiungere al tuo Album: ");
            string nomePokemon = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(nomePokemon))
            {
                Console.WriteLine("Il nome del Pokémon non può essere vuoto.");
                return;
            }

            Console.Write("Inserisci l'ID dell'espansione: ");
            if (!int.TryParse(Console.ReadLine(), out int idEspansione))
            {
                Console.WriteLine("ID espansione non valido.");
                return;
            }

            var idCarta = _albumDb.TrovaIdCarta(nomePokemon, idEspansione);

            bool cartaAggiunta = _albumDb.AggiungiCarta(idAlbum, idCarta.Value, nomePokemon, idEspansione);

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

    public void RimuoviCarta()
    {
        try
        {
            Console.Write("Inserisci l'ID dell'album su cui vuoi operare: ");
            if (!int.TryParse(Console.ReadLine(), out int idAlbum))
            {
                Console.WriteLine("ID album non valido.");
                return;
            }

            Console.Write("Inserisci il nome della carta Pokémon che vuoi rimuovere dall'Album: ");
            string nomePokemon = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(nomePokemon))
            {
                Console.WriteLine("Il nome del Pokémon non può essere vuoto.");
                return;
            }

            Console.Write("Inserisci l'ID dell'espansione: ");
            if (!int.TryParse(Console.ReadLine(), out int idEspansione))
            {
                Console.WriteLine("ID espansione non valido.");
                return;
            }

            var idCarta = _albumDb.TrovaIdCarta(nomePokemon, idEspansione);

            bool cartaRimossa = _albumDb.RimuoviCarta(idAlbum, idCarta.Value, nomePokemon, idEspansione);
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
