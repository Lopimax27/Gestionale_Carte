using MySql.Data.MySqlClient;

public class ServiziAlbum
{
    private readonly MySqlConnection _conn;

    public ServiziAlbum(MySqlConnection conn)
    {
        _conn = conn;
    }

    public void CreaAlbum(string nomeAlbum)
    {
        string query = "INSERT INTO album (nome_album) VALUES (@nomeAlbum)";
        using var cmd = new MySqlCommand(query, _conn);
        cmd.Parameters.AddWithValue("@nomeAlbum", nomeAlbum);
        cmd.ExecuteNonQuery();
    }

    public void EliminaAlbum(string nomeAlbum)
    {
        string queryCerca = "SELECT id_album, nome_album FROM album WHERE nome_album = @nomeAlbum";
        using var cmdCerca = new MySqlCommand(queryCerca, _conn);
        cmdCerca.Parameters.AddWithValue("@nomeAlbum", nomeAlbum);

        using var reader = cmdCerca.ExecuteReader();
        var albumTrovati = new List<int>();

        Console.WriteLine("\nAlbum trovati:");
        while (reader.Read())
        {
            int id = reader.GetInt32("id_album");
            string nome = reader.GetString("nome_album");
            Console.WriteLine($"ID Album: {id} | Nome Album: {nome}");
            albumTrovati.Add(id);
        }

        if (albumTrovati.Count == 0)
        {
            Console.WriteLine("Nessun album trovato.");
            return;
        }

        reader.Close();

        Console.WriteLine("\nInserisci l'ID dell'album da eliminare:");
        if (!int.TryParse(Console.ReadLine(), out int idDaEliminare) || !albumTrovati.Contains(idDaEliminare))
        {
            Console.WriteLine("ID non valido.");
            return;
        }

        string queryElimina = "DELETE FROM album WHERE id_album = @idAlbum";
        using var cmdElimina = new MySqlCommand(queryElimina, _conn);
        cmdElimina.Parameters.AddWithValue("@idAlbum", idDaEliminare);

        int righe = cmdElimina.ExecuteNonQuery();
        Console.WriteLine(righe > 0 ? "Album eliminato con successo." : "Errore durante l'eliminazione.");
    }

    public void MostraAlbum(int idUtente)
    {
        string query = @"
            SELECT a.id_album, a.nome_album
            FROM collezione c
            JOIN album a ON c.id_album = a.id_album
            WHERE c.id_utente = @idUtente";

        using var cmd = new MySqlCommand(query, _conn);
        cmd.Parameters.AddWithValue("@idUtente", idUtente);

        using var reader = cmd.ExecuteReader();

        if (!reader.HasRows)
        {
            Console.WriteLine("Non hai nessun album.");
            return;
        }

        Console.WriteLine("Ecco tutti i tuoi album:");
        while (reader.Read())
        {
            int id = reader.GetInt32("id_album");
            string nome = reader.GetString("nome_album");
            Console.WriteLine($"ID Album: {id} | Nome Album: {nome}");
        }
    }
}
