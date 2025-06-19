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
}