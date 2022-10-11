namespace Tarea1;

public abstract class DeckElement
{
    public abstract string GetDeckElementName();
    
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
public class RingArea : DeckElement
{
    public RingArea(List<Card> cards)
        : base(cards)
    {
    }

    public override string GetDeckElementName()
    {
        return "RingArea";
    }
    public void AddCard(Card card)
    {
        Cards.Add(card);
        Console.WriteLine($"{card.Title} goes to the Ring Area");  
    }
}
public class Arsenal : DeckElement
{
    public Arsenal(List<Card> cards)
        : base(cards)
    {
    }

    public override string GetDeckElementName()
    {
        return "Arsenal";
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

public class Ringside : DeckElement
{
    public Ringside(List<Card> cards)
        : base(cards)
    {
        
    }
    public override string GetDeckElementName()
    {
        return "Ringside";
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
    public override string GetDeckElementName()
    {
        return "Hand";
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