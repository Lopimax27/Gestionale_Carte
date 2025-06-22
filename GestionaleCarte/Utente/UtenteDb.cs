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
        try
        {

            string sqlUtente = "SELECT id_utente FROM utente WHERE email=@email OR username=@username";

            using var cmdUtente = new MySqlCommand(sqlUtente, _conn);
            cmdUtente.Parameters.AddWithValue("@email", email);
            cmdUtente.Parameters.AddWithValue("@username", username);

            using var rdrUser = cmdUtente.ExecuteReader();
            return rdrUser.Read();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
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
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public Utente? TrovaPerUsername(string username)
    {
        try
        {
            string sqlFind = "SELECT id_utente, is_admin,username,email FROM utente WHERE username = @username";
            using var cmd = new MySqlCommand(sqlFind, _conn);
            cmd.Parameters.AddWithValue("@username", username);

            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                int id = rdr.GetInt32("id_utente");
                string user = rdr.GetString("username");
                string email = rdr.GetString("email");
                bool isAdmin = rdr.GetBoolean("is_admin");
                return new Utente(id, user, email, isAdmin, _conn);
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public string? TrovaPasswordHash(int utenteId)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }


    }

    public List<Utente>? TuttiUtenti()
    {
        try
        {
            List<Utente> lista = new List<Utente>();

            using var cmd = new MySqlCommand("SELECT id_utente, is_admin,username,email FROM utente WHERE is_admin = false", _conn);
            using var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int id = rdr.GetInt32("id_utente");
                string user = rdr.GetString("username");
                string email = rdr.GetString("email");
                bool isAdmin = rdr.GetBoolean("is_admin");
                lista.Add(new Utente(id, user, email, isAdmin, _conn));
            }
            return lista;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}
