using System.Linq;


namespace Tarea1;

public class Deck
{
    private Card[] _cards;
    public SuperStar SuperStar;

    public Deck(Card[] cards, SuperStar superstar)
    {
        _cards = cards;
        SuperStar = superstar;
    }

    public void Describe()
    {
        Console.WriteLine($"The deck with the Super Star {SuperStar.Type} !!!");
    }
    

    public Card[] GetCards()
    {
        return _cards;
    }
    
    // No Funciona xD    
    public void ShuffleCards()
    {
        Random rnd = new Random();
        for (int i = 0; i < _cards.Count(); i++)
        {
            int j = rnd.Next(0, _cards.Count());
            Card temp = _cards[i];
            _cards[i] = _cards[j];
            _cards[j] = temp;
        }
    }
}