using MySql.Data.MySqlClient;

public class UtenteProva
{
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

    public void VisualizzaCarte(MySqlConnection conn, int idAlbum)
    {
        string query = @"SELECT
                        c.nome_pokemon,
                        c.tipo,
                        c.rarita,
                        c.prezzo,
                        c.is_reverse,
                        ac.is_obtained,
                        ac.is_wanted
                        FROM album_carta ac
                        JOIN carta c ON ac.id_carta = c.id_carta
                        WHERE ac.id_album = @idAlbum";

        try
        {
            MySqlCommand cmdVisualCarte = new MySqlCommand(query, conn);
            cmdVisualCarte.Parameters.AddWithValue("@idAlbum", idAlbum);

            MySqlDataReader rdrVisual = cmdVisualCarte.ExecuteReader();
            if (rdrVisual.HasRows)
            {
                Console.WriteLine("Ecco tutte le carte dell'album selezionato");

                while (rdrVisual.Read())
                {
                    string nome = rdrVisual.GetString("nome_pokemon");
                    string tipo = rdrVisual.GetString("tipo");
                    string rarita = rdrVisual.GetString("rarita");
                    decimal prezzo = rdrVisual.GetDecimal("prezzo");
                    bool isReverse = rdrVisual.GetBoolean("is_reverse");
                    bool isObtained = rdrVisual.GetBoolean("is_obtained");
                    bool isWanted = rdrVisual.GetBoolean("is_wanted");

                    Console.WriteLine($"Nome: {nome} | Tipo: {tipo} | Rarità: {rarita} | Prezzo: €{prezzo} | Reverse: {(isReverse ? "✔" : "✘")} | Posseduta: {(isObtained ? "✔" : "✘")} | Desiderata: {(isWanted ? "✔" : "✘")}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la visualizzazione delle carte" + ex.Message);
        }

    }

}

