using MySql.Data.MySqlClient;
using static Carta; // Per gli enumeratori

public class ServiziCarta
{
    private readonly MySqlConnection _conn;
    private EspansioneDb _espansioneDb;
    private ICartaDB _cartaDb;

    public ServiziCarta(MySqlConnection conn, EspansioneDb espansioneDb, ICartaDB cartaDb)
    {
        _conn = conn;
        _espansioneDb = espansioneDb;
        _cartaDb = cartaDb;
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



        string sqlAddCarta = "insert into carta(nome_pokemon, tipo, rarita, prezzo, url_img, is_reverse, id_espansione) values (@nome_pokemon, @tipo, @rarita, @prezzo, @url_img, @is_reverse, @id_espansione)";
        using var cmdAddCarta = new MySqlCommand(sqlAddCarta, _conn);
        cmdAddCarta.Parameters.AddWithValue("@nome_pokemon", nomeCarta);
        cmdAddCarta.Parameters.AddWithValue("@tipo", tipoCarta + 1);
        cmdAddCarta.Parameters.AddWithValue("@rarita", raritaCarta + 1);
        cmdAddCarta.Parameters.AddWithValue("@prezzo", prezzoCarta);
        cmdAddCarta.Parameters.AddWithValue("@url_img", urlImgCarta);
        cmdAddCarta.Parameters.AddWithValue("@is_reverse", isReverse);
        cmdAddCarta.Parameters.AddWithValue("@id_espansione", espansioneID);
        cmdAddCarta.ExecuteNonQuery();
    }

    public void MostraCartePerEspansione()
    {
        Console.Write("Inserisci il nome dell'espansione: ");
        string nomeEspansione = Console.ReadLine()?.Trim();

        var carte = _cartaDb.TrovaPerEspansione(nomeEspansione);

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



        string sql = "select carta.id_carta from carta where nome_pokemon = @nome and id_espansione = @id_espansione;";
        using var cmd = new MySqlCommand(sql, _conn);
        cmd.Parameters.AddWithValue("@nome", nomeCarta);
        cmd.Parameters.AddWithValue("@id_espansione", espansioneID);
        using var rdr = cmd.ExecuteReader();
        int cartaID = 0;
        if (!rdr.Read())
        {
            Console.WriteLine($"Carta non trovata.");
            rdr.Close();
        }
        else
        {
            cartaID = rdr.GetInt32("id_carta");
            rdr.Close();
            sql = "delete from carta where carta.id_carta = @carta_id limit 1";
            using var cmd2 = new MySqlCommand(sql, _conn);
            cmd2.Parameters.AddWithValue("@carta_id", cartaID);
            cmd2.ExecuteNonQuery();
            Console.WriteLine($"Carta {nomeCarta}, {espansioneCarta} elimnata.");
        }
    }

}
