using System.Text.RegularExpressions;

/// <summary>
/// Classe statica per la validazione degli input dell'utente
/// </summary>
public static class ValidazioneInput
{
    /// <summary>
    /// Estensione che valida il formato di un indirizzo email
    /// </summary>
    /// <param name="email">Stringa email da validare</param>
    /// <returns>True se l'email è valida, False altrimenti</returns>
    public static bool IsValidEmail(this string email)
    {
        // Regex per validare il formato email: nome@dominio.estensione
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    /// <summary>
    /// Estensione che valida la forza di una password
    /// </summary>
    /// <param name="password">Password da validare</param>
    /// <returns>True se la password rispetta i criteri di sicurezza, False altrimenti</returns>
    public static bool IsValidPassword(this string password)
    {
        // Validazione password: 8-20 caratteri, almeno una minuscola, una maiuscola e un numero
        return password.Length >= 8 && password.Length <= 20 && Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)");
    }
}