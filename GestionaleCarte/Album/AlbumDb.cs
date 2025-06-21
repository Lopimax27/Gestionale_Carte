using MySql.Data.MySqlClient;


public class AlbumDb : IAlbumDb
{
    private readonly MySqlConnection _conn;

    public AlbumDb(MySqlConnection connection)
    {
        _conn = connection;
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
    public bool AggiungiCarta(int idAlbum, int idCarta, string nomePokemon, string nomeEspansione,bool isObtained, bool isWanted)
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
                        string insertQuery = "INSERT INTO Album_Carta (id_album, id_carta, is_obtained, is_wanted) VALUES (@id_album, @id_carta, @isObtained, @isWanted)";
                        using (var cmd = new MySqlCommand(insertQuery, _conn))
                        {
                            cmd.Parameters.AddWithValue("@id_album", idAlbum);
                            cmd.Parameters.AddWithValue("@id_carta", idCarta);
                            cmd.Parameters.AddWithValue("@isObtained", isObtained);
                            cmd.Parameters.AddWithValue("@isWanted", isWanted);
                            cmd.ExecuteNonQuery();
                        }
                        Console.WriteLine($"Carta aggiunta all'Album:\nNome = {nomePokemon}\nNome Espansione = {nomeEspansione}");
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

    public bool RimuoviCarta(int idAlbum, int idCarta, string nomePokemon, string nomeEspansione)
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
                    Console.WriteLine($"Carta rimossa dall'Album:\nNome = {nomePokemon}\nNome Espansione = {nomeEspansione}");
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

    public List<Carta> ListaCarte(int idAlbum)
    {
        List<Carta> listacarte = new List<Carta>();

        string query = @"SELECT
                        c.nome_pokemon,
                        c.tipo,
                        c.rarita,
                        c.prezzo,
                        c.is_reverse,
                        ac.is_obtained,
                        ac.is_wanted
                        FROM album_carta ac
                        JOIN carta c ON ac.id_carta = c.id_carta
                        WHERE ac.id_album = @idAlbum";

        try
        {
            using var cmdVisualCarte = new MySqlCommand(query, _conn);
            cmdVisualCarte.Parameters.AddWithValue("@idAlbum", idAlbum);

            using var rdrVisual = cmdVisualCarte.ExecuteReader();
            if (rdrVisual.HasRows)
            {
                while (rdrVisual.Read())
                {

                    Carta carta = new Carta
                    {
                        NomePokemon = rdrVisual.GetString("nome_pokemon"),
                        TipoCarta = (Carta.Tipo)Enum.Parse(typeof(Carta.Tipo), rdrVisual.GetString("tipo")),
                        RaritaCarta = (Carta.Rarita)Enum.Parse(typeof(Carta.Rarita), rdrVisual.GetString("rarita").Replace(" ","")),
                        Prezzo = rdrVisual.GetDecimal("prezzo"),
                        IsReverse = rdrVisual.GetBoolean("is_reverse"),
                        IsObtained = rdrVisual.GetBoolean("is_obtained"),
                        IsWanted = rdrVisual.GetBoolean("is_wanted"),
                    };

                    listacarte.Add(carta);
                }
                return listacarte;
            }
            else
            {
                Console.WriteLine("Carte non trovate, Album vuoto");
                return listacarte;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la visualizzazione delle carte" + ex.Message);
            return listacarte;
        }

    }
}
