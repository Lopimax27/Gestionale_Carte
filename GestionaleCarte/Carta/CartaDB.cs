using MySql.Data.MySqlClient;

public class CartaDB : ICartaDB
{
    private readonly MySqlConnection connection;

    public CartaDB(MySqlConnection conn)
    {
        connection = conn;
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