using MySql.Data.MySqlClient;

public class UtenteProva {
    public void CreaCollezione(MySqlConnection conn, string nomeCollezione, int idUtente)
{

    string query = "INSERT INTO Collezione (nome_collezione, id_utente) VALUES (@nomeCollezione, @idUtente )";

    try
    {
        MySqlCommand cmdCreaColl = new MySqlCommand(query, conn);

        cmdCreaColl.Parameters.AddWithValue("@nomeCollezione", nomeCollezione);
        cmdCreaColl.Parameters.AddWithValue("@idUtente", idUtente);

        cmdCreaColl.ExecuteNonQuery();

        Console.WriteLine("Creazione della collezione avvenuta con successo");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Errore durante la creazione della collezione" + ex.Message);
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

