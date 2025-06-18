//da inserire all'interno di Admin.cs

public void AggiungiCarta()
{
    Console.WriteLine("== Aggiungi una nuova carta ==");
    Console.Write("Nome Pokemon: ");
    string nomePokemon = Console.ReadLine();

    Console.WriteLine("Tipo (scegli tra: Acqua, Fuoco, Erba, Elettro, Psico, Lotta, Buio, Metallo, Normale, Folletto, Drago):");
    string tipo = Console.ReadLine();

    Console.WriteLine("Rarità (scegli tra: Comune, Non Comune, Rara, Rara-Holo, Ultra-Rara, Rara-Segreta):");
    string rarita = Console.ReadLine();

    Console.Write("Prezzo: ");
    decimal prezzo;
    while (!decimal.TryParse(Console.ReadLine(), out prezzo))
    {
        Console.Write("Inserisci un valore numerico valido per il prezzo: ");
    }

    Console.Write("Inserisci l'URL dell'immagine: ");
    string urlImg = Console.ReadLine();

    Console.Write("La carta è reverse? (s/n): ");
    bool isReverse = Console.ReadLine().Trim().ToLower() == "s";

    Console.Write("ID espansione: ");
    int idEspansione;
    while (!int.TryParse(Console.ReadLine(), out idEspansione))
    {
        Console.Write("Inserisci un valore numerico valido per l'ID espansione: ");
    }

    string query = "INSERT INTO Carta (nome_pokemon, tipo, rarita, prezzo, url_img, is_reverse, id_espansione) " +
                   "VALUES (@nomePokemon, @tipo, @rarita, @prezzo, @urlImg, @isReverse, @idEspansione);";

    Console.WriteLine("\n[ LOG ] Query generata:");
    Console.WriteLine(query);
}