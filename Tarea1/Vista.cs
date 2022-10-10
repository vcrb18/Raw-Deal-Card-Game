namespace Tarea1;

public class Vista
{
    // Tengoque hacer un metodo que sea elegirCarta()
    
    public static int AskForNumber(int minValue, int maxValue)
    {
        int number;
        bool wasParseSuccessfull;
        do
        {
            string? userInput = Console.ReadLine();
            wasParseSuccessfull = int.TryParse(userInput, out number);
        } while (!wasParseSuccessfull || number < minValue || number > maxValue);

        return number;
    }
    public static Card chooseCard(List<Card> cardsToChoose)
    {
        int counter = 1;
        Console.WriteLine("Please choose one of the following cards:");
        foreach (var c in cardsToChoose)
        { 
            Console.WriteLine($"({counter}) {c.Title}. Types: {string.Join(", ", c.Types)}. Subtypes: {string.Join(", ", c.Subtypes)}. Fortitude: {c.Fortitude}. Damage: {c.Damage}. StunValue: {c.StunValue}. CardEffect: {c.CardEffect} ");            
        }

        int choosenNumber = AskForNumber(1, cardsToChoose.Count);
        Card choosenCard = cardsToChoose[choosenNumber - 1];
        return choosenCard;
    }

    public static void InformNoReversalInHand()
    {
        Console.WriteLine("Opponent of this turn does note have Reversal in his hand");
    }

    public static void HasReversalButNotAvailable()
    {
        Console.WriteLine("The opponent has one or more Reversal/s in his hand but he is not available to play any.");
    }

    public static void HasAvailableReversalToPlay()
    {
        Console.WriteLine("These are the cards you can play:");
    }

    public static void AskToPlayReversalOrNot()
    {
        Console.WriteLine("Please enter 0 if you don't want to play a Reversal Card or 1 if you do.");
    }

    public static void ThisCardHasNoExtraEffect()
    {
        Console.WriteLine("The card played has no extra effect.");
    }

    public enum availableDecks
    {
        HHH,
        JERICHO,
        KANE,
        MANKIND,
        STONE_COLD,
        THE_ROCK,
        UNDERTAKER
    }

    public static Dictionary<int, string> createDecksDictionary()
    {
        Dictionary<int, string> decksDict = new Dictionary<int, string>();
        decksDict[1] = availableDecks.HHH.ToString();
        decksDict[2] = availableDecks.JERICHO.ToString();
        decksDict[3] = availableDecks.KANE.ToString();
        decksDict[4] = availableDecks.MANKIND.ToString();
        decksDict[5] = availableDecks.STONE_COLD.ToString();
        decksDict[6] = availableDecks.THE_ROCK.ToString();
        decksDict[7] = availableDecks.UNDERTAKER.ToString();
        return decksDict;
    }
    public static string ChooseAnyAvailableDeck()
    {
        Dictionary<int, string> decksDict = createDecksDictionary();
        
        Console.WriteLine("Choose any of the following decks");
        for (int i = 1; i < 25; i++)
        {
            if (i < 10)
            {
                Console.WriteLine($"{i}- decks/0{i}.txt");
            }
            else
            {
                Console.WriteLine($"{i}- decks/{i}.txt");
            }
        }
        Console.WriteLine("(Enter a number between 1 and 24)");
        int choosenDeck = AskForNumber(1, 20);
        string choosenDeckInString = choosenDeck.ToString().PadLeft(2, '0');
        // string deckStringName = decksDict[choosenDeck];
        // Otro modulo que se encargue de buscar el mazo.
        // return deckStringName;
        return choosenDeckInString.ToString();
    }

    public static void superStarsFaceEachOther(Player playerOne, Player playerTwo)
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"Face each other: {playerOne.Deck.SuperStar.Type} and {playerTwo.Deck.SuperStar.Type}");
        Console.WriteLine($"{playerOne.Deck.SuperStar.Type} has {playerOne.Fortitude}F, {playerOne.MyHand.Cards.Count} cards in his hand and has {playerOne.Arsenal.Cards.Count} cards in the Arsenal  ");
        Console.WriteLine($"{playerTwo.Deck.SuperStar.Type} has {playerTwo.Fortitude}F, {playerTwo.MyHand.Cards.Count} cards in his hand and has {playerTwo.Arsenal.Cards.Count} cards in the Arsenal  ");
        Console.WriteLine("------------------------------------");
    }

    public static void TheROckCanNotUseAbility()
    {
        Console.WriteLine("THE ROCK has no cards in the Ringside, so he can not play his ability");
    }

    public static int AskToPlayRockAbility(Player player)
    {
        Console.WriteLine($"{player.Deck.SuperStar.Type} Can use the Superstar ability:");
        Console.WriteLine($"{player.Deck.SuperStar.SuperStarAbility}");
        Console.WriteLine(". What do you want to do?");
        Console.WriteLine("     0. Do not use the ability");
        Console.WriteLine("     1. Use the ability");
        Console.WriteLine("(Enter a number between 0 and 1)");
        int choosenOption = AskForNumber(0, 1);
        return choosenOption;
    }
    public static int BeginingTurnOptions(Player player, Player opponent)
    {
        int choosenOption;
        bool incorrectOption = true;
        // do
        // {
            superStarsFaceEachOther(player, opponent);
            Console.WriteLine($"{player.Deck.SuperStar.Type} plays. What do you want to do?");
            // Console.WriteLine("     0. Use your super ability");
            Console.WriteLine("     1. See your cards or the cards from the opponent");
            Console.WriteLine("     2. Play a card");
            Console.WriteLine("     3. End your turn");
            Console.WriteLine("(Enter a number between 0 and 3)");
            // choosenOption = AskForNumber(0, 3);
            choosenOption = AskForNumber(1, 3);
        //     if (choosenOption == 1)
        //     {
        //         WhatCardsoSee(player, opponent);
        //         incorrectOption = true;                                  
        //     }
        //     else if (choosenOption == 2)
        //     {
        //         incorrectOption = false;
        //     }
        //     else
        //     {
        //         // TurnForPlayerEnds(player);
        //         incorrectOption = false;
        //     }
        // // } while (incorrectOption == true);
        return choosenOption;
    }
    
    public static int BeginingTurnOptionsWithSuperAbility(Player player, Player opponent)
    {
        int choosenOption;
        bool incorrectOption = true;
        // do
        // {
        superStarsFaceEachOther(player, opponent);
        Console.WriteLine($"{player.Deck.SuperStar.Type} plays. What do you want to do?");
        Console.WriteLine("     0. Use your super ability");
        Console.WriteLine("     1. See your cards or the cards from the opponent");
        Console.WriteLine("     2. Play a card");
        Console.WriteLine("     3. End your turn");
        Console.WriteLine("(Enter a number between 0 and 3)");
        choosenOption = AskForNumber(0, 3);
        //     if (choosenOption == 1)
        //     {
        //         WhatCardsoSee(player, opponent);
        //         incorrectOption = true;                                  
        //     }
        //     else if (choosenOption == 2)
        //     {
        //         incorrectOption = false;
        //     }
        //     else
        //     {
        //         // TurnForPlayerEnds(player);
        //         incorrectOption = false;
        //     }
        // // } while (incorrectOption == true);
        return choosenOption;
    }

    public static void WhatCardsoSee(Player player, Player opponent)
    {
        Console.WriteLine($"{player.Deck.SuperStar.Type} plays. What cards do you want to see?");
        Console.WriteLine("     1. My hand");
        Console.WriteLine("     2. My ringside");
        Console.WriteLine("     3. My ring area");
        Console.WriteLine("     4. My opponent's ringside");
        Console.WriteLine("     5. My opponent's ring area");
        Console.WriteLine("(Enter a number between 1 and 5)");
        int choosenOption = AskForNumber(1, 5);
        if (choosenOption == 1)
        {
            SeeHandCards(player);
        } else if (choosenOption == 2)
        {
            SeePlayerRingside(player);
        } else if (choosenOption == 3)
        {
            SeePlayerRingArea(player);
        } else if (choosenOption == 4)
        {
            SeeOpponentRingside(opponent);
        }
        else
        {
            SeeOpponentRingArea(opponent);
        }
    }

    public static void SeeHandCards(Player player)
    {
        ListCardsOutput(player.MyHand.Cards);
    }

    public static void SeePlayerRingside(Player player)
    {
        ListCardsOutput(player.Ringside.Cards);
    }

    public static void SeePlayerRingArea(Player player)
    {
        ListCardsOutput(player.Ringside.Cards);
    }

    public static void SeeOpponentRingside(Player opponent)
    {
        ListCardsOutput(opponent.Ringside.Cards);
    }

    public static void SeeOpponentRingArea(Player opponent)
    {
        ListCardsOutput(opponent.RingArea.Cards);
    }
    public static void ListCardsOutput(List<Card> cardsList)
    {
        int counter = 0;
        foreach (Card card in cardsList)
        {
            Console.WriteLine($"------------- Card #{counter}");
            Console.WriteLine($"Title: {card.Title}");
            Console.WriteLine($"Stats: [{card.Fortitude}F/{card.Damage}D/{card.StunValue}SV");
            Console.WriteLine($"Types: {card.Types}");
            Console.WriteLine($"Subtypes: {card.Subtypes}");
            Console.WriteLine($"Effect: {card.CardEffect}");
            counter++;
        }
        Console.WriteLine("-------------");
    }

    public static void TurnForPlayerEnds(Player player)
    {
        Console.WriteLine($"Turn for {player.Deck.SuperStar.Type} ends");
    }

    public static int ChooseCardIDToPlay(List<Card> availableHandCardsofPlayer)
    {
        int counter = 0;
        foreach (Card card in availableHandCardsofPlayer)
        {
            Console.WriteLine($"------------- Card #{counter}");
            Console.WriteLine($"Play this card as a [MANEUVER]");
            Console.WriteLine($"Title: {card.Title}");
            Console.WriteLine($"Stats: [{card.Fortitude}F/{card.Damage}D/{card.StunValue}SV");
            Console.WriteLine($"Types: {card.Types}");
            Console.WriteLine($"Subtypes: {card.Subtypes}");
            Console.WriteLine($"Effect: {card.CardEffect}");
            counter++;
        }
        Console.WriteLine("-------------");
        Console.WriteLine("Enter the ID of the card you want to play. You can enter -1 to cancel.");
        int idCard = AskForNumber(-1, counter - 1);
        return idCard;
    }
    
    public static void InformThatPlayerMustDiscard(Player playerWhoDiscards, int numberOfCardsToDiscard)
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"Player {playerWhoDiscards.Deck.SuperStar.Type} must discard {numberOfCardsToDiscard} card (s)");
        Console.WriteLine("What card do you want to discard?\n");
    }
    
    public static void InformThatPlayerCanDrawCard(Player playerWhoDraws, int numberOfCardsToDraw)
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"Player {playerWhoDraws.Deck.SuperStar.Type} will draw {numberOfCardsToDraw} card (s)");
    }

    public static int PlayerCanTakeACard(Player player, List<Card> cardsToChooseFrom, int howManyCards)
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"Player {player.Deck.SuperStar.Type} can choose {howManyCards} card(s) and put them in the hand.");
        Console.WriteLine("What card do you want to get?");
        int counter = 0;
        foreach (Card card in cardsToChooseFrom)
        {
            Console.WriteLine($"------------- Card #{counter}");
            Console.WriteLine($"Title: {card.Title}");
            Console.WriteLine($"Stats: [{card.Fortitude}F/{card.Damage}D/{card.StunValue}SV");
            Console.WriteLine($"Types: {card.Types}");
            Console.WriteLine($"Subtypes: {card.Subtypes}");
            Console.WriteLine($"Effect: {card.CardEffect}");
            counter++;
        }
        Console.WriteLine($"(Enter a number between 0 and {counter - 1}");
        int numberChoosen = AskForNumber(0, counter - 1);
        return numberChoosen;
    }
    
    public static int ChooseCardIDToDiscard(List<Card> availableHandCardsofPlayer)
    {
        int counter = 0;
        foreach (Card card in availableHandCardsofPlayer)
        {
            Console.WriteLine($"------------- Card #{counter}");
            Console.WriteLine($"Title: {card.Title}");
            Console.WriteLine($"Stats: [{card.Fortitude}F/{card.Damage}D/{card.StunValue}SV");
            Console.WriteLine($"Types: {card.Types}");
            Console.WriteLine($"Subtypes: {card.Subtypes}");
            Console.WriteLine($"Effect: {card.CardEffect}");
            counter++;
        }
        Console.WriteLine("-------------");
        Console.WriteLine("Enter the ID of the card you want to discard.");
        int idCard = AskForNumber(0, counter - 1);
        return idCard;
    }

    public static void PlayerTriesToPlayCard(Player player, Card cardToPlay)
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"{player.Deck.SuperStar} tries to play the following card as [MANEUVER]");
        Console.WriteLine($"Title: {cardToPlay.Title}");
        Console.WriteLine($"Stats: [{cardToPlay.Fortitude}F/{cardToPlay.Damage}D/{cardToPlay.StunValue}SV");
        Console.WriteLine($"Types: {cardToPlay.Types}");
        Console.WriteLine($"Subtypes: {cardToPlay.Subtypes}");
        Console.WriteLine($"Effect: {cardToPlay.CardEffect}");
    }

    public static void HasOptionToReverseCard(Player player)
    {
        Console.WriteLine($"But {player.Deck.SuperStar} has the option to reverse the card:");
    }

    public static void NoAvailableReversalToPlay()
    {
        Console.WriteLine("I am sorry, but there is notghing you can play");
    }
    

    public static void cardFromPlayerPlayedSuccessfully(Player player, Player opponent, Card playedCard)
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"{opponent.Deck.SuperStar.Type} does not reverse the card from {player.Deck.SuperStar.Type}. The card {playedCard.Title} [MANEUVER] is succesfully played.");
        Console.WriteLine($"Title: {playedCard.Title}");
        Console.WriteLine($"Stats: [{playedCard.Fortitude}F/{playedCard.Damage}D/{playedCard.StunValue}SV");
        Console.WriteLine($"Types: {playedCard.Types}");
        Console.WriteLine($"Subtypes: {playedCard.Subtypes}");
        Console.WriteLine($"Effect: {playedCard.CardEffect}");
    }

    public static void HowMuchDamageIsReceived(Player opponent, int damage)
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"{opponent.Deck.SuperStar.Type} receives {damage} of damage.\n");
    }

    public static void NewDamageBecauseOfMankind(Player player, int newDamage)
    {
        Console.WriteLine("------------------------------------");
        PlayerUsesSuperstarAbility(player);
        Console.WriteLine($"The new damage is of {newDamage}");

    }

    public static void ReceivingDamage(Card droppedCard, int actualDamage, int totalDamage)
    {
        Console.WriteLine($"------------------------------------ {actualDamage}/{totalDamage} damage");
        Console.WriteLine($"Title: {droppedCard.Title}");
        Console.WriteLine($"Stats: [{droppedCard.Fortitude}F/{droppedCard.Damage}D/{droppedCard.StunValue}SV");
        Console.WriteLine($"Types: {droppedCard.Types}");
        Console.WriteLine($"Subtypes: {droppedCard.Subtypes}");
        Console.WriteLine($"Effect: {droppedCard.CardEffect}");
    }

    public static void DeckIsInvalid(string superstarName)
    {
        Console.WriteLine($"I am sorry but the deck {superstarName} is invalid");
    }

    public static void PlayerUsesSuperstarAbility(Player player)
    {
        Console.WriteLine($"------------------------------------");
        Console.WriteLine($"Player {player.Deck.SuperStar.Type} uses the super ability: ");
        Console.WriteLine($"{player.Deck.SuperStar.SuperStarAbility}");
    }
}