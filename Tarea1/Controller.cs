namespace Tarea1;
using System.Text.Json;
using System.Reflection;
using System.Text.Json.Serialization;
public class Controller
{
    public static void Run()
    {
        Console.WriteLine("Welcome!");
        
        List<object> StoneColdCards = ChargeDecks("STONE_COLD");
        Card[] DeckCardsSC = (Card[])StoneColdCards[1];
        SuperStar superStarSC = (SuperStar)StoneColdCards[0];
        Deck StoneColdDeck = new Deck(DeckCardsSC, superStarSC);
        
        List<object> TheRockCards = ChargeDecks("THE_ROCK");
        Card[] DeckCardsTR = (Card[])TheRockCards[1];
        SuperStar superStarTR = (SuperStar)TheRockCards[0];
        Deck TheRockDeck = new Deck(DeckCardsTR, superStarTR);

        List<Player> players = ChooseDeck(StoneColdDeck, TheRockDeck);
        Player playerOne = players[0];
        Player playerTwo = players[1];
        playerOne.ShuffleDeck();
        playerTwo.ShuffleDeck();
        Console.WriteLine("It's time to.... PLAY!!!");
        Console.WriteLine(("\nPlayers put their deck downside in the desk"));
        
        // ------------------------------------------
        Console.WriteLine($"La mano del jugador 1 antes de robar tiene: {playerOne.GetHandLength()}");
        Console.WriteLine($"La mano del jugador 2 antes de robar tiene: {playerTwo.GetHandLength()}");
        // ------------------------------------------
        
        playerOne.DrawCards(playerOne.Deck.SuperStar.HandSize);
        // ------------------------------------------
        Console.WriteLine("Jugador 1 ya robo las cartas iniciales, el jugador 2 no.");
        Console.WriteLine($"Cartas jugador 1: {playerOne.GetHandLength()}");
        Console.WriteLine($"Cartas jugador 2: {playerTwo.GetHandLength()}");
        // ------------------------------------------
        playerTwo.DrawCards(playerTwo.Deck.SuperStar.HandSize);
        
        // ------------------------------------------
        Console.WriteLine($"La mano del jugador 1 despues de robar tiene: {playerOne.GetHandLength()}");
        Console.WriteLine($"La mano del jugador 2 despues de robar tiene: {playerTwo.GetHandLength()}");
        // ------------------------------------------
        
        List<Player> starterPlayers = WhoStarts(playerOne, playerTwo);
        Player starter = starterPlayers[0];
        Player notStarter = starterPlayers[1];
        Console.WriteLine($"Player {starter.Number + 1} starts");
        StartGame(starter, notStarter);
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
        int numberOfCards = 60;
        Card[] arr = new Card[numberOfCards];
        var cardsSuperstarList = new List<object>();
        while (!r.EndOfStream)
        {
            string line = r.ReadLine();
            // Console.WriteLine((line.Length));
            if (counter >= 1)
            {
                string cardNumber = line.Substring(0, 1);
                int cardNumberRepeated = Int32.Parse(cardNumber);
                string cardName = line.Substring(2, line.Length - 2);
                for (int i = 0; i < cardNumberRepeated; i++)
                {
                    List<object> cardAttributes = SearchCardName(cardName);
                    Card cardsJson = new Card((string)cardAttributes[0], (List<string>)cardAttributes[1], (List<string>)cardAttributes[2],
                        ((string)cardAttributes[3]), (string)cardAttributes[4], (string)cardAttributes[5], (string)cardAttributes[6], (CardInfo)cardAttributes[7]);  //Aca debo ponerle (Skills)cardAttributes[6]
                    cardsJson.setSkill();
                    arr[counter - 1] = cardsJson;
                    counter += 1;
                }
            }
            if (counter == 0)
            {
                string[] words = line.Split(" (Superstar Card)");
                string superStarName = words[0];
                List<object> superStarAttributes = SearchSuperStar(superStarName);
                SuperStar super = new SuperStar((string)superStarAttributes[0], (int)superStarAttributes[1],
                    (int)superStarAttributes[2], (string)superStarAttributes[3]);
                cardsSuperstarList.Add(super);                
                counter += 1;
            }
        }
        r.Close();
        // Necesito retornar el arr de cartas del mazo
        // Necesito retornar la SuperEstrella
        cardsSuperstarList.Add(arr);
        return cardsSuperstarList;
    }
    
    // Retorna la lista de atributos de la carta.
    private static List<object> SearchCardName(string cardName)
    {
        List<object> r = new List<object>();
        foreach (var card in ReadCardInfos())
        {
            if (card.Title == cardName)
            {
                // Skill poder = card.CardInfo.createEffect();
                
                r.Add(card.Title);
                r.Add(card.Types);
                r.Add(card.Subtypes);
                r.Add(card.Fortitude);
                r.Add(card.Damage);
                r.Add(card.StunValue);
                r.Add(card.CardEffect);
                r.Add(card.CardInfo);
                // r.Add(poder);

                
                // ReadEffect probandoClase = new ReadEffect();
                // Object claseCreada = probandoClase.GiveEffect("ReverseSpecificCard");
                // Instanciar efecto
                // efecto = metodoInsatanciaEfecto(card.EffectClass)
                // r.Add(efecto) en vez de r.Add(card.CardEffect
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
                // Aca instanciar SuperStarAbility
                r.Add(super.SuperStarAbility);
            }
        }
        return r;
    }
    
    private static List<Player> ChooseDeck(Deck DeckOne, Deck DeckTwo)
    {
        Console.WriteLine($"There are two Decks available. The first one is:");
        DeckOne.Describe();
        
        Console.WriteLine("The second one available is:");
        DeckTwo.Describe();
        
        Console.WriteLine("\n Player 1 please choose your deck:");
        Console.WriteLine($"    (0) {DeckOne.SuperStar.Type} Deck");
        Console.WriteLine($"    (1) {DeckTwo.SuperStar.Type} Deck");

        List<Card> emptyList = new List<Card>();
        Hand hand1 = new Hand(emptyList);
        Hand hand2 = new Hand(emptyList);
        Player PlayerOne = new Player(0, null, null, new Ringside(emptyList), new RingArea(emptyList), hand1);
        Player PlayerTwo = new Player(1, null, null,new Ringside(emptyList), new RingArea(emptyList), hand2);
        int answer = AskForNumber(0, 1);
        if (answer == 0)
        {
            PlayerOne.Deck = DeckOne;
            List<Card> deckCardsPlayerOne = DeckOne.GetCards().ToList();
            PlayerOne.Arsenal = new Arsenal(deckCardsPlayerOne);
            
            PlayerTwo.Deck = DeckTwo;
            List<Card> deckCardsPlayerTwo = DeckTwo.GetCards().ToList();
            PlayerTwo.Arsenal = new Arsenal(deckCardsPlayerTwo);
            
            Console.WriteLine("Player 1 chooses Deck One. Deck Two is assigned automatically to Player 2");
            // Player PlayerOne = new Player(0, DeckOne);
            // Player PlayerTwo = new Player(1, DeckTwo);
        }
        else
        {
            PlayerOne.Deck = DeckTwo;
            List<Card> deckCardsPlayerOne = DeckTwo.GetCards().ToList();
            PlayerOne.Arsenal = new Arsenal(deckCardsPlayerOne);
            
            PlayerTwo.Deck = DeckOne;
            List<Card> deckCardsPlayerTwo = DeckOne.GetCards().ToList();
            PlayerTwo.Arsenal = new Arsenal(deckCardsPlayerTwo);
            Console.WriteLine("Player 1 chooses Deck Two. Deck One is assigned automatically to Player 2");
            // Player PlayerOne = new Player(0, DeckTwo);
            // Player PlayerTwo = new Player(1, DeckOne);
        }
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
        Console.WriteLine($"The Star Value for the first player is {starValuePlayerOne}.");
        Console.WriteLine($"The Star Value for the second player is {starValuePlayerTwo}.");
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
        // Puede usar superstar. EN PROCESO.
        // Draw Segment. LISTO
        // Main Segment
            // Usar SuperStar ability. EN PROCESO.
            // Jugar maneuver. LISTO
            // Jugar Action. IGNORAR
        // TUrn ends
            // Decide terminar. LISTO
            // Oponente revierte alguna carta jugada en el Main. IGNORAR
        bool gameOn = true;
        // Si superstarSkill es de tipo before, tirar habilidad.
        
        player.DrawCards(1);
        bool play = true;
        // Aca tengo q volver en el ciclo
        do
        {
            // VUELVE A ENTRAR AQUI NO SE PQ
            Console.WriteLine("The cards in your hand are the following:");
            player.GetHandCards();  // CAMBIO TRES
            Console.WriteLine("Select the card you want to play");
            int cardNumber = AskForNumber(0, player.GetHandLength() + 1);  // CAMBIO DOS
            if (cardNumber == 0)
            {
                Console.WriteLine($"Turn for player number {player.Number + 1} ends");
                Console.WriteLine($"El booleano play es: {play}");
                play = true;
            }
            else
            {
                List<Card> handList = player.GetHand();
                Card selectedCard = handList[cardNumber - 1];  // CAMBIO UNO
                List<string> cardTypes = selectedCard.Types;
                // Checkear si dentro de los cardTypes seleccionados es un maneuver
                bool maneuverPresent = cardTypes.Contains("Maneuver");
                // Si no es un maneuver, volver a preguntar numero. Hacer un ciclo. PREGUNTAR POR TERMINAR TURNO
                // Si es un maneuver.
                //      checkear su fortitude value y compararlo con el jugador,
                //      para ver si puede jugarla.
                if (maneuverPresent == true)
                {
                    //      Si no puede jugarlo. Volver a pregiuntar numero
                    //      Si puede jugarlo, hacer el daño al oponente y bajarlo al Ring Area
                    //          IMPORTANTE: actualizar fortitude del jugador al jugar una carta
                    int cardFortitude = Convert.ToInt32(selectedCard.Fortitude);
                    int playerFortitude = player.Fortitude;
                    if (cardFortitude <= playerFortitude)
                    {
                        // Player va a jugar carta
                        bool opponentHasReversal = opponent.HaveReversalInHand();
                        if (opponentHasReversal == true)
                        {
                            bool opponentHasReversalToPlay = opponent.PlayerHasAvailableReversals(selectedCard);
                            if (opponentHasReversalToPlay)
                            {
                                List<Card> availableReversals = opponent.GetAvailableReversals(selectedCard);
                                // Avisar y mostrarle cuales
                                Vista.HasAvailableReversalToPlay();
                                //
                                // Does the player want to play the reversal?
                                Vista.AskToPlayReversalOrNot();
                                Int32 doesPlayerWantToPlayReversal = Vista.AskForNumber(0, 1);
                                if (doesPlayerWantToPlayReversal == 1)
                                {
                                    Card choosenReversalToPlay = Vista.chooseCard(availableReversals);
                                    ReverseSKill reversalSkill = choosenReversalToPlay.CardSkill as ReverseSKill;
                                    // JUGAR EL REVERSAL. Esto implica:
                                        // La carta jugada NO tiene ningun efecto.
                                        // No se alcanzo a jugar la carta asiq ok.
                                        
                                        // La carta jugada NO causa ningun dano
                                        // No se alcanzo a jugar la carta asiq ok
                                        
                                        // La carta jugada es puesta en su Rinside
                                        // MOVER selectedCard a Ringside
                                        player.MoveCardToRingside(selectedCard);
                                        
                                        // LUEGO:
                                        // Se aplica el efecto del REVERSAL
                                        reversalSkill.UseAbility();
                                        
                                        // Se efectua el dano del reversal
                                        player.ReceiveDamage(choosenReversalToPlay);

                                        // El reversal queda puesto en el ring area
                                        opponent.PutDownReversalToRingArea(choosenReversalToPlay);

                                        // Se actualiza el fortitude rating del jugador que jugo el reversal.
                                        opponent.UpdateFortitude(Convert.ToInt32(choosenReversalToPlay.Fortitude));
                                }
                                else
                                {
                                    ///////////////////////////////////////////
                                    bool endGame = opponent.ReceiveDamage(selectedCard);
                                    if (endGame == true)
                                    {
                                        gameOn = false;
                                        break;
                                    }
                                    player.PlayManeuver(selectedCard);
                                    play = false;
                                    ///////////////////////////////////////////
                                    play = true;
                                }
                            }
                            else
                            {
                                // Avisar que los reversal que tiene no los puede jugar.
                                Vista.HasReversalButNotAvailable();
                                ///////////////////////////////////////////
                                bool endGame = opponent.ReceiveDamage(selectedCard);
                                if (endGame == true)
                                {
                                    gameOn = false;
                                    break;
                                }
                                player.PlayManeuver(selectedCard);
                                play = false;
                                ///////////////////////////////////////////
                            }
                        }
                        else
                        {
                            Vista.InformNoReversalInHand();
                            ///////////////////////////////////////////
                            bool endGame = opponent.ReceiveDamage(selectedCard);
                            if (endGame == true)
                            {
                                gameOn = false;
                                break;
                            }
                            player.PlayManeuver(selectedCard);
                            play = false;
                            ///////////////////////////////////////////
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("You dont have enough Fortitude level to play that card. Please select one that you can play.");
                        play = false;
                    }
                }
                else
                {
                    Console.WriteLine("The selected card is not a maneuver. Please select one that is.");
                    play = false;
                }
                //      Volver al ciclo, mencionar que puede jugar otra carta o terminar el turno
                
            }
       } while (play == false);

        return gameOn;
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