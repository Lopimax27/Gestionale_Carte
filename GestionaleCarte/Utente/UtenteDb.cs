using MySql.Data.MySqlClient;

public class UtenteDb : IUtenteDb
{
    private readonly MySqlConnection _conn;

    public UtenteDb(MySqlConnection connection)
    {
        _conn = connection;
    }

    public bool Esiste(string username, string email)
    {
        string sqlUtente = "SELECT id_utente FROM utente WHERE email=@email OR username=@username";

        using var cmdUtente = new MySqlCommand(sqlUtente, _conn);
        cmdUtente.Parameters.AddWithValue("@email", email);
        cmdUtente.Parameters.AddWithValue("@username", username);

        using var rdrUser = cmdUtente.ExecuteReader();
        return rdrUser.Read();
    }

    public bool Inserisci(string username, string email, string passwordHash)
    {
        try
        {
            string sqlInsert = "INSERT INTO utente (username, email, password_hash) VALUES (@username, @email, @password_hash)";
            using var cmd = new MySqlCommand(sqlInsert, _conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password_hash", passwordHash);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

    public Utente? TrovaPerUsername(string username)
    {
        string sqlFind = "SELECT id_utente, is_admin FROM utente WHERE username = @username";
        using var cmd = new MySqlCommand(sqlFind, _conn);
        cmd.Parameters.AddWithValue("@username", username);

        using var rdr = cmd.ExecuteReader();
        if (rdr.Read())
        {
            int id = rdr.GetInt32("id_utente");
            bool isAdmin = rdr.GetBoolean("is_admin");
            return new Utente(id, isAdmin, _conn);
        }

        return null;
    }

    public string? TrovaPasswordHash(int utenteId)
    {
        string sqlPassword = "SELECT password_hash FROM utente WHERE id_utente = @utenteId";
        using var cmd = new MySqlCommand(sqlPassword, _conn);
        cmd.Parameters.AddWithValue("@utenteId", utenteId);

        using var rdr = cmd.ExecuteReader();
        if (!rdr.Read())
        {
            return null;
        }

        return rdr.GetString("password_hash");
    }
}
