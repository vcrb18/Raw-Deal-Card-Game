using System.Reflection;

namespace Tarea1;

public class Player
{
    public int Number;
    public Deck Deck;
    public Ringside Ringside;
    public Arsenal Arsenal;
    public RingArea RingArea;
    public int Fortitude = 0;
    public Hand MyHand;
    public Player(int number, Deck deck, Arsenal arsenal, Ringside ringside, RingArea ringArea, Hand myHand)
    {
        Number = number;
        Deck = deck;
        Arsenal = arsenal;
        Ringside = ringside;
        RingArea = ringArea;
        MyHand = myHand;
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

    public void DrawCards(int draw)
    {
        Console.WriteLine($"Player number {Number + 1} draws {draw} Cards");
        int counter = 0;
        for (int i = 0; i < draw; i++)
        {
            counter++;
            Console.WriteLine($"Es la vez numero {counter} que entro al for");
            List<Card> arsenalCards = Arsenal.Cards;
            
            // Meter la carta del Arsenal a mi mano
            Card newCard = arsenalCards[i];
            MyHand.AddCardToHand(newCard);
            Console.WriteLine($"Cartas en la mano ahora del jugador {Number} = {GetHandLength()}");
            
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
        foreach (var c in MyHand.Cards)
        {
            Console.WriteLine($"({counter}) {c.Title}. Types: {string.Join(", ", c.Types)}. Subtypes: {string.Join(", ", c.Subtypes)}. Fortitude: {c.Fortitude}. Damage: {c.Damage}. StunValue: {c.StunValue}. CardEffect: {c.CardEffect} ");
            counter += 1;
        }
    }

    public int GetHandLength()
    {
        return MyHand.Cards.Count;
    }

    public List<Card> GetHand()
    {
        return MyHand.Cards;
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
        foreach (var c in MyHand.Cards)
        {
            if (card.Title == c.Title)
            {
                MyHand.Cards.Remove(c);
                break;
            }
        }
    }

    
    // Lo que queirto ahcer es retornar el objecto respectivo del jugador
    // asi puedo trabajar y mover las cartas de un lado a otro
    public DeckElement GetDeckElement(DeckElement element)
    {
        // Va a recibir un Deck Element
        // Va a retornar el respectivo DeckElement del player
        foreach (PropertyInfo prop in this.GetType().GetProperties())
        {
            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            if (type == typeof(element))
            {
                return 
            }
        }
    }


    
    
    // Se asume que la carta de mas arriba es la PRIMERA. Asi al sacar la primera es la de mas arriba
    // Roban mano inicial
}