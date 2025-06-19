using MySql.Data.MySqlClient;

public class Utente
{
    public int UtenteId { get; private set; }
    public MySqlConnection Connection { get; private set; }

    public Utente(MySqlConnection conn, int id)
    {
        Connection = conn;
        UtenteId = id;
    }
    
    public void CreaAlbum(MySqlConnection conn)
    {
        Console.WriteLine("Inserisci il nome dell'album: ");
        string nomeAlbum = Console.ReadLine();

        string query = @"INSERT INTO album (nome_album) VALUES (@nomeAlbum)";
        MySqlCommand cmdAddAlbum = new MySqlCommand(query, conn);
        cmdAddAlbum.Parameters.AddWithValue("@nome_album", nomeAlbum);
        cmdAddAlbum.ExecuteNonQuery();
    }

    public void EliminaAlbum(MySqlConnection conn)
    {
        Console.WriteLine("Inserisci il nome dell'album da cercare:");
        string nomeAlbum = Console.ReadLine();

        string queryCerca = "SELECT id_album, nome_album FROM album WHERE nome_album = @nomeAlbum";
        using (MySqlCommand cmdCerca = new MySqlCommand(queryCerca, conn))
        {
            cmdCerca.Parameters.AddWithValue("@nomeAlbum", nomeAlbum);

            using (MySqlDataReader reader = cmdCerca.ExecuteReader())
            {
                List<int> albumTrovati = new List<int>();

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
                    Console.WriteLine("Nessun album trovato con quel nome.");
                    return;
                }

                reader.Close(); // Chiudere il reader prima di eseguire un nuovo comando

                Console.WriteLine("\nInserisci l'ID dell'album da eliminare:");
                if (int.TryParse(Console.ReadLine(), out int idDaEliminare))
                {
                    if (!albumTrovati.Contains(idDaEliminare))
                    {
                        Console.WriteLine("ID non valido. L'ID inserito non corrisponde a nessun album elencato.");
                        return;
                    }

                    string queryElimina = "DELETE FROM album WHERE id_album = @idAlbum";
                    using (MySqlCommand cmdElimina = new MySqlCommand(queryElimina, conn))
                    {
                        cmdElimina.Parameters.AddWithValue("@idAlbum", idDaEliminare);

                        int righe = cmdElimina.ExecuteNonQuery();
                        if (righe > 0)
                            Console.WriteLine("Album eliminato con successo.");
                        else
                            Console.WriteLine("Errore durante l'eliminazione.");
                    }
                }
                else
                {
                    Console.WriteLine("Input non valido. Inserisci un ID numerico.");
                }
            }
        }
    }

    public void MostraAlbum(MySqlConnection conn, int idUtente)
    {
        string queryMostra =
                        @"SELECT a.id_album, a.nome_album
                        FROM collezione c
                        JOIN album a ON c.id_album = a.id_album
                        WHERE c.id_utente = @idUtente";

        MySqlCommand cmdMostra = new MySqlCommand(queryMostra, conn);
        cmdMostra.Parameters.AddWithValue("@id_utente", idUtente);

        MySqlDataReader rdrMostra = cmdMostra.ExecuteReader();

        if (rdrMostra.HasRows)
        {
            Console.WriteLine($"Ecco tutti i tuoi album\n");
            while (rdrMostra.Read())
            {
                int id = rdrMostra.GetInt32("id_album");    // GetInt32("id_album") legge il valore intero dalla colonna "id_album" del risultato della query.

                string nome = rdrMostra.GetString("nome_album");    // GetString("nome_album") legge la stringa dalla colonna "nome_album" del risultato della query.
                Console.WriteLine($"ID Album: {id} | Nome Album {nome}");
            }
        }
        else
        {
            Console.WriteLine($"Non hai nessun album");
            return;
        }
    }

    public void AggiungiCarta(MySqlConnection conn)
    {

    }

}