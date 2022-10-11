using System.Diagnostics.SymbolStore;
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

    public Card takeTopCardFromArsenal()
    {
        List<Card> arsenalCards = Arsenal.Cards;
        Card newCard = arsenalCards[0];
        Arsenal.Cards.RemoveAt(0);
        return newCard;
    }
    
    public void DrawCards(int draw)
    {
        int counter = 0;
        for (int i = 0; i < draw; i++)
        {
            counter++;
            List<Card> arsenalCards = Arsenal.Cards;
            Card newCard = arsenalCards[0];
            MyHand.Cards.Add(newCard);
            Arsenal.Cards.RemoveAt(0);
        }
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
    }

    public List<bool> ReceiveDamage(int damage, bool edgeCase, Player playerWhoPlayedmaneuver, Card cardToReverse)
    {
        bool play = false;
        List<bool> endGameAndPlayList = new List<bool>();
        bool endGame = false;
        Vista.HowMuchDamageIsReceived(this, damage);
        if (Deck.SuperStar.Type == "MANKIND" && edgeCase == false)
        {
            damage = damage - 1;
            Vista.NewDamageBecauseOfMankind(this, damage);
        }
        for (int i = 1; i < damage + 1; i++)
        {
            Card droppedCard = Arsenal.DropUpperCard();
            Vista.ReceivingDamage(droppedCard, i, damage);
            if (edgeCase == false)
            {
                if (CheckDroppedReversal(droppedCard, cardToReverse) == true)
                {
                    if (typeof(Tarea1.ReverseCalledCleanBreak).IsInstanceOfType(cardToReverse.CardSkill) == false)
                    {
                        Vista.ThisReversalWillReverseCard(playerWhoPlayedmaneuver);
                        // ACA EL PLAY VA A SER TRUE
                        play = true;
                        if (i != damage)
                        {
                            playerWhoPlayedmaneuver.DrawCards(Convert.ToInt32(cardToReverse.StunValue));
                            Vista.PlayerDrawsStunValue(playerWhoPlayedmaneuver, Convert.ToInt32(cardToReverse.StunValue));
                        }
                        break;
                    }                    
                }
            }
            bool arsenalCards = Arsenal.HaveCards();
            if (arsenalCards == false)
            {
                endGame = true;
                Ringside.AddCard(droppedCard);
                break;
            }
            Ringside.AddCard(droppedCard);
        }
        endGameAndPlayList.Add(endGame);
        endGameAndPlayList.Add(play);
        return endGameAndPlayList;
    }

    public bool CheckDroppedReversal(Card droppedCard, Card cardToReverse)
    {
        bool availableToPlay = false;
        if (droppedCard.Types.Contains("Reversal"))
        {
            ReverseSkill droppedReversalSkill = droppedCard.CardSkill as ReverseSkill;
            if (droppedReversalSkill.fullfillConditionOne(cardToReverse))
            {
                if (Convert.ToInt32(droppedCard.Fortitude) <= Fortitude)
                {
                    availableToPlay = true;
                }
            }
        }
        return availableToPlay;
    }

    public void TakeCardFromRingside(Card card)
    {
        foreach (var c in Ringside.Cards)
        {
            if (card.Title == c.Title)
            {
                Ringside.Cards.Remove(c);
                break;
            }
        }
    }

    public void AddCardToHand(Card card)
    {
        MyHand.Cards.Add(card);
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

    public List<Card> AvailableCardsToPlayInTurn()
    {
        List<Card> availableCards = new List<Card>(); 
        foreach (var handCard in MyHand.Cards)
        {
            if (Convert.ToInt32(handCard.Fortitude) <= Fortitude)
            {
                if (handCard.Types.Contains("Maneuver") == true || handCard.Types.Contains("Action") == true)
                {
                    availableCards.Add(handCard);
                }
            }
        }

        return availableCards;
    }
    public DeckElement GetDeckElementDos(string deckElementName)
    {
        DeckElement r = null;
        DeckElement[] propertiesArray = { this.Ringside, this.Arsenal, this.RingArea, this.MyHand };
        for (int i = 0; i < propertiesArray.Length; i++)
        {
            DeckElement playerElement = propertiesArray[i];
            if (playerElement.GetDeckElementName() == deckElementName)
            {
                r = playerElement;
            }
        }
        return r;
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
        List<Card> myReversals = PlayerReversals();
        if (myReversals.Count > 0)
        {
            foreach (var reversalCard in myReversals)
            {
                if (reversalCard == null)
                {
                }
                else
                {
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
        DiscardCard(cartToMove);
        Ringside.AddCardAtTheEnd(cartToMove);
    }

    public void MoveCardToBottomArsenal(Card cardToMove)
    {
        Arsenal.Cards.Add(cardToMove);
        
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
}