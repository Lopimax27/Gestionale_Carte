using MySql.Data.MySqlClient;

public class ServiziAlbum
{
    private readonly MySqlConnection _conn;

    public readonly IAlbumDb _albumDb;

    public ServiziAlbum(MySqlConnection conn, IAlbumDb albumDb)
    {
        _conn = conn;
        _albumDb = albumDb;
    }
    
    public void MostraAlbum(int idUtente)
    {
        string query = @"
            SELECT a.id_album, a.nome_album
            FROM collezione c
            JOIN album a ON c.id_album = a.id_album
            WHERE c.id_utente = @idUtente";

        using var cmd = new MySqlCommand(query, _conn);
        cmd.Parameters.AddWithValue("@idUtente", idUtente);

        using var reader = cmd.ExecuteReader();

        if (!reader.HasRows)
        {
            Console.WriteLine("Non hai nessun album.");
            return;
        }

        Console.WriteLine("Ecco tutti i tuoi album:");
        while (reader.Read())
        {
            int id = reader.GetInt32("id_album");
            string nome = reader.GetString("nome_album");
            Console.WriteLine($"ID Album: {id} | Nome Album: {nome}");
        }
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
