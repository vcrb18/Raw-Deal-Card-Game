namespace Tarea1;

public class Player
{
    public int Number;
    public Deck Deck;
    public Ringside Ringside;
    public Arsenal Arsenal;
    public RingArea RingArea;
    public int Fortitude = 0;
    private List<Card> _hand;
    public Player(int number, Deck deck, Arsenal arsenal, Ringside ringside, RingArea ringArea)
    {
        Number = number;
        Deck = deck;
        Arsenal = arsenal;
        Ringside = ringside;
        RingArea = ringArea;
        _hand = new List<Card>();
    }

    public void Describe()
    {
        Console.WriteLine($"I am Player number {Number + 1}.");
        // Console.WriteLine("My Deck is: {Deck.Describe()}. I also have the following cards in my Deck: \n {Deck.GetCards()}");
    }

    public void ShuffleDeck()
    {
        Console.WriteLine($"Player number {Number + 1} suffles his deck.");
        Deck.ShuffleCards();
    }

    public void DrowCards(int drow)
    {
        Console.WriteLine($"Player number {Number + 1} drows {drow} Cards");
        for (int i = 0; i < drow; i++)
        {
            List<Card> arsenalCards = Arsenal.Cards;
            
            // Meter la carta del Arsenal a mi mano
            Card newCard = arsenalCards[i];
            _hand.Add(newCard);
            
            // Eliminar la carta del Arsenal
            Arsenal.Cards.RemoveAt(i);
        }
        // Console.WriteLine("The cards now in the hand are the following:");
        // GetHandCards();

    }

    public void GetHandCards()
    {
        int counter = 0;
        foreach (var c in _hand)
        {
            Console.WriteLine($"({counter}) {c.Title}. Types: {string.Join(", ", c.Types)}. Subtypes: {string.Join(", ", c.Subtypes)}. Fortitude: {c.Fortitude}. Damage: {c.Damage}. StunValue: {c.StunValue}. CardEffect: {c.CardEffect} ");
            counter += 1;
        }
    }

    public int GetHandLength()
    {
        return _hand.Count;
    }

    public List<Card> GetHand()
    {
        return _hand;
    }


    
    
    // Se asume que la carta de mas arriba es la PRIMERA. Asi al sacar la primera es la de mas arriba
    // Roban mano inicial
}