using MySql.Data.MySqlClient;

/// <summary>
/// Implementazione concreta dell'interfaccia IAlbumDb per la gestione degli album nel database MySQL
/// </summary>
public class AlbumDb : IAlbumDb
{
    /// <summary>
    /// Connessione al database MySQL
    /// </summary>
    private readonly MySqlConnection _conn;

    /// <summary>
    /// Costruttore che inizializza la connessione al database
    /// </summary>
    /// <param name="connection">Connessione MySQL attiva</param>
    public AlbumDb(MySqlConnection connection)
    {
        _conn = connection;
    }

    /// <summary>
    /// Aggiunge una carta a un album con conferma dell'utente
    /// </summary>
    /// <param name="idAlbum">ID dell'album di destinazione</param>
    /// <param name="idCarta">ID della carta da aggiungere</param>
    /// <param name="nomePokemon">Nome del Pokémon (per messaggi)</param>
    /// <param name="nomeEspansione">Nome dell'espansione (per messaggi)</param>
    /// <param name="isObtained">Flag che indica se la carta è posseduta</param>
    /// <param name="isWanted">Flag che indica se la carta è desiderata</param>
    /// <returns>True se l'aggiunta ha successo, False altrimenti</returns>
    public bool AggiungiCarta(int idAlbum, int idCarta, string nomePokemon, string nomeEspansione, bool isObtained, bool isWanted)
    {
        try
        {
            // Verifica se la carta è già presente nell'album
            CartaGiaPresente(idAlbum, idCarta);

            // Richiesta conferma utente
            Console.WriteLine("Vuoi aggiungere la carta all'album?");
            Console.WriteLine("[1] Sì");
            Console.WriteLine("[2] No");
            string scelta = Console.ReadLine()?.Trim();

            switch (scelta)
            {
                case "1":
                    try
                    {
                        // Query di inserimento carta nell'album con stati
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

    /// <summary>
    /// Controlla se una carta è già presente in un album
    /// </summary>
    /// <param name="idAlbum">ID dell'album</param>
    /// <param name="idCarta">ID della carta</param>
    /// <returns>True se la carta è presente, False altrimenti</returns>
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

    /// <summary>
    /// Rimuove una carta da un album con conferma dell'utente
    /// </summary>
    /// <param name="idAlbum">ID dell'album di origine</param>
    /// <param name="idCarta">ID della carta da rimuovere</param>
    /// <param name="nomePokemon">Nome del Pokémon (per messaggi)</param>
    /// <param name="nomeEspansione">Nome dell'espansione (per messaggi)</param>
    /// <returns>True se la rimozione ha successo, False altrimenti</returns>
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

    /// <summary>
    /// Trova l'ID di una carta in base al nome del Pokémon e all'ID dell'espansione
    /// </summary>
    /// <param name="nomePokemon">Nome del Pokémon</param>
    /// <param name="idEspansione">ID dell'espansione</param>
    /// <returns>ID della carta se trovata, null altrimenti</returns>
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

    /// <summary>
    /// Restituisce la lista delle carte in un album
    /// </summary>
    /// <param name="idAlbum">ID dell'album</param>
    /// <returns>Lista di oggetti Carta presenti nell'album</returns>
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
                        RaritaCarta = (Carta.Rarita)Enum.Parse(typeof(Carta.Rarita), rdrVisual.GetString("rarita").Replace(" ", "")),
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

    /// <summary>
    /// Calcola il valore totale di un album in base ai prezzi delle carte
    /// </summary>
    /// <param name="idAlbum">ID dell'album</param>
    /// <returns>Valore totale dell'album</returns>
    public decimal ValoreAlbum(int idAlbum)
    {
        decimal valore = 0;
        string sqlSum = @"Select sum(prezzo) from carta join album_carta on carta.id_carta=album_carta.id_carta 
        where album_carta.id_album=@idAlbum
        group by album_carta.id_album";

        try
        {
            using var cmd = new MySqlCommand(sqlSum, _conn);
            cmd.Parameters.AddWithValue("@idAlbum", idAlbum);
            var result = cmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                valore = Convert.ToDecimal(result);
            }

            return valore;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante il calcolo del valore" + ex.Message);
            return valore;
        }
    }
}
