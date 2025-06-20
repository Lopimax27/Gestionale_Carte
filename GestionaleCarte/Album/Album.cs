using MySql.Data.MySqlClient;

public class Album
{
    // Stringa di connessione al database MySQL
    public MySqlConnection Connection { get; private set; }

    public Album(MySqlConnection conn)
    {
        Connection = conn;
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

            
            {

                // Trova id_carta corrispondente
                string queryCarta = "SELECT id_carta FROM Carta WHERE nome_pokemon = @nome_pokemon AND id_espansione = @id_espansione LIMIT 1";
                int? idCarta = null;
                using (var cmd = new MySqlCommand(queryCarta, Connection))
                {
                    cmd.Parameters.AddWithValue("@nome_pokemon", nomePokemon);
                    cmd.Parameters.AddWithValue("@id_espansione", idEspansione);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        idCarta = Convert.ToInt32(result);
                }

                if (idCarta == null)
                {
                    Console.WriteLine("Carta non trovata nel database.");
                    return;
                }

                // Controlla se la carta è già nell'album
                string queryAlbumCarta = "SELECT COUNT(*) FROM Album_Carta WHERE id_album = @id_album AND id_carta = @id_carta";
                using (var cmd = new MySqlCommand(queryAlbumCarta, Connection))
                {
                    cmd.Parameters.AddWithValue("@id_album", idAlbum);
                    cmd.Parameters.AddWithValue("@id_carta", idCarta.Value);
                    long count = (long)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        Console.WriteLine("La carta è già presente nell'album.");
                        return;
                    }
                }

                // Conferma utente
                Console.WriteLine("Vuoi aggiungere la carta all'album?");
                Console.WriteLine("[1] Sì");
                Console.WriteLine("[2] No");
                string scelta = Console.ReadLine()?.Trim();

                switch (scelta)
                {
                    case "1":
                        try
                        {
                            string insertQuery = "INSERT INTO Album_Carta (id_album, id_carta, is_obtained, is_wanted) VALUES (@id_album, @id_carta, TRUE, FALSE)";
                            using (var cmd = new MySqlCommand(insertQuery, Connection))
                            {
                                cmd.Parameters.AddWithValue("@id_album", idAlbum);
                                cmd.Parameters.AddWithValue("@id_carta", idCarta.Value);
                                cmd.ExecuteNonQuery();
                            }
                            Console.WriteLine($"Carta aggiunta all'Album:\nNome = {nomePokemon}\nID Espansione = {idEspansione}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Errore durante l'inserimento della carta nell'album: " + ex.Message);
                        }
                        break;
                    default:
                        Console.WriteLine("Operazione annullata.");
                        return;
                }
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


            {

                // Trova id_carta corrispondente
                string queryCarta = "SELECT id_carta FROM Carta WHERE nome_pokemon = @nome_pokemon AND id_espansione = @id_espansione LIMIT 1";
                int? idCarta = null;
                using (var cmd = new MySqlCommand(queryCarta, Connection))
                {
                    cmd.Parameters.AddWithValue("@nome_pokemon", nomePokemon);
                    cmd.Parameters.AddWithValue("@id_espansione", idEspansione);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        idCarta = Convert.ToInt32(result);
                }

                if (idCarta == null)
                {
                    Console.WriteLine("Carta non trovata nel database.");
                    return;
                }

                // Verifica se la carta è presente nell'album
                string queryAlbumCarta = "SELECT COUNT(*) FROM Album_Carta WHERE id_album = @id_album AND id_carta = @id_carta";
                using (var cmd = new MySqlCommand(queryAlbumCarta, Connection))
                {
                    cmd.Parameters.AddWithValue("@id_album", idAlbum);
                    cmd.Parameters.AddWithValue("@id_carta", idCarta.Value);
                    long count = (long)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        Console.WriteLine("Carta non presente nell'album.");
                        return;
                    }
                }

                // Conferma utente
                Console.WriteLine("Vuoi rimuovere la carta dall'album?");
                Console.WriteLine("[1] Sì");
                Console.WriteLine("[2] No");
                string scelta = Console.ReadLine()?.Trim();

                switch (scelta)
                {
                    case "1":
                        try
                        {
                            string deleteQuery = "DELETE FROM Album_Carta WHERE id_album = @id_album AND id_carta = @id_carta LIMIT 1";
                            using (var cmd = new MySqlCommand(deleteQuery, Connection))
                            {
                                cmd.Parameters.AddWithValue("@id_album", idAlbum);
                                cmd.Parameters.AddWithValue("@id_carta", idCarta.Value);
                                cmd.ExecuteNonQuery();
                            }
                            Console.WriteLine($"Carta rimossa dall'Album:\nNome = {nomePokemon}\nID Espansione = {idEspansione}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Errore durante la rimozione della carta dall'album: " + ex.Message);
                        }
                        break;
                    default:
                        Console.WriteLine("Operazione annullata.");
                        return;
                }
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
}