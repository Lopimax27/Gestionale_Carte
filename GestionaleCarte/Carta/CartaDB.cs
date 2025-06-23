using MySql.Data.MySqlClient;

/// <summary>
/// Implementazione concreta dell'interfaccia ICartaDB per la gestione delle carte nel database MySQL
/// </summary>
public class CartaDB : ICartaDB
{
    /// <summary>
    /// Connessione al database MySQL
    /// </summary>
    private readonly MySqlConnection connection;

    /// <summary>
    /// Costruttore che inizializza la connessione al database
    /// </summary>
    /// <param name="conn">Connessione MySQL attiva</param>
    public CartaDB(MySqlConnection conn)
    {
        connection = conn;
    }

    /// <summary>
    /// Inserisce una nuova carta nel database con tutti i suoi attributi
    /// </summary>
    /// <param name="nomePokemon">Nome del Pokémon</param>
    /// <param name="tipoPokemon">Tipo elementale della carta</param>
    /// <param name="raritaCarta">Rarità della carta</param>
    /// <param name="prezzoCarta">Prezzo della carta in euro</param>
    /// <param name="isReverse">Flag per carta Reverse</param>
    /// <param name="idEspansione">ID dell'espansione di appartenenza</param>
    /// <param name="urlCarta">URL dell'immagine della carta</param>
    /// <returns>True if the insertion is successful, False otherwise</returns>
    public bool InserisciCarta(string nomePokemon, Carta.Tipo tipoPokemon, Carta.Rarita raritaCarta, decimal prezzoCarta, bool isReverse, int idEspansione, string urlCarta)
    {
        try
        {
            // Query di inserimento carta con tutti gli attributi
            string sqlAddCarta = "insert into carta(nome_pokemon, tipo, rarita, prezzo, url_img, is_reverse, id_espansione) values (@nome_pokemon, @tipo, @rarita, @prezzo, @url_img, @is_reverse, @id_espansione)";
            using var cmdAddCarta = new MySqlCommand(sqlAddCarta, connection);
            cmdAddCarta.Parameters.AddWithValue("@nome_pokemon", nomePokemon);
            cmdAddCarta.Parameters.AddWithValue("@tipo", tipoPokemon + 1); // +1 per convertire enum base-0 a base-1 nel DB
            cmdAddCarta.Parameters.AddWithValue("@rarita", raritaCarta + 1); // +1 per convertire enum base-0 a base-1 nel DB
            cmdAddCarta.Parameters.AddWithValue("@prezzo", prezzoCarta);
            cmdAddCarta.Parameters.AddWithValue("@url_img", urlCarta);
            cmdAddCarta.Parameters.AddWithValue("@is_reverse", isReverse);
            cmdAddCarta.Parameters.AddWithValue("@id_espansione", idEspansione);
            cmdAddCarta.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

    /// <summary>
    /// Rimuove una carta dal database tramite il suo ID
    /// </summary>
    /// <param name="id_carta">ID della carta da rimuovere</param>
    /// <param name="nomePokemon">Nome del Pokémon (per messaggio di conferma)</param>
    /// <param name="espansioneCarta">Nome dell'espansione (per messaggio di conferma)</param>
    /// <returns>True se la rimozione ha successo, False altrimenti</returns>
    public bool RimuoviCarta(int id_carta, string nomePokemon, string espansioneCarta)
    {
        try
        {
            // Query di eliminazione carta tramite ID
            string sql = "delete from carta where carta.id_carta = @carta_id;";
            using var cmd2 = new MySqlCommand(sql, connection);
            cmd2.Parameters.AddWithValue("@carta_id", id_carta);
            cmd2.ExecuteNonQuery();
            Console.WriteLine($"Carta {nomePokemon}, {espansioneCarta} elimnata.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

    /// <summary>
    /// Trova una carta nel database in base al nome e all'ID dell'espansione
    /// </summary>
    /// <param name="nome">Nome del Pokémon</param>
    /// <param name="espansioneId">ID dell'espansione</param>
    /// <returns>Oggetto Carta se trovata, null altrimenti</returns>
    public Carta? TrovaCarta(string nome, int espansioneId)
    {
        using var cmd = new MySqlCommand("SELECT * FROM Carta WHERE nome_pokemon = @nome AND id_espansione = @espansione LIMIT 1", connection);
        cmd.Parameters.AddWithValue("@nome", nome);
        cmd.Parameters.AddWithValue("@espansione", espansioneId);

        using var rdr = cmd.ExecuteReader();

        if (rdr.Read())
        {
            return new Carta
            {
                Id = rdr.GetInt32("id_carta"),
                NomePokemon = rdr.GetString("nome_pokemon"),
                Prezzo = rdr.GetDecimal("prezzo"),
            };
        }
        return null;
    }

    /// <summary>
    /// Trova tutte le carte appartenenti a una specifica espansione
    /// </summary>
    /// <param name="nomeEspansione">Nome dell'espansione</param>
    /// <returns>Lista di oggetti Carta appartenenti all'espansione</returns>
    public List<Carta> TrovaPerEspansione(string nomeEspansione)
    {
        using var cmd = new MySqlCommand(@"
        SELECT c.id_carta, c.nome_pokemon, c.prezzo, c.tipo, c.rarita
        FROM Carta c
        JOIN Espansione e ON c.id_espansione = e.id_espansione
        WHERE e.nome_espansione = @nomeEspansione", connection);

        cmd.Parameters.AddWithValue("@nomeEspansione", nomeEspansione);

        using var rdr = cmd.ExecuteReader();

        var carte = new List<Carta>();

        while (rdr.Read())
        {
            var carta = new Carta
            {
                Id = rdr.GetInt32("id_carta"),
                NomePokemon = rdr.GetString("nome_pokemon"),
                Prezzo = rdr.GetDecimal("prezzo"),
                TipoCarta = Enum.TryParse<Carta.Tipo>(rdr.GetString("tipo"), out var tipo) ? tipo : throw new Exception("Tipo non valido"),
                RaritaCarta = Enum.TryParse<Carta.Rarita>(rdr.GetString("rarita").Replace(" ", "_"), out var rarita) ? rarita : throw new Exception("Rarità non valida")
            };
            carte.Add(carta);
        }
        return carte;
    }
}