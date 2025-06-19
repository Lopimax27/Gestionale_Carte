using MySql.Data.MySqlClient;

public class EspansioneDb : IEspansioneDb
{
    private readonly MySqlConnection _conn;

    public EspansioneDb(MySqlConnection conn)
    {
        _conn = conn;
    }

    public Espansione? TrovaPerNome(string nomeEspansione)
    {
        using var cmd = new MySqlCommand("SELECT * FROM Espansione WHERE nome_espansione = @nome ", _conn);
        cmd.Parameters.AddWithValue("@nome", nomeEspansione);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Espansione
            {
                Id = reader.GetInt32("id_espansione"),
                Nome = reader.GetString("nome_espansione"),
                Anno = reader.GetDateTime("anno_espansione")
            };
        }
        return null;
    }

    public bool InserisciEspansione(string nomeEspansione, DateTime anno)
    {
        try
        {
            using var cmd = new MySqlCommand("INSERT INTO Espansione (nome_espansione, anno_espansione) VALUES (@nome, @anno)", _conn);
            cmd.Parameters.AddWithValue("@nome", nomeEspansione);
            cmd.Parameters.AddWithValue("@anno", anno);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }
}