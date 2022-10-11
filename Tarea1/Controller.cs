namespace Tarea1;
using System.Text.Json;
using System.Reflection;
using System.Text.Json.Serialization;
public class Controller
{
    public static void Run()
    {
        Console.WriteLine("-------------------------");
        string nameDeckOne = Vista.ChooseAnyAvailableDeck();
        string nameDeckTwo = Vista.ChooseAnyAvailableDeck();
        List<object> deckOneToCreate = ChargeDecks(nameDeckOne);
        List<object> deckTwoToCreate = ChargeDecks(nameDeckTwo);
        Card[] deckCardsOne = (Card[])deckOneToCreate[1];
        SuperStar superStarDeckOne = (SuperStar)deckOneToCreate[0];
        Deck firstDeckToChoose = new Deck(deckCardsOne, superStarDeckOne);
        Card[] deckCardsTwo = (Card[])deckTwoToCreate[1];
        SuperStar superStarDeckTwo = (SuperStar)deckTwoToCreate[0];
        Deck secondDeckToChoose = new Deck(deckCardsTwo, superStarDeckTwo);
        List<Player> players = ChooseDeck(firstDeckToChoose, secondDeckToChoose);
        bool firstDeckIsValid = firstDeckToChoose.CheckIfDeckIsValid();
        bool secondDeckIsValid = secondDeckToChoose.CheckIfDeckIsValid();
        if (firstDeckIsValid && secondDeckIsValid)
        {
            Player playerOne = players[0];
            Player playerTwo = players[1];
            playerOne.DrawCards(playerOne.Deck.SuperStar.HandSize);
            playerTwo.DrawCards(playerTwo.Deck.SuperStar.HandSize);
            Vista.superStarsFaceEachOther(playerOne, playerTwo);
            List<Player> starterPlayers = WhoStarts(playerOne, playerTwo);
            Player starter = starterPlayers[0];
            Player notStarter = starterPlayers[1];
            StartGame(starter, notStarter);
        }
    }

    private static Card[] ReadCardInfos()
    {
        string fileName = "cards.json";
        string jsonString = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<Card[]>(jsonString);
    }

    private static SuperStar[] ReadSuperstarInfos()
    {
        string fileName = "superstars.json";
        string jsonString = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<SuperStar[]>(jsonString);
    }
    
    private static List<object> ChargeDecks(string path)
    {
        StreamReader r = new StreamReader($"decks/{path}.txt");
        int counter = 0;
        List<Card> arr = new List<Card>();
        var cardsSuperstarList = new List<object>();
        while (!r.EndOfStream)
        {
            string line = r.ReadLine();
            if (counter >= 1)
            {
                string cardNumber = line.Substring(0, 1);
                int cardNumberRepeated = Int32.Parse(cardNumber);
                string cardName = line.Substring(2, line.Length - 2);
                for (int i = 0; i < cardNumberRepeated; i++)
                {
                    List<object> cardAttributes = SearchCardName(cardName);
                    Card cardsJson = new Card((string)cardAttributes[0], (List<string>)cardAttributes[1],
                        (List<string>)cardAttributes[2],
                        ((string)cardAttributes[3]), (string)cardAttributes[4], (string)cardAttributes[5],
                        (string)cardAttributes[6], (CardInfo)cardAttributes[7], (Skill)cardAttributes[8]);
                    arr.Add(cardsJson);
                    counter += 1;
                }
            }
            if (counter == 0)
            {
                string[] words = line.Split(" (Superstar Card)");
                string superStarName = words[0];
                List<object> superStarAttributes = SearchSuperStar(superStarName);
                SuperStar super = new SuperStar((string)superStarAttributes[0], (int)superStarAttributes[1],
                    (int)superStarAttributes[2], (string)superStarAttributes[3], (SuperStarSkill)superStarAttributes[4]);
                Console.WriteLine($"");
                cardsSuperstarList.Add(super);                
                counter += 1;
            }
        }
        r.Close();
        cardsSuperstarList.Add(Enumerable.Reverse(arr).ToArray());
        return cardsSuperstarList;
    }
    private static List<object> SearchCardName(string cardName)
    {
        List<object> r = new List<object>();
        foreach (var card in ReadCardInfos())
        {
            if (card.Title == cardName)
            {
                r.Add(card.Title);
                r.Add(card.Types);
                r.Add(card.Subtypes);
                r.Add(card.Fortitude);
                r.Add(card.Damage);
                r.Add(card.StunValue);
                r.Add(card.CardEffect);
                r.Add(card.CardInfo);
                CardInfo cardInfo = card.CardInfo;
                Skill objetoSkill = cardInfo.CreateSkillInstance();
                if (cardInfo.ClassName == "ReverseSubtypeManeuver")
                {
                    ReverseSubtypeManeuver skillImplementado = objetoSkill as ReverseSubtypeManeuver;
                    r.Add(skillImplementado);
                }
                else if (cardInfo.ClassName == "ReversalAction")
                {
                    ReverseAnyAction skillImplementado = objetoSkill as ReverseAnyAction;
                    r.Add(skillImplementado);
                }
                else if (cardInfo.ClassName == "ReverseSubtypeManeuverSpecial")
                {
                    ReverseSubtypeManeuverSpecial skillImplementado = objetoSkill as ReverseSubtypeManeuverSpecial;
                    r.Add(skillImplementado);
                }
                else if (cardInfo.ClassName == "ReverseAnyManeuverSpecial")
                {
                    ReverseAnyManeuverSpecial skillImplementado = objetoSkill as ReverseAnyManeuverSpecial;
                    r.Add(skillImplementado);
                }
                else if (cardInfo.ClassName == "ReverseAnyManeuverPlusOneEffect")
                {
                    ReverseAnyManeuverPlusOneEffect skillImplementado = objetoSkill as ReverseAnyManeuverPlusOneEffect;
                    r.Add(skillImplementado);
                }
                else if (cardInfo.ClassName == "ReverseCalledCleanBreak")
                {
                    ReverseCalledCleanBreak skillImplementado = objetoSkill as ReverseCalledCleanBreak;
                    r.Add(skillImplementado);
                }
                else
                {
                    r.Add(objetoSkill);
                }
            }
        }
        return r;
    }
    
    private static List<object> SearchSuperStar(string SuperStarName)
    {
        List<object> r = new List<object>();
        foreach (var super in ReadSuperstarInfos())
        {
            if (super.Type == SuperStarName)
            {
                r.Add(super.Type);
                r.Add(super.HandSize);
                r.Add(super.StarValue);
                r.Add(super.SuperStarAbility);
                if (super.Type == "KANE")
                {
                    KaneSkill kaneSkill = new KaneSkill("must", "beginning");
                    r.Add(kaneSkill);
                }
                else if (super.Type == "HHH")
                {
                    HHHSkill hhhSkill = new HHHSkill("none", "none");
                    r.Add(hhhSkill);
                }
                else if (super.Type == "CHRIS JERICHO")
                {
                    JerichoSkill jerichoSkill = new JerichoSkill("may","once");
                    r.Add(jerichoSkill);
                }
                else if (super.Type == "STONE COLD STEVE AUSTIN")
                {
                    StoneColdSkill stoneColdSkill = new StoneColdSkill("may","once");
                    r.Add(stoneColdSkill);
                }
                else if (super.Type == "THE UNDERTAKER")
                {
                    UndertakerSkill stoneColdSkill = new UndertakerSkill("may","once");
                    r.Add(stoneColdSkill);
                }
                else if (super.Type == "THE ROCK")
                {
                    TheRockSkill stoneColdSkill = new TheRockSkill("may","beginning");
                    r.Add(stoneColdSkill);
                }
                else
                {
                    MankindSkill stoneColdSkill = new MankindSkill("always","during");
                    r.Add(stoneColdSkill);
                }
            }
        }
        return r;
    }
    
    private static List<Player> ChooseDeck(Deck DeckOne, Deck DeckTwo)
    {
        List<Card> emptyListRingsidePlayer1 = new List<Card>();
        List<Card> emptyListRingAreaPlayer1 = new List<Card>();
        List<Card> emptyListHandPlayer1 = new List<Card>();
        List<Card> emptyListRingsidePlayer2 = new List<Card>();
        List<Card> emptyListRingAreaPlayer2 = new List<Card>();
        List<Card> emptyListHandPlayer2 = new List<Card>();
        Hand hand1 = new Hand(emptyListHandPlayer1);
        Hand hand2 = new Hand(emptyListHandPlayer2);
        Player PlayerOne = new Player(0, null, null, new Ringside(emptyListRingsidePlayer1), new RingArea(emptyListRingAreaPlayer1), hand1);
        Player PlayerTwo = new Player(1, null, null,new Ringside(emptyListRingsidePlayer2), new RingArea(emptyListRingAreaPlayer2), hand2);
        PlayerOne.Deck = DeckOne;
        List<Card> deckCardsPlayerOne = DeckOne.GetCards().ToList();
        PlayerOne.Arsenal = new Arsenal(deckCardsPlayerOne);
        PlayerTwo.Deck = DeckTwo;
        List<Card> deckCardsPlayerTwo = DeckTwo.GetCards().ToList();
        PlayerTwo.Arsenal = new Arsenal(deckCardsPlayerTwo);
        var PlayersList = new List<Player>();
        PlayersList.Add(PlayerOne);
        PlayersList.Add(PlayerTwo);
        return PlayersList;
    }

    private static int AskForNumber(int minValue, int maxValue)
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

    private static List<Player> WhoStarts(Player firstPlayer, Player secondPlayer)
    {
        List<Player> startersArray = new List<Player>();
        int starValuePlayerOne = firstPlayer.Deck.SuperStar.StarValue;
        int starValuePlayerTwo = secondPlayer.Deck.SuperStar.StarValue;
        if (starValuePlayerOne > starValuePlayerTwo)
        {
            startersArray.Add(firstPlayer);
            startersArray.Add(secondPlayer);
            return startersArray;
        }
        else if (starValuePlayerOne < starValuePlayerTwo)
        {
            startersArray.Add(secondPlayer);
            startersArray.Add(firstPlayer);
            return startersArray;
        }
        else
        {
            Console.WriteLine("Star Value for both players is the same. A coin will be thrown.");
            Player starter = Coin(firstPlayer, secondPlayer);
            if (starter.Number == 0)
            {
                startersArray.Add(starter);
                startersArray.Add(secondPlayer);
                return startersArray;
            }
            else
            {
                startersArray.Add(starter);
                startersArray.Add(firstPlayer);
                return startersArray;
            }
            
        }
    }

    private static Player Coin(Player PlayerOne, Player PlayerTwo)
    {
        Console.WriteLine("\nPlayer 1 please choose:");
        Console.WriteLine(" (0) Heads or");
        Console.WriteLine(" (1) Tails");
        int choosenNumber = AskForNumber(0, 1);
        Console.WriteLine("We throw the coin!!! And...");
        Random rnd = new Random();
        int number = rnd.Next(0, 2);
        if (number == 0)
        {
            Console.WriteLine("We get Heads");
            if (choosenNumber == 0)
            {
                return PlayerOne;
            }
            else
            {
                return PlayerTwo;
            }
        }
        else
        {
            Console.WriteLine("We get Tails");
            if (choosenNumber == 1)
            {
                return PlayerOne;
            }
            else
            {
                return PlayerTwo;
            }
        }
    }
    private static bool Turn(Player player, Player opponent)
    {
        bool playerUsedSuperAbility = false;
        bool gameOn = true;
        if (player.Deck.SuperStar.Skill.WhenCondition == "beginning")
        {
            if (player.Deck.SuperStar.Skill.UseCondition == "must")
            {
                KaneSkill superstarSkill = player.Deck.SuperStar.Skill as KaneSkill;
                Vista.PlayerUsesSuperstarAbility(player);
                
                superstarSkill.UseAbility(player, opponent);
                
            }
            else
            {
                if (player.Ringside.Cards.Count > 0)
                {
                    int chooseToPlayTheRockAbility = Vista.AskToPlayRockAbility(player);
                    if (chooseToPlayTheRockAbility == 1)
                    {
                        Vista.PlayerUsesSuperstarAbility(player);
                        player.Deck.SuperStar.Skill.UseAbility(player, opponent);
                    }
                }
                else
                {
                    Vista.TheROckCanNotUseAbility();
                }
            }
        }
        if (player.Deck.SuperStar.Type == "MANKIND")
        {
            player.DrawCards(2);
        }
        else
        {
            player.DrawCards(1);
        }
        bool play = true;
        do
        {
            int initialPlayerChoice;
            if (player.Deck.SuperStar.Skill.WhenCondition == "once")
            {
                if (playerUsedSuperAbility == false)
                {
                    initialPlayerChoice = Vista.BeginingTurnOptionsWithSuperAbility(player, opponent);
                    
                }
                else
                {
                    initialPlayerChoice = Vista.BeginingTurnOptions(player, opponent);
                }
            }
            else
            {
                initialPlayerChoice = Vista.BeginingTurnOptions(player, opponent);
            }

            if (initialPlayerChoice == 0)
            {
                playerUsedSuperAbility = true;
                Vista.PlayerUsesSuperstarAbility(player);
                player.Deck.SuperStar.Skill.UseAbility(player, opponent);
                play = false;
            }

            else if (initialPlayerChoice == 1)
            {
                Vista.WhatCardsoSee(player, opponent);
                play = false;
            }

            else if (initialPlayerChoice == 2)
            { 
                List<Card> avaialableManeuvers = player.AvailableCardsToPlayInTurn();
                int idCardToPlay = Vista.ChooseCardIDToPlay(avaialableManeuvers);
                if (idCardToPlay == -1)
                {
                 play = false;
                }
                else
                {
                 Card choosenCardToPlay = avaialableManeuvers[idCardToPlay];
                 Vista.PlayerTriesToPlayCard(player, choosenCardToPlay);
                 Vista.HasOptionToReverseCard(opponent);
                 if (opponent.PlayerHasAvailableReversals(choosenCardToPlay) != true)
                 {
                     Vista.NoAvailableReversalToPlay();
                     play = false;
                     Vista.cardFromPlayerPlayedSuccessfully(player, opponent, choosenCardToPlay);
                     List<bool> endGameAndPlayList = opponent.ReceiveDamage(Convert.ToInt32(choosenCardToPlay.Damage), false, player, choosenCardToPlay);
                     if (endGameAndPlayList[1] == true)
                     {
                         play = true;
                     }
                     if (endGameAndPlayList[0] == true)
                     {
                         gameOn = false;
                         break;
                     }
                     player.PlayManeuver(choosenCardToPlay);
                 }
                 else
                 {
                     List<Card> availableReversals = opponent.GetAvailableReversals(choosenCardToPlay);
                     Vista.HasAvailableReversalToPlay();
                     int idReversalToPlay = Vista.ChooseCardIDToPlay(availableReversals);
                     if (idReversalToPlay == -1)
                     {
                         play = false;
                         Vista.cardFromPlayerPlayedSuccessfully(player, opponent, choosenCardToPlay);
                         List<bool> endGameAndPlayList = opponent.ReceiveDamage(Convert.ToInt32(choosenCardToPlay.Damage), false, player, choosenCardToPlay);
                         if (endGameAndPlayList[1] == true)
                         {
                             play = true;
                         }
                         if (endGameAndPlayList[0] == true)
                         {
                             gameOn = false;
                             break;
                         }
                         player.PlayManeuver(choosenCardToPlay);
                     }
                     else
                     {
                         Card choosenReversalToPlay = availableReversals[idReversalToPlay];
                         Vista.PlayAReversalFromHand(choosenReversalToPlay);
                         player.MoveCardToRingside(choosenCardToPlay);
                         choosenReversalToPlay.CardSkill.UseAbility(player, opponent);
                         string checkedDamage = ChangeCatDamageToString(choosenReversalToPlay.Damage, choosenCardToPlay);
                         player.ReceiveDamage(Convert.ToInt32(checkedDamage), false, player, choosenCardToPlay);
                         opponent.DiscardCard(choosenReversalToPlay);
                         opponent.PutDownReversalToRingArea(choosenReversalToPlay);
                         string checkedDamageForRingArea = ChangeCatDamageToZero(choosenReversalToPlay.Damage);
                         opponent.UpdateFortitude(Convert.ToInt32(checkedDamageForRingArea));
                         play = true;
                     }
                 }
                }
                
            }
            else
            {
                Vista.TurnForPlayerEnds(player);
                play = true;
            }
       } while (play == false);

        return gameOn;
    }
    
    public static string ChangeCatDamageToString(string damage, Card cardToReverse)
    {
        string newDamage;
        if (damage == "#")
        {
            newDamage = cardToReverse.Damage;
        }
        else
        {
            newDamage = damage;
        }

        return newDamage;
    }

    public static string ChangeCatDamageToZero(string damage)
    {
        string checkedDamage;
        if (damage == "#")
        {
            checkedDamage = "0";
        }
        else
        {
            checkedDamage = damage;
        }

        return checkedDamage;
    }


    private static void StartGame(Player starter, Player notStarter)
    {
        bool end = false;
        bool caseOne = true;
        do
        {
            if (caseOne == true)
            {
                bool gameOn = Turn(starter, notStarter);
                if (gameOn == false)
                {
                    Console.WriteLine("A player died. GAME OVER");
                    if (starter.Arsenal.Cards.Count == 0)
                    {
                        Vista.PlayerWins(notStarter);
                    }
                    else
                    {
                        Vista.PlayerWins(starter);
                    }
                    break;
                }
                caseOne = false;
                bool sarterCardsLeft = starter.Arsenal.HaveCards();
                bool notStarterCardsLeft = notStarter.Arsenal.HaveCards();
                if (sarterCardsLeft == false || notStarterCardsLeft == false)
                {
                    end = true;
                }
            }
            else
            {
                bool gameOn = Turn(notStarter, starter);
                if (gameOn == false)
                {
                    Console.WriteLine("A player died. GAME OVER");
                    if (starter.Arsenal.Cards.Count == 0)
                    {
                        Vista.PlayerWins(notStarter);
                    }
                    else
                    {
                        Vista.PlayerWins(starter);
                    }
                    break;
                }
                caseOne = true;
                bool sarterCardsLeft = starter.Arsenal.HaveCards();
                bool notStarterCardsLeft = notStarter.Arsenal.HaveCards();
                if (sarterCardsLeft == false || notStarterCardsLeft == false)
                {
                    end = true;
                }
            }
        } while (end == false);
    }
}