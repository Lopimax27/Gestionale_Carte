using MySql.Data.MySqlClient;

/// <summary>
/// Implementazione concreta dell'interfaccia IEspansioneDb per la gestione delle espansioni nel database MySQL
/// </summary>
public class EspansioneDb : IEspansioneDb
{
    /// <summary>
    /// Connessione al database MySQL
    /// </summary>
    private readonly MySqlConnection _conn;

    /// <summary>
    /// Costruttore che inizializza la connessione al database
    /// </summary>
    /// <param name="conn">Connessione MySQL attiva</param>
    public EspansioneDb(MySqlConnection conn)
    {
        _conn = conn;
    }

    /// <summary>
    /// Cerca un'espansione nel database tramite il suo nome
    /// </summary>
    /// <param name="nomeEspansione">Nome dell'espansione da cercare</param>
    /// <returns>Oggetto Espansione se trovato, null altrimenti</returns>
    public Espansione? TrovaPerNome(string nomeEspansione)
    {
        // Query per cercare espansione tramite nome
        using var cmd = new MySqlCommand("SELECT * FROM Espansione WHERE nome_espansione = @nomeEspansione", _conn);
        cmd.Parameters.AddWithValue("@nomeEspansione", nomeEspansione);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            // Creazione oggetto Espansione con dati dal database
            return new Espansione
            {
                Id = reader.GetInt32("id_espansione"),
                Nome = reader.GetString("nome_espansione"),
                Anno = reader.GetDateTime("anno_espansione")
            };
        }
        return null;
    }

    /// <summary>
    /// Inserisce una nuova espansione nel database
    /// </summary>
    /// <param name="nomeEspansione">Nome dell'espansione</param>
    /// <param name="anno">Anno di rilascio dell'espansione</param>
    /// <returns>True se l'inserimento ha successo, False altrimenti</returns>
    public bool InserisciEspansione(string nomeEspansione, DateTime anno)
    {
        try
        {
            using var cmd = new MySqlCommand("INSERT INTO Espansione (nome_espansione, anno_espansione) VALUES (@nome, @anno)", _conn);
            cmd.Parameters.AddWithValue("@nome", nomeEspansione);
            cmd.Parameters.AddWithValue("@anno", anno);
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
    /// Rimuove un'espansione esistente dal database
    /// </summary>
    /// <param name="idEspansione">ID dell'espansione da rimuovere</param>
    /// <returns>True se la rimozione ha successo, False altrimenti</returns>
    public bool RimuoviEspansione(int idEspansione)
    {
        try
        {
            using var cmd = new MySqlCommand("Delete from espansione where id_espansione=@id ", _conn);
            cmd.Parameters.AddWithValue("@id", idEspansione);
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