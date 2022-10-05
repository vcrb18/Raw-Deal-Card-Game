using System.Linq;


namespace Tarea1;

public class Deck
{
    public Card[] Cards;
    public SuperStar SuperStar;

    public Deck(Card[] cards, SuperStar superstar)
    {
        Cards = cards;
        SuperStar = superstar;
    }

    public void Describe()
    {
        Console.WriteLine($"The deck with the Super Star {SuperStar.Type} !!!");
    }
    

    public Card[] GetCards()
    {
        return Cards;
    }
    
    // No Funciona xD    
    public void ShuffleCards()
    {
        Random rnd = new Random();
        for (int i = 0; i < Cards.Count(); i++)
        {
            int j = rnd.Next(0, Cards.Count());
            Card temp = Cards[i];
            Cards[i] = Cards[j];
            Cards[j] = temp;
        }
    }
}