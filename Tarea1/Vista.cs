namespace Tarea1;

public class Vista
{
    // Tengoque hacer un metodo que sea elegirCarta()
    
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
    public static Card elegirCarta(List<Card> cardsToChoose)
    {
        int counter = 1;
        Console.WriteLine("Escoge una de las siguientes cartas");
        foreach (var c in cardsToChoose)
        { 
            Console.WriteLine($"({counter}) {c.Title}. Types: {string.Join(", ", c.Types)}. Subtypes: {string.Join(", ", c.Subtypes)}. Fortitude: {c.Fortitude}. Damage: {c.Damage}. StunValue: {c.StunValue}. CardEffect: {c.CardEffect} ");            
        }

        int choosenNumber = AskForNumber(1, cardsToChoose.Count);
        Card choosenCard = cardsToChoose[choosenNumber - 1];
        return choosenCard;
    }
}