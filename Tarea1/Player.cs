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
        Console.WriteLine($"({counter}): to end your turn.");
        counter += 1;
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

    public void PlayManeuver(Card maneuver)
    {
        // Bajar la carta su Ring Area
        // Esto implica:
        //      actualizar su Fortitude
        //      Actualizar las cartas del Ring area
        //      Sacar la carta de su mano
        UpdateFortitude(Convert.ToInt32(maneuver.Damage));
        RingArea.AddCard(maneuver);
        DiscardCard(maneuver);
    }

    public void UpdateFortitude(int fortitudeNumber)
    {
        Fortitude += fortitudeNumber;
        Console.WriteLine($"$Your new fortitude is of {Fortitude}");
    }

    public bool ReceiveDamage(Card card)
    {
        bool endGame = false;
        Console.WriteLine($"Player number {Number + 1} receives {card.Damage} damage");
        // Descartar el dano respectivo de cartas del Arsenal
        for (int i = 0; i < Convert.ToInt32(card.Damage); i++)
        {
            Card droppedCard = Arsenal.DropUpperCard();
            Console.WriteLine($"Arsenal cards: ${Arsenal.Cards.Count}");
            // Chequear si quedan cartas. Si no quedan, se acaba el juego.
            bool arsenalCards = Arsenal.HaveCards();
            if (arsenalCards == false)
            {
                endGame = true;
                Ringside.AddCard(droppedCard);  // Agregi la carta al Ringside pq voy a salir del for.
                break;
            }
            
            // Actualizar su Ringside con cada carta que se va botando
            Ringside.AddCard(droppedCard);
        }

        return endGame;
    }

    public void DiscardCard(Card card)
    {
        foreach (var c in _hand)
        {
            if (card.Title == c.Title)
            {
                _hand.Remove(c);
                break;
            }
        }
    }


    
    
    // Se asume que la carta de mas arriba es la PRIMERA. Asi al sacar la primera es la de mas arriba
    // Roban mano inicial
}