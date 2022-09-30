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
    
    
}