public class ServiziEspansione
{
    public readonly IEspansioneDb _espansioneDb;

    public ServiziEspansione(IEspansioneDb espansioneDb)
    {
        _espansioneDb = espansioneDb;
    }

    public Espansione? TrovaEspansione()
    {
        Console.Write("Inserisci il nome dell'espansione: ");
        string nome = Console.ReadLine()?.Trim();

        var esp = _espansioneDb.TrovaPerNome(nome);

        if (esp != null)
        {
            Console.WriteLine($"Espansione trovata {esp.Nome} ");
        }
        else
        {
            Console.WriteLine("Nessuna espansione trovata con questo nome");
        }
        return esp;
    }

    public void CreaEspansioneDb(Utente utente)
    {
        if (!utente.IsAdmin)
        {
            Console.WriteLine("Solo gli admin possono creare nuove espansione!");
        }
        
    }
}