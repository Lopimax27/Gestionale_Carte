using MySql.Data.MySqlClient;

public class UtenteDb : IUtenteDb
{
    private readonly MySqlConnection conn;

    public UtenteDb(MySqlConnection connection)
    {
        conn = connection;
    }

    public bool Esiste(string username, string email)
    {
        string sqlUtente = "Select id_utente from Utente where email=@email or username=@username;";

        using var cmdUtente = new MySqlCommand(sqlUtente, conn);
        cmdUtente.Parameters.AddWithValue("@email", email);
        cmdUtente.Parameters.AddWithValue("@username", username);

        using var rdrUser = cmdUtente.ExecuteReader();

        if (rdrUser.Read())
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool Inserisci(string username, string email, string passwordHash)
    {
        try
        {
            string sqlInsert = "INSERT INTO Utente (username, email, password_hash) VALUES (@username, @email, @password_hash)";
            using var cmd = new MySqlCommand(sqlInsert, conn);
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
        string sqlFind = "Select id_utente from Utente where username=@username";
        using var cmd = new MySqlCommand(sqlFind, conn);
        using var rdr = cmd.ExecuteReader();

        if (rdr.Read())
        {
            int id = rdr.GetInt32("id_utente");
            bool isAdmin = rdr.GetBoolean("is_admin");
            if (isAdmin)
            {
                return new Utente(conn, id, true);
            }
            else
            { 
                return new Utente(conn, id, false);
            }
        }
        return null;
    }
}