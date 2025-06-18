using MySql.Data.MySqlClient;

public class Utente
{
    private MySqlConnection _connection;
    private int _utenteId;

    public Utente(MySqlConnection connection, int utenteId)
    {
        _connection = connection;
        _utenteId = utenteId;
    }
    
}