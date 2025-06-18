using MySql.Data.MySqlClient;

public sealed class Admin
{
    private string _username = "admin";
    public string Username
    {
        get { return _username; }
    }

    private string _password = "admin";
    public string Password
    {
        get { return _password; }
    }


}
