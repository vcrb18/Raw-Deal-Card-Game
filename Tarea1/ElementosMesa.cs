namespace Tarea1;

public class DeckElement
{
    public List<Card> Cards;

    public DeckElement(List<Card> cards)
    {
        Cards = cards;
    }
}

public class RingArea : DeckElement
{
    public RingArea(List<Card> cards)
        : base(cards)
    {
    }

    public void AddCard(Card card)
    {
        Cards.Add(card);
        Console.WriteLine($"{card.Title} goes to the Ring Area");  
    }
}
// Carta boca abajo
// La carta de mas arriba es la primera de la lista
public class Arsenal : DeckElement
{
    public Arsenal(List<Card> cards)
        : base(cards)
    {
    }

    public Card DropUpperCard()
    {
        Card droppedCard = Cards[0];
        Cards.RemoveAt((0));
        return droppedCard;
    }
}

// Cartas boca arriba
// La ultima carta de la lista es la que esta mas arriba,
// a diferencia del Arsenal
public class Ringside : DeckElement
{
    public Ringside(List<Card> cards)
        : base(cards)
    {
        
    }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }
}