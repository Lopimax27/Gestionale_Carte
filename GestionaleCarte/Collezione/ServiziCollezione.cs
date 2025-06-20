using MySql.Data.MySqlClient;

public class ServiziCollezione
{
    private readonly ICollezioneDb _collezioneDb;

    public ServiziCollezione(ICollezioneDb collezioneDb)
    {
        _collezioneDb = collezioneDb;
    }

    public Collezione OttieniCreaCollezione(int utenteId)
    {
        var collezione = _collezioneDb.TrovaPerUtenteId(utenteId);

        if (collezione == null)
        {
            Console.Write("Ops! Ci siamo resi conto che non hai ancora una collezione!\nInserisci il nome per la tua collezione:");
            string nomeCollezione = Console.ReadLine();
            bool fatto = _collezioneDb.CreaCollezione(utenteId, nomeCollezione);
            collezione = _collezioneDb.TrovaPerUtenteId(utenteId)!;//Punto esclamativo dice fidati capo non è nullo
            if (fatto)
            {
                Console.WriteLine("Collezione creata con successo!");
            }
            else
            {
                Console.WriteLine("Non siamo riusciti a creare la collezione,verifica di aver inserito un nome valido");
            }
            return collezione;
        }
        else
        {
            Console.WriteLine("Abbiamo trovato la tua collezione");
            return collezione;
        }
    }

    public Album? CreaAlbum(int utenteId)
    {
        Console.Write("Inserisci il nome del tuo nuovo album: ");
        string nomeAlbum = Console.ReadLine();

        if (string.IsNullOrEmpty(nomeAlbum))
        {
            Console.WriteLine("Nome Album vuoto. Riprovare");
            return null;
        }

        var collezione = OttieniCreaCollezione(utenteId);
        bool creato=_collezioneDb.CreaAlbum(collezione.UtenteId, collezione.Id, nomeAlbum);

        if (creato)
        {
            Console.WriteLine("Album creato con successo");
        }
        else
        {
            Console.WriteLine("Errore nella creazione dell'Album riprovare");
        }

        var album = _collezioneDb.TrovaPerNomeCollezioneId(collezione.Id, nomeAlbum);

        return album;
    }
}