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
}

public class Arsenal : DeckElement
{
    public Arsenal(List<Card> cards)
        : base(cards)
    {
        
    }
}

public class Ringside : DeckElement
{
    public Ringside(List<Card> cards)
        : base(cards)
    {
        
    }
}