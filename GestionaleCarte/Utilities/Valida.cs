using System.Text.RegularExpressions;
public static class ValidazioneInput
{
    public static bool IsValidEmail(this string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public static bool IsValidPassword(this string password)
    {
        return password.Length >= 8 && password.Length <= 20 && Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)");
    }
}