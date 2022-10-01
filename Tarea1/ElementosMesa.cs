namespace Tarea1;

public class DeckElement
{
    public List<Card> Cards;

    public DeckElement(List<Card> cards)
    {
        Cards = cards;
    }

    public void AddCardAtTheEnd(Card card)
    {
        Cards.Add(card);
    }

    public void AddCardAtTheBeginning(Card card)
    {
        
    }
}

// El orden aqui en realidad da lo mismo
// Estan puestos en la mesa nomas.
// Los del final de la lista seran los ultimos en bajarse.
// Por lo tanto los primeros de la lista son los que primero se bajaron.
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

    public bool HaveCards()
    {
        if (Cards.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
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

public class Hand : DeckElement
{
    public Hand(List<Card> cards)
        : base(cards)
    {
        
    }

    public void AddCardToHand(Card card)
    {
        Cards.Add(card);
    }

    public void RemoveCardFromHand(Card cardToRemove)
    {
        Cards.Remove(cardToRemove);
    }


}