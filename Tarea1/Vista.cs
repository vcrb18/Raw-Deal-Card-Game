namespace Tarea1;

public class Vista
{
    // Tengoque hacer un metodo que sea elegirCarta()
    
    public static int AskForNumber(int minValue, int maxValue)
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
    public static Card chooseCard(List<Card> cardsToChoose)
    {
        int counter = 1;
        Console.WriteLine("Please choose one of the following cards:");
        foreach (var c in cardsToChoose)
        { 
            Console.WriteLine($"({counter}) {c.Title}. Types: {string.Join(", ", c.Types)}. Subtypes: {string.Join(", ", c.Subtypes)}. Fortitude: {c.Fortitude}. Damage: {c.Damage}. StunValue: {c.StunValue}. CardEffect: {c.CardEffect} ");            
        }

        int choosenNumber = AskForNumber(1, cardsToChoose.Count);
        Card choosenCard = cardsToChoose[choosenNumber - 1];
        return choosenCard;
    }

    public static void InformNoReversalInHand()
    {
        Console.WriteLine("Opponent of this turn does note have Reversal in his hand");
    }

    public static void HasReversalButNotAvailable()
    {
        Console.WriteLine("The opponent has one or more Reversal/s in his hand but he is not available to play any.");
    }

    public static void HasAvailableReversalToPlay()
    {
        Console.WriteLine("The player has one or more Reversals to play.");
    }

    public static void AskToPlayReversalOrNot()
    {
        Console.WriteLine("Please enter 0 if you don't want to play a Reversal Card or 1 if you do.");
    }

    public static void ThisCardHasNoExtraEffect()
    {
        Console.WriteLine("The card played has no extra effect.");
    }

    public enum availableDecks
    {
        HHH,
        JERICHO,
        KANE,
        MANKIND,
        STONE_COLD,
        THE_ROCK,
        UNDERTAKER
    }

    public static Dictionary<int, string> createDecksDictionary()
    {
        Dictionary<int, string> decksDict = new Dictionary<int, string>();
        decksDict[1] = availableDecks.HHH.ToString();
        decksDict[2] = availableDecks.JERICHO.ToString();
        decksDict[3] = availableDecks.KANE.ToString();
        decksDict[4] = availableDecks.MANKIND.ToString();
        decksDict[5] = availableDecks.STONE_COLD.ToString();
        decksDict[6] = availableDecks.THE_ROCK.ToString();
        decksDict[7] = availableDecks.UNDERTAKER.ToString();
        return decksDict;
    }
    public static string ChooseAnyAvailableDeck()
    {
        Dictionary<int, string> decksDict = createDecksDictionary();
        
        Console.WriteLine("Choose any of the following decks");
        for (int i = 1; i < 8; i++)
        {
            Console.WriteLine($"{i}- decks/{i}.txt");
        }
        Console.WriteLine("(Enter a number between 1 and 7)");
        int choosenDeck = AskForNumber(1, 7);
        string deckStringName = decksDict[choosenDeck];
        // Otro modulo que se encargue de buscar el mazo.
        return deckStringName;
    }

    public static void superStarsFaceEachOther(Player playerOne, Player playerTwo)
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"Face each other: {playerOne.Deck.SuperStar.Type} and {playerTwo.Deck.SuperStar.Type}");
        Console.WriteLine($"{playerOne.Deck.SuperStar.Type} has {playerOne.Fortitude}F, {playerOne.MyHand.Cards.Count}cads in his hand and has {playerOne.Arsenal.Cards.Count} cards in the Arsenal  ");
        Console.WriteLine($"{playerTwo.Deck.SuperStar.Type} has {playerTwo.Fortitude}F, {playerTwo.MyHand.Cards.Count}cads in his hand and has {playerTwo.Arsenal.Cards.Count} cards in the Arsenal  ");
        Console.WriteLine("------------------------------------");
    }

    public static void StartsTurn(Player player, Player opponent)
    {
        Console.WriteLine($"{player.Deck.SuperStar.Type} plays. What do you want to do?");
        Console.WriteLine("     0. Use your super ability");
        Console.WriteLine("     1. See your cards or the cards from the opponent");
        Console.WriteLine("     2. Play a card");
        Console.WriteLine("     3. End your turn");
        Console.WriteLine("(Enter a number between 0 and 3)");
        AskForNumber(0, 3);
    }

    public static void SeeCards(Player player, Player opponent)
    {
        Console.WriteLine($"{player.Deck.SuperStar.Type} plays. What cards do you want to see?");
        Console.WriteLine("     1. My hand");
        Console.WriteLine("     2. My ringside");
        Console.WriteLine("     3. My ring area");
        Console.WriteLine("     4. My opponent's ringside");
        Console.WriteLine("     5. My opponent's ring area");
        Console.WriteLine("(Enter a number between 1 and 5)");
    }

    public static void SeeHandCards(Player player)
    {
        int counter = 0;
        foreach (Card handCard in player.MyHand.Cards)
        {
            Console.WriteLine($"------------- Card #{counter}");
            Console.WriteLine($"Title: {handCard.Title}");
            Console.WriteLine($"Stats: [{handCard.Fortitude}F/{handCard.Damage}D/{handCard.StunValue}SV");
            Console.WriteLine($"Types: {handCard.Types}");
            Console.WriteLine($"Subtypes: {handCard.Subtypes}");
            Console.WriteLine($"Effect: {handCard.CardEffect}");
        }
    }
}