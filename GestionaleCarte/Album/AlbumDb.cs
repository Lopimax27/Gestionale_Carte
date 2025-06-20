using MySql.Data.MySqlClient;

public class AlbumDb : IAlbumDb
{
    private readonly MySqlConnection _conn;

    public AlbumDb(MySqlConnection connection)
    {
        _conn = connection;
    }

    public bool AggiungiCarta(int idAlbum, int idCarta, string nomePokemon, int idEspansione)
    {
        try
        {
            CartaGiaPresente(idAlbum, idCarta);

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
                        using (var cmd = new MySqlCommand(insertQuery, _conn))
                        {
                            cmd.Parameters.AddWithValue("@id_album", idAlbum);
                            cmd.Parameters.AddWithValue("@id_carta", idCarta);
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
                    break;

            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

    public bool CartaGiaPresente(int idAlbum, int idCarta)
    {
        // Controlla se la carta è già nell'album
            string queryAlbumCarta = "SELECT COUNT(*) FROM Album_Carta WHERE id_album = @id_album AND id_carta = @id_carta";
        using (var cmd = new MySqlCommand(queryAlbumCarta, _conn))
        {
            cmd.Parameters.AddWithValue("@id_album", idAlbum);
            cmd.Parameters.AddWithValue("@id_carta", idCarta);
            long count = (long)cmd.ExecuteScalar();
            if (count > 0)
            {
                Console.WriteLine("La carta è già presente nell'album.");
                return false;
            }
            else
            {
                return true;
                }

            }
    }

    public bool RimuoviCarta(int idAlbum, int idCarta, string nomePokemon, int idEspansione)
    {
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
                    using (var cmd = new MySqlCommand(deleteQuery, _conn))
                    {
                        cmd.Parameters.AddWithValue("@id_album", idAlbum);
                        cmd.Parameters.AddWithValue("@id_carta", idCarta);
                        cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine($"Carta rimossa dall'Album:\nNome = {nomePokemon}\nID Espansione = {idEspansione}");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore durante la rimozione della carta dall'album: " + ex.Message);
                    return false;
                }
            default:
                Console.WriteLine("Operazione annullata.");
                return false;
        }
        
    }

    public int? TrovaIdCarta(string nomePokemon, int idEspansione)
    {
        string queryCarta = "SELECT id_carta FROM Carta WHERE nome_pokemon = @nome_pokemon AND id_espansione = @id_espansione LIMIT 1";
        int? idCarta = null;
        using (var cmd = new MySqlCommand(queryCarta, _conn))
        {
            cmd.Parameters.AddWithValue("@nome_pokemon", nomePokemon);
            cmd.Parameters.AddWithValue("@id_espansione", idEspansione);
            var result = cmd.ExecuteScalar();
            if (result != null)
                idCarta = Convert.ToInt32(result);
        }

        if (idCarta != null)
        {
            Console.WriteLine($"Carta trovata.");
        }
        else
        {
            Console.WriteLine("Nessuna carta trovata.");
        }
        return idCarta;

    }
}
