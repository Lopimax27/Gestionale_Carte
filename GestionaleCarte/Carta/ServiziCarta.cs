using static Carta; // Per gli enumeratori

public class ServiziCarta
{

    public readonly ICartaDB _cartaDB;
    private EspansioneDb _espansioneDb;

    public ServiziCarta(ICartaDB cartaDB ,EspansioneDb espansioneDb)
    {
        _cartaDB = cartaDB;
        _espansioneDb = espansioneDb;
    }


    public void AggiungiCartaDB(Utente u)
    {
        if (!u.IsAdmin)
        {
            Console.WriteLine($"Solo gli admin possono aggiungere carte al database!");
            return;
        }

        Console.Write($"Inserisci il nome della carta: ");
        string nomeCarta = Console.ReadLine();


        Console.Write($"Inserisci il tipo della carta: ");
        Tipo tipoCarta;
        while (!Enum.TryParse(Console.ReadLine(), out tipoCarta))
        {
            Console.WriteLine($"Valore non valido. Inserisci un tipo valido: ");
        }


        Console.Write($"Inserisci l'espansione della carta: ");
        string espansioneCarta = Console.ReadLine();
        int espansioneID = 0;
        do
        {
            var e = _espansioneDb.TrovaPerNome(espansioneCarta);

            if (e != null)
            {
                espansioneID = e.Id;
                break;
            }
            else
            {
                Console.WriteLine($"Espansione non valida. Inserisci un espansione valida.");
                return;
            }
        } while (true);


        Console.Write($"Inserisci la rarita della carta: ");
        Rarita raritaCarta;
        while (!Enum.TryParse(Console.ReadLine(), out raritaCarta))
        {
            Console.Write("Valore non valido. Inserisci una rarità valida: ");
        }

        Console.Write($"Inserisci prezzo della carta: ");
        decimal prezzoCarta;
        while (!decimal.TryParse(Console.ReadLine(), out prezzoCarta))
        {
            Console.Write("Valore non valido. Inserisci un numero decimale per il prezzo della carta: ");
        }


        Console.Write($"Inserisci url dell'immagine della carta: ");
        string urlImgCarta = Console.ReadLine();


        bool isReverse;
        do
        {
            Console.Write($"Reverse (true/false): ");
            string reverse = Console.ReadLine().ToLower();

            if (reverse == "true")
            {
                isReverse = true;
                break;
            }
            else if (reverse == "false")
            {
                isReverse = false;
                break;
            }
        }
        while (true);


        bool cartaAggiuntaDB = _cartaDB.InserisciCarta(nomeCarta, tipoCarta, raritaCarta, prezzoCarta, isReverse, espansioneID, urlImgCarta);

        if (cartaAggiuntaDB)
        {
            Console.WriteLine($"Carta {nomeCarta} , {espansioneCarta} creata con successo");
        }
        else
        {
            Console.WriteLine("Errore durante la creazione della carta");
        }
    }

    public void MostraCartePerEspansione()
    {
        Console.Write("Inserisci il nome dell'espansione: ");
        string nomeEspansione = Console.ReadLine()?.Trim();

        var carte = _cartaDB.TrovaPerEspansione(nomeEspansione);

        if (carte.Count == 0)
        {
            Console.WriteLine("Nessuna carta trovata per questa espansione.");
            return;
        }

        Console.WriteLine($"\nCarte trovate per l'espansione '{nomeEspansione}':\n");

        foreach (var carta in carte)
        {
            Console.WriteLine($"ID: {carta.Id} | Nome: {carta.NomePokemon} | Tipo: {carta.TipoCarta} | Rarità: {carta.RaritaCarta} | Prezzo: €{carta.Prezzo}");
        }
    }

    public void RimuoviCartaDB(Utente u)
    {
        if (!u.IsAdmin)
        {
            Console.WriteLine($"Solo gli admin possono rimuovere carte dal database!");
            return;
        }

        Console.Write("Inserisci nome: ");
        string nomeCarta = Console.ReadLine();

        Console.Write($"Inserisci l'espansione della carta: ");
        string espansioneCarta = Console.ReadLine();
        int espansioneID = 0;
        do
        {
            var e = _espansioneDb.TrovaPerNome(espansioneCarta);

            if (e != null)
            {
                espansioneID = e.Id;
                break;
            }
            else
            {
                Console.WriteLine($"Espansione non valida. Inserisci un espansione valida.");
                return;
            }
        } while (true);


        var cartaDaEliminare = _cartaDB.TrovaCarta(nomeCarta, espansioneID);
        bool cartaRimossaDB = _cartaDB.RimuoviCarta(cartaDaEliminare.Id, cartaDaEliminare.NomePokemon, espansioneCarta);

        if (cartaRimossaDB)
        {
            Console.WriteLine($"Carta {nomeCarta} , {espansioneCarta} rimossa con successo");
        }
        else
        {
            Console.WriteLine("Errore durante la rimozione della carta");
        }
    }

}
