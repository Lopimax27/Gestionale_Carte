using MySql.Data.MySqlClient;
public class Utente
{
    public int UtenteId { get; }
    public bool IsAdmin { get; }
    public string Email { get; }
    public string Username { get; }
    public MySqlConnection Connection { get; private set; }

    public Utente(int utenteId, string username, string email, bool isAdmin, MySqlConnection connection)
    {
        UtenteId = utenteId;
        Email = email;
        Username = username;
        IsAdmin = isAdmin;
        Connection = connection;
    }

    public override string ToString()
    {
        return $"UtenteId: {UtenteId} | Username: {Username} | Email: {Email}";
    }
}
