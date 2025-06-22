

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
            string? nomeCollezione = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nomeCollezione))
            {
                Console.WriteLine("Nome collezione non valido!");
                throw new InvalidOperationException("Nome collezione richiesto");
            }

            bool fatto = _collezioneDb.CreaCollezione(utenteId, nomeCollezione);
            collezione = _collezioneDb.TrovaPerUtenteId(utenteId)!;//Punto esclamativo dice fidati capo non è nullo

            if (fatto)
            {
                Console.WriteLine("Collezione creata con successo!");
            }
            else
            {
                Console.WriteLine("Non siamo riusciti a creare la collezione,verifica di aver inserito un nome valido");
                throw new InvalidOperationException("Creazione collezione fallita");
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
        string? nomeAlbum = Console.ReadLine();

        if (string.IsNullOrEmpty(nomeAlbum))
        {
            Console.WriteLine("Nome Album vuoto. Riprovare");
            return null;
        }

        var collezione = OttieniCreaCollezione(utenteId);
        bool creato = _collezioneDb.CreaAlbum(collezione.Id, nomeAlbum);

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

    public void EliminaALbum(int utenteId)
    {
        var collezione = OttieniCreaCollezione(utenteId);

        Console.Write("Inserisci il nome del album da eliminare: ");
        string? nomeAlbum = Console.ReadLine();

        if (string.IsNullOrEmpty(nomeAlbum))
        {
            Console.WriteLine("Nome Album vuoto. Riprovare");
            return;
        }

        var album = _collezioneDb.TrovaPerNomeCollezioneId(collezione.Id, nomeAlbum);

        if (album == null)
        {
            Console.WriteLine("Non è stato trovato un album con questo nome");
            return;
        }

        bool eliminato = _collezioneDb.EliminaALbum(collezione.Id, album.Id);

        if (eliminato)
        {
            Console.WriteLine("Album eliminato con successo");
        }
        else
        {
            Console.WriteLine("Eliminazione fallita riprovare");
        }
        
    }

    public void VisualizzaAlbum(int utenteId)
    {
        var collezione = OttieniCreaCollezione(utenteId);

        List<Album>? albums = _collezioneDb.VisualizzaCollezione(collezione.Id);

        if (albums == null || albums.Count == 0)
        {
            Console.WriteLine("Non hai ancora degli album! Torna al menu e creane uno per visualizzarlo.");
            return;
        }

        foreach (Album a in albums)
        {
            Console.WriteLine(a);
        }
    }
}