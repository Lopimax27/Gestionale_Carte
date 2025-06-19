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
            return;
        }

        Console.Write("Inserisci il nome dell'espansione: ");
        string nome = Console.ReadLine()?.Trim();

        Console.Write("Inserisci anno di uscita (fromato yyyy-mm-gg): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime anno))
        {
            Console.Write("Data non valida");
            return;
        }

        bool riuscito = _espansioneDb.InserisciEspansione(nome, anno);

        if (riuscito)
        {
            Console.WriteLine($"Espansione {nome} creata con successo");
        }
        else
        {
            Console.WriteLine("Errore durante la creazione dell'espansione");
        }
    }
}