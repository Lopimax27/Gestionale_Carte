using MySql.Data.MySqlClient;

public class CollezioneDb : ICollezioneDb
{
    private readonly MySqlConnection connection;
    public CollezioneDb(MySqlConnection conn)
    {
        connection = conn;
    }

    public bool CreaAlbum(int utenteId,int collezioneId, string nomeAlbum)
    {
        try
        {
            string sqlInsert = "Insert into Album(nome_album,id_collezione) values (@nomeAlbum,@collezioneId)";
            using var cmd = new MySqlCommand(sqlInsert, connection);
            cmd.Parameters.AddWithValue("@nomeAlbum", nomeAlbum);
            cmd.Parameters.AddWithValue("@collezioneId", collezioneId);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }

    }

    public Album? TrovaPerNomeCollezioneId(int collezioneId,string nomeAlbum)
    {
        string sqlFind = "Select id_album,nome_album from Album where id_collezione=@collezioneId and nome_album=@nomeAlbum";
        using var cmd = new MySqlCommand(sqlFind, connection);
        cmd.Parameters.AddWithValue("@collezioneId", collezioneId);
        cmd.Parameters.AddWithValue("@nomeAlbum", nomeAlbum );

        using var rdr = cmd.ExecuteReader();

        if (!rdr.Read())
        {
            return null;
        }

        int albumId = rdr.GetInt32("id_album");
        nomeAlbum = rdr.GetString("nome_album");

        return new Album
        {
            Id = albumId,
            Nome = nomeAlbum
        };
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

    public bool CreaCollezione(int utenteId, string nomeCollezione)
    {
        try
        {
            string sqlInsert = "Insert into Collezione (id_utente,nome_collezione) values (@utenteId,@nomeCollezione)";
            using var cmd = new MySqlCommand(sqlInsert, connection);
            cmd.Parameters.AddWithValue("@utenteId", utenteId);
            cmd.Parameters.AddWithValue("@nomeCollezione", nomeCollezione);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }
    
    public void VisualizzaCollezione(MySqlConnection conn, int idCollezione)
    {
        string query = @"SELECT id_album, nome_album FROM album
                        WHERE id_collezione = @idCollezione";

        try
        {
            MySqlCommand cmdVisualizza = new MySqlCommand(query, conn);
            cmdVisualizza.Parameters.AddWithValue("@idCollezione", idCollezione);
            MySqlDataReader rdrVisualizza = cmdVisualizza.ExecuteReader();

            if (rdrVisualizza.HasRows)
            {
                Console.WriteLine("Ecco tutti gli album nella collezione");

                while (rdrVisualizza.Read())
                {
                    int id = rdrVisualizza.GetInt32("id_album");
                    string nome = rdrVisualizza.GetString("nome_album");

                    Console.WriteLine($"ID Album: {id} | Nome Album {nome}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la visualizzazione degli album" + ex.Message);
        }
    }
}