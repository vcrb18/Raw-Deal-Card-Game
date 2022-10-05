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

    static void PrintEquals(object obj1, object obj2){
            if(obj1.Equals(obj2)){
                Console.WriteLine("Both of the values are equal");
            }else{
                Console.WriteLine("Both of the values are not equal");
            }
        }
    
    public void DrawCards(int draw, Player opponentPlayer)
    {
        int counter = 0;
        for (int i = 0; i < draw; i++)
        {
            counter++;
            // Console.WriteLine($"Es la vez numero {counter} que entro al for");
            List<Card> arsenalCards = Arsenal.Cards;
            
            // Console.WriteLine($"Esta es la primera carta del arsenal del player: {arsenalCards[i].Title}");
            // Console.WriteLine($"{arsenalCards[i].CardSkill}");
            // Console.WriteLine($"Esta es la primera carta del arsenal del opponent: {opponentPlayer.Arsenal.Cards[i].Title}");
            // Console.WriteLine($"{opponentPlayer.Arsenal.Cards[i].CardSkill}");
            
            // Meter la carta del Arsenal a mi mano
            Card newCard = arsenalCards[i];
            
            // Console.WriteLine($"Cartas en la mano justo antes de robar del jugador {Number} = {GetHandLength()}");
            // Console.WriteLine($"Cartas en la mano justo antes de robar del jugador {opponentPlayer.Number} = {GetHandLength()}");
            
            ///////////////////////////////
            // MyHand.AddCardToHand(newCard); // Accede a ambas manos por alguna razon
            MyHand.Cards.Add(newCard);
            // opponentPlayer.MyHand.RemoveCardFromHand(newCard);
            // Console.WriteLine($"PrintEquals(MyHand, opponentPlayer.MyHand)");
            // PrintEquals(MyHand, opponentPlayer.MyHand);
            
            ///////////////////////////////

            // Console.WriteLine($"Cartas en la mano justo despeus de robar del jugador {Number} = {GetHandLength()}");
            // Console.WriteLine($"Cartas en la mano justo despeus de robar del jugador {opponentPlayer.Number} = {GetHandLength()}");
            
            // Console.WriteLine($"Player {Number} last hand card: {MyHand.Cards[MyHand.Cards.Count - 1].Title}");

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

    public void PutDownReversalToRingArea(Card reversalCard)
    {
        RingArea.AddCardAtTheEnd(reversalCard);
    }

    public void UpdateFortitude(int fortitudeNumber)
    {
        Fortitude += fortitudeNumber;
        Console.WriteLine($"$Your new fortitude is of {Fortitude}");
    }

    public bool ReceiveDamage(Player opponent, Card card)
    {
        bool endGame = false;
        int totalDamage = Convert.ToInt32(card.Damage);
        Vista.HowMuchDamageIsReceived(opponent, card);
        // Descartar el dano respectivo de cartas del Arsenal
        for (int i = 1; i < totalDamage + 1; i++)
        {
            
            Card droppedCard = Arsenal.DropUpperCard();
            Vista.ReceivingDamage(droppedCard, i, totalDamage);
            /////////////////////////////
            // Chequear si la carta droppeada es un Reversal
                // Revisar si fullfillConditionOne es True. 
                // Revisar si tiene suficiente fortitude para jugarla
                    // Se detiene el dano. BREAK
                    // Avisar que se termino el turno. NO ES LO MISMO QUE GAMEON
                    // el oponente si baja la carta jugada al ring area y aactualiza fortitude.
                    // No se aplica dano del reversal.
                    // No se aplica efecto del reversal
                    
                    // Revisar si no se alcanzo a hacer todo el dano
                    // El player roba cartas igual al stun val;ue de la carta que fue revertida.
            /////////////////////////////
            // Console.WriteLine($"Arsenal cards: ${Arsenal.Cards.Count}");
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

    // Actualmente solo para maneuvers.
    public List<Card> AvailableCardsToPlayInTurn()
    {
        List<Card> availableCards = new List<Card>(); 
        foreach (var handCard in MyHand.Cards)
        {
            if (Convert.ToInt32(handCard.Fortitude) <= Fortitude && handCard.Types.Contains("Maneuver"))
            {
                availableCards.Add(handCard);
            }
        }

        return availableCards;
    }
    

    public DeckElement GetDeckElement(DeckElement element)
    {
        DeckElement r = null;
        Type tipo = element.GetType();
        //Ringside, Arsenal, RingArea, Hand
        DeckElement[] propertiesArray = { this.Ringside, this.Arsenal, this.RingArea, this.MyHand };
        for (int i = 0; i < propertiesArray.Length; i++)
        {
            DeckElement playerElement = propertiesArray[i];
            if (playerElement.GetType() == element.GetType())
            {
                r = playerElement;
            }
        }

        return r;
    }

    public bool HaveReversalInHand()
    {
        bool reversalExists = false;
        foreach (var c in GetHand())
        {
            if (c.Types.Contains("Reversal"))
            {
                reversalExists = true;
            }
        }

        return reversalExists;
    }

    public List<Card> PlayerReversals()
    {
        List<Card> listOfReversals = new List<Card>();
        foreach (var c in GetHand())
        {
            if (c.Types.Contains("Reversal"))
            {
                listOfReversals.Add(c);
            }
        }
        return listOfReversals;
    }

    public List<Card> GetAvailableReversals(Card cardToReverse)
    {
        List<Card> availableReversals = new List<Card>();
        List<Card> myReversals = PlayerReversals();  // puede ser vacio
        // Console.WriteLine($"myReversals: {myReversals.Count}");
        if (myReversals.Count > 0)
        {
            foreach (var reversalCard in myReversals)
            {
                if (reversalCard == null)
                {
                }
                else
                {
                    // ReverseSKill reversalCardSkill = reversalCard.CardSkill as ReverseSKill;
                    ReverseSkill reversalCardSkill = (ReverseSkill)reversalCard.CardSkill;
                    if (reversalCardSkill.fullfillConditionOne(cardToReverse) == true)
                    {
                        if (Convert.ToInt32(reversalCard.Fortitude) <= Fortitude)
                        {
                            availableReversals.Add(reversalCard);
                        }
                    }
                }
            }
        }
        return availableReversals;
    }
    public bool PlayerHasAvailableReversals(Card cardToReverse)
    {
        bool available = false;
        if (HaveReversalInHand() == true)
        {
            if (GetAvailableReversals(cardToReverse).Count > 0)
            {
                available = true;
            }
        }
        return available;
    }

    public void MoveCardToRingside(Card cartToMove)
    {
        // Quitarla de la mano
        DiscardCard(cartToMove);
        // Agregarla al Ringside
        Ringside.AddCardAtTheEnd(cartToMove);
    }

    public void PlayerReversalsNotSpecial()
    {
        List<Card> listOfNotSpecialReversals = new List<Card>();
        foreach (var reversal in PlayerReversals())
        {
            
        }
    }

    public bool HaveAnyNotSpecialReversal()
    {
        bool exists = false;
        foreach (var reversalCard in PlayerReversals())
        {
            if (!( reversalCard.Subtypes.Contains("ReversalSpecial") || reversalCard.Subtypes.Contains("ReversalGrappleSpecial") || reversalCard.Subtypes.Contains("ReversalStrikeSpecial")))
            {
                exists = true;
            }
        }

        return exists;
    }

    public void ConditionOneReversal()
    {
        List<Card> playerReversals = PlayerReversals();
        foreach (var reversalCard in playerReversals)
        {
            if (reversalCard.Subtypes.Contains("ReversalSpecial"))
            {
                
            }
        }
    }



    // Se asume que la carta de mas arriba es la PRIMERA. Asi al sacar la primera es la de mas arriba
    // Roban mano inicial
}