using MySql.Data.MySqlClient;

public class Utente
{
    public int UtenteId { get; private set; }
    public MySqlConnection Connection { get; private set; }

    public Utente(MySqlConnection conn, int id)
    {
        Connection = conn;
        UtenteId=id;
    }
    
}