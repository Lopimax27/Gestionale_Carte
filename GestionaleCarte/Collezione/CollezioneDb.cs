using MySql.Data.MySqlClient;

public class CollezioneDb : ICollezioneDb
{
    private readonly MySqlConnection connection;
    public CollezioneDb(MySqlConnection conn)
    {
        connection = conn;
    }

    public Collezione? TrovaPerUtenteId(int utenteId)
    {
        string sqlFind = "Select id_collezione,id_utente from Collezione where id_utente=@utenteId";
        using var cmd = new MySqlCommand(sqlFind, connection);
        cmd.Parameters.AddWithValue("@utenteId", utenteId);

        using var rdr = cmd.ExecuteReader();

        if (rdr.Read())
        {
            return new Collezione { Id = rdr.GetInt32("id_collezione"), UtenteId = rdr.GetInt32("id_utente") };
        }
        return null;
    }

    public bool CreaCollezione(int utenteId)
    {
        try
        {
            string sqlInsert = "Insert into Collezione (id_utente) values (@utenteId)";
            using var cmd = new MySqlCommand(sqlInsert, connection);
            cmd.Parameters.AddWithValue("@utenteId", utenteId);
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