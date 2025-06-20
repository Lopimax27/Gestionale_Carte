using MySql.Data.MySqlClient;
public class Utente
{
    public int UtenteId { get; }
    public bool IsAdmin { get; }
    public MySqlConnection Connection { get; private set; }

    public Utente(int utenteId, bool isAdmin, MySqlConnection connection)
    {
        UtenteId = utenteId;
        IsAdmin = isAdmin;
        Connection = connection;
    }
}
