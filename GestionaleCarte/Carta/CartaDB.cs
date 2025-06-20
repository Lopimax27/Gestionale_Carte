using System.Security.Policy;
using MySql.Data.MySqlClient;

public class CartaDB : ICartaDB
{
    private readonly MySqlConnection connection;

    public CartaDB(MySqlConnection conn)
    {
        connection = conn;
    }

    public bool InserisciCarta(string nomePokemon, Carta.Tipo tipoPokemon, Carta.Rarita raritaCarta, decimal prezzoCarta, bool isReverse, int idEspansione, string urlCarta)
    {
        try
        {
            string sqlAddCarta = "insert into carta(nome_pokemon, tipo, rarita, prezzo, url_img, is_reverse, id_espansione) values (@nome_pokemon, @tipo, @rarita, @prezzo, @url_img, @is_reverse, @id_espansione)";
            using var cmdAddCarta = new MySqlCommand(sqlAddCarta, connection);
            cmdAddCarta.Parameters.AddWithValue("@nome_pokemon", nomePokemon);
            cmdAddCarta.Parameters.AddWithValue("@tipo", tipoPokemon + 1);
            cmdAddCarta.Parameters.AddWithValue("@rarita", raritaCarta + 1);
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

    public bool RimuoviCarta(int id_carta, string nomePokemon, string espansioneCarta)
    {
        try
        {
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