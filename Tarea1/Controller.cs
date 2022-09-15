namespace Tarea1;
using System.Text.Json;
using System.Text.Json.Serialization;
public class Controller
{
    public static void Run()
    {
        Console.WriteLine("Welcome!");
        
        List<object> StoneColdCards = ChargeDecks("STONE_COLD");
        Card[] DeckCardsSC = (Card[])StoneColdCards[1];
        SuperStar superStarSC = (SuperStar)StoneColdCards[0];
        Deck StoneColdDeck = new Deck(DeckCardsSC, superStarSC);
        
        List<object> TheRockCards = ChargeDecks("THE_ROCK");
        Card[] DeckCardsTR = (Card[])TheRockCards[1];
        SuperStar superStarTR = (SuperStar)TheRockCards[0];
        Deck TheRockDeck = new Deck(DeckCardsTR, superStarTR);

        List<Player> players = ChooseDeck(StoneColdDeck, TheRockDeck);
        Player playerOne = players[0];
        Player playerTwo = players[1];
        playerOne.ShuffleDeck();
        playerTwo.ShuffleDeck();
        Console.WriteLine("It's time to.... PLAY!!!");
        Console.WriteLine(("\nPlayers put their deck downside in the desk"));
        
        playerOne.DrowCards();
        playerTwo.DrowCards();

        Player starter = WhoStarts(playerOne, playerTwo);
        Console.WriteLine($"Player {starter.Number + 1} starts");
    }

    private static Card[] ReadCardInfos()
    {
        string fileName = "cards.json";
        string jsonString = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<Card[]>(jsonString);
    }

    private static SuperStar[] ReadSuperstarInfos()
    {
        string fileName = "superstars.json";
        string jsonString = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<SuperStar[]>(jsonString);
    }

    private static List<object> ChargeDecks(string path)
    {
        StreamReader r = new StreamReader($"decks/{path}.txt");
        int counter = 0;
        int numberOfCards = 60;
        Card[] arr = new Card[numberOfCards];
        var cardsSuperstarList = new List<object>();
        while (!r.EndOfStream)
        {
            string line = r.ReadLine();
            // Console.WriteLine((line.Length));
            if (counter >= 1)
            {
                string cardNumber = line.Substring(0, 1);
                int cardNumberRepeated = Int32.Parse(cardNumber);
                string cardName = line.Substring(2, line.Length - 2);
                for (int i = 0; i < cardNumberRepeated; i++)
                {
                    List<object> cardAttributes = SearchCardName(cardName);
                    Card cardsJson = new Card((string)cardAttributes[0], (List<string>)cardAttributes[1], (List<string>)cardAttributes[2],
                        (string)cardAttributes[3], (string)cardAttributes[4], (string)cardAttributes[5], (string)cardAttributes[6]);
                    arr[counter - 1] = cardsJson;
                    counter += 1;
                }
            }
            if (counter == 0)
            {
                string[] words = line.Split(" (Superstar Card)");
                string superStarName = words[0];
                List<object> superStarAttributes = SearchSuperStar(superStarName);
                SuperStar super = new SuperStar((string)superStarAttributes[0], (int)superStarAttributes[1],
                    (int)superStarAttributes[2], (string)superStarAttributes[3]);
                cardsSuperstarList.Add(super);                
                counter += 1;
            }
        }
        r.Close();
        // Necesito retornar el arr de cartas del mazo
        // Necesito retornar la SuperEstrella
        cardsSuperstarList.Add(arr);
        return cardsSuperstarList;
    }
    
    // Este metodo tendra que retornar los atributos que recibe la clase carta
    private static List<object> SearchCardName(string cardName)
    {
        List<object> r = new List<object>();
        foreach (var card in ReadCardInfos())
        {
            if (card.Title == cardName)
            {
                r.Add(card.Title);
                r.Add(card.Types);
                r.Add(card.Subtypes);
                r.Add(card.Fortitude);
                r.Add(card.Damage);
                r.Add(card.StunValue);
                r.Add(card.CardEffect);
            }
        }
        return r;
    }
    
    private static List<object> SearchSuperStar(string SuperStarName)
    {
        List<object> r = new List<object>();
        foreach (var super in ReadSuperstarInfos())
        {
            if (super.Type == SuperStarName)
            {
                r.Add(super.Type);
                r.Add(super.HandSize);
                r.Add(super.StarValue);
                r.Add(super.SuperStarAbility);
            }
        }
        return r;
    }
    
    private static List<Player> ChooseDeck(Deck DeckOne, Deck DeckTwo)
    {
        Console.WriteLine($"There are two Decks available. The first one is:");
        DeckOne.Describe();
        
        Console.WriteLine("The second one available is:");
        DeckTwo.Describe();
        
        Console.WriteLine("\n Player 1 please choose your deck:");
        Console.WriteLine($"    (0) {DeckOne.SuperStar.Type} Deck");
        Console.WriteLine($"    (1) {DeckTwo.SuperStar.Type} Deck");

        List<Card> emptyList = new List<Card>();
        Player PlayerOne = new Player(0, null, null, new Ringside(emptyList), new RingArea(emptyList));
        Player PlayerTwo = new Player(1, null, null,new Ringside(emptyList), new RingArea(emptyList));
        int answer = AskForNumber(0, 1);
        if (answer == 0)
        {
            PlayerOne.Deck = DeckOne;
            List<Card> deckCardsPlayerOne = DeckOne.GetCards().ToList();
            PlayerOne.Arsenal = new Arsenal(deckCardsPlayerOne);
            
            PlayerTwo.Deck = DeckTwo;
            List<Card> deckCardsPlayerTwo = DeckTwo.GetCards().ToList();
            PlayerTwo.Arsenal = new Arsenal(deckCardsPlayerTwo);
            
            Console.WriteLine("Player 1 chooses Deck One. Deck Two is assigned automatically to Player 2");
            // Player PlayerOne = new Player(0, DeckOne);
            // Player PlayerTwo = new Player(1, DeckTwo);
        }
        else
        {
            PlayerOne.Deck = DeckTwo;
            List<Card> deckCardsPlayerOne = DeckTwo.GetCards().ToList();
            PlayerOne.Arsenal = new Arsenal(deckCardsPlayerOne);
            
            PlayerTwo.Deck = DeckOne;
            List<Card> deckCardsPlayerTwo = DeckOne.GetCards().ToList();
            PlayerTwo.Arsenal = new Arsenal(deckCardsPlayerTwo);
            Console.WriteLine("Player 1 chooses Deck Two. Deck One is assigned automatically to Player 2");
            // Player PlayerOne = new Player(0, DeckTwo);
            // Player PlayerTwo = new Player(1, DeckOne);
        }
        var PlayersList = new List<Player>();
        PlayersList.Add(PlayerOne);
        PlayersList.Add(PlayerTwo);
        return PlayersList;
    }

    private static int AskForNumber(int minValue, int maxValue)
    {
        int number;
        bool wasParseSuccessfull;
        do
        {
            string? userInput = Console.ReadLine();
            wasParseSuccessfull = int.TryParse(userInput, out number);
        } while (!wasParseSuccessfull || number < minValue || number > maxValue);

        return number;
    }

    private static Player WhoStarts(Player firstPlayer, Player secondPlayer)
    {
        int starValuePlayerOne = firstPlayer.Deck.SuperStar.StarValue;
        int starValuePlayerTwo = secondPlayer.Deck.SuperStar.StarValue;
        Console.WriteLine($"The Star Value for the first player is {starValuePlayerOne}.");
        Console.WriteLine($"The Star Value for the second player is {starValuePlayerTwo}.");
        if (starValuePlayerOne > starValuePlayerTwo)
        {
            return firstPlayer;
        }
        else if (starValuePlayerOne < starValuePlayerTwo)
        {
            return secondPlayer;
        }
        else
        {
            Console.WriteLine("Star Value for both players is the same. A coin will be thrown.");
            Player starter = Coin(firstPlayer, secondPlayer);
            return starter;
        }
    }

    private static Player Coin(Player PlayerOne, Player PlayerTwo)
    {
        Console.WriteLine("\nPlayer 1 please choose:");
        Console.WriteLine(" (0) Heads or");
        Console.WriteLine(" (1) Tails");
        int choosenNumber = AskForNumber(0, 1);
        Console.WriteLine("We throw the coin!!! And...");
        Random rnd = new Random();
        int number = rnd.Next(0, 2);
        if (number == 0)
        {
            Console.WriteLine("We get Heads");
            if (choosenNumber == 0)
            {
                return PlayerOne;
            }
            else
            {
                return PlayerTwo;
            }
        }
        else
        {
            Console.WriteLine("We get Tails");
            if (choosenNumber == 1)
            {
                return PlayerOne;
            }
            else
            {
                return PlayerTwo;
            }
        }
    }
    
    
}