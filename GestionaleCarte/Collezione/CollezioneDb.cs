using MySql.Data.MySqlClient;

/// <summary>
/// Implementazione concreta dell'interfaccia ICollezioneDb per la gestione delle collezioni nel database MySQL
/// </summary>
public class CollezioneDb : ICollezioneDb
{
    /// <summary>
    /// Connessione al database MySQL
    /// </summary>
    private readonly MySqlConnection connection;

    /// <summary>
    /// Costruttore che inizializza la connessione al database
    /// </summary>
    /// <param name="conn">Connessione MySQL attiva</param>
    public CollezioneDb(MySqlConnection conn)
    {
        connection = conn;
    }

    /// <summary>
    /// Crea un nuovo album all'interno di una collezione esistente
    /// </summary>
    /// <param name="collezioneId">ID della collezione padre</param>
    /// <param name="nomeAlbum">Nome dell'album da creare</param>
    /// <returns>True se la creazione ha successo, False altrimenti</returns>
    public bool CreaAlbum(int collezioneId, string nomeAlbum)
    {
        try
        {
            // Query di inserimento nuovo album nella collezione
            string sqlInsert = "Insert into Album(nome_album,id_collezione) values (@nomeAlbum,@collezioneId)";
            using var cmd = new MySqlCommand(sqlInsert, connection);
            cmd.Parameters.AddWithValue("@nomeAlbum", nomeAlbum);
            cmd.Parameters.AddWithValue("@collezioneId", collezioneId);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Elimina un album da una collezione
    /// </summary>
    /// <param name="collezioneId">ID della collezione</param>
    /// <param name="albumId">ID dell'album da eliminare</param>
    /// <returns>True se l'eliminazione ha successo, False altrimenti</returns>
    public bool EliminaALbum(int collezioneId, int albumId)
    {
        try
        {
            string sqlDelete = "Delete from Album where id_album=@albumId and id_collezione=@collezioneId";
            using var cmd = new MySqlCommand(sqlDelete, connection);
            cmd.Parameters.AddWithValue("@albumId", albumId);
            cmd.Parameters.AddWithValue("@collezioneId", collezioneId);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

    }

    /// <summary>
    /// Trova un album in base al nome e all'ID della collezione
    /// </summary>
    /// <param name="collezioneId">ID della collezione</param>
    /// <param name="nomeAlbum">Nome dell'album da cercare</param>
    /// <returns>Restituisce l'album trovato o null se non esiste</returns>
    public Album? TrovaPerNomeCollezioneId(int collezioneId, string nomeAlbum)
    {
        string sqlFind = "Select id_album,nome_album from Album where id_collezione=@collezioneId and nome_album=@nomeAlbum";
        using var cmd = new MySqlCommand(sqlFind, connection);
        cmd.Parameters.AddWithValue("@collezioneId", collezioneId);
        cmd.Parameters.AddWithValue("@nomeAlbum", nomeAlbum);

        using var rdr = cmd.ExecuteReader();

        if (!rdr.Read())
        {
            return null;
        }

        int albumId = rdr.GetInt32("id_album");
        nomeAlbum = rdr.GetString("nome_album");

        return new Album(albumId, nomeAlbum);
    }

    /// <summary>
    /// Trova una collezione in base all'ID utente
    /// </summary>
    /// <param name="utenteId">ID dell'utente proprietario della collezione</param>
    /// <returns>Restituisce la collezione trovata o null se non esiste</returns>
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

    /// <summary>
    /// Crea una nuova collezione per un utente
    /// </summary>
    /// <param name="utenteId">ID dell'utente proprietario della collezione</param>
    /// <param name="nomeCollezione">Nome della collezione da creare</param>
    /// <returns>True se la creazione ha successo, False altrimenti</returns>
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

    /// <summary>
    /// Visualizza gli album di una collezione
    /// </summary>
    /// <param name="idCollezione">ID della collezione di cui si vogliono visualizzare gli album</param>
    /// <returns>Restituisce una lista di album nella collezione, o una lista vuota se non ce ne sono</returns>
    public List<Album>? VisualizzaCollezione(int idCollezione)
    {
        string query = @"SELECT id_album, nome_album FROM album
                        WHERE id_collezione = @idCollezione";
        List<Album> lista = new List<Album>();
        try
        {
            using var cmdVisualizza = new MySqlCommand(query, connection);
            cmdVisualizza.Parameters.AddWithValue("@idCollezione", idCollezione);
            using var rdrVisualizza = cmdVisualizza.ExecuteReader();

            if (rdrVisualizza.HasRows)
            {
                while (rdrVisualizza.Read())
                {
                    int id = rdrVisualizza.GetInt32("id_album");
                    string nome = rdrVisualizza.GetString("nome_album");
                    var album = new Album(id, nome);
                    lista.Add(album);
                }
                return lista;
            }
            else
            {
                return lista;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la visualizzazione degli album" + ex.Message);
            return lista;
        }
    }
}