using System;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

class Carta
{
    public enum Tipo
    {
        Acqua,
        Fuoco,
        Erba,
        Elettro,
        Psico,
        Lotta, 
        Buio,
        Metallo,
        Normale, 
        Folletto, 
        Drago
    }

    public enum Rarita
    {
        Comune,
        Non_Comune,
        Rara,
        Rara_Holo,
        Ultra_Rara,
        Rara_Segreta
    }

    public void AggiungiCarta(MySqlConnection conn)
    {
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
            string sqlEspansione = "Select espansione.id_espansione from espansione where espansione.nome_espansione=@espansione;";
            MySqlCommand cmdEspansione = new MySqlCommand(sqlEspansione, conn);
            cmdEspansione.Parameters.AddWithValue("@espansione", espansioneCarta);
            MySqlDataReader rdr = cmdEspansione.ExecuteReader();

            if (rdr.Read())
            {
                espansioneID = (int)rdr[0];
                rdr.Close();
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
        MySqlCommand cmdAddCarta = new MySqlCommand(sqlAddCarta, conn);
        cmdAddCarta.Parameters.AddWithValue("@nome_pokemon", nomeCarta);
        cmdAddCarta.Parameters.AddWithValue("@tipo", tipoCarta + 1);
        cmdAddCarta.Parameters.AddWithValue("@rarita", raritaCarta + 1);
        cmdAddCarta.Parameters.AddWithValue("@prezzo", prezzoCarta);
        cmdAddCarta.Parameters.AddWithValue("@url_img", urlImgCarta);
        cmdAddCarta.Parameters.AddWithValue("@is_reverse", isReverse);
        cmdAddCarta.Parameters.AddWithValue("@id_espansione", espansioneID);
        cmdAddCarta.ExecuteNonQuery();
    }
}