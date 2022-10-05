using System.Linq;


namespace Tarea1;

public class Deck
{
    public Card[] Cards;
    public SuperStar SuperStar;

    public Deck(Card[] cards, SuperStar superstar)
    {
        Cards = cards;
        SuperStar = superstar;
    }

    public void Describe()
    {
        Console.WriteLine($"The deck with the Super Star {SuperStar.Type} !!!");
    }
    

    public Card[] GetCards()
    {
        return Cards;
    }
    
    // No Funciona xD    
    public void ShuffleCards()
    {
        Random rnd = new Random();
        for (int i = 0; i < Cards.Count(); i++)
        {
            int j = rnd.Next(0, Cards.Count());
            Card temp = Cards[i];
            Cards[i] = Cards[j];
            Cards[j] = temp;
        }
    }

    public bool CheckIfDeckIsValid()
    {
        if (checkSixtyCards() == false || checkRepeatedCards() == false || checkHeelAndFaceExistence() == false ||
            CheckLogo() == false)
        {
            Vista.DeckIsInvalid(SuperStar.Type);
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool checkSixtyCards()
    {
        if (Cards.Length != 60)
        {
            Console.WriteLine($"{SuperStar.Type} falla en 60 cartas");
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool checkRepeatedCards()
    {
        bool deckIsCorrect = true;
        for (int firstCardPosition = 0; firstCardPosition < Cards.Length; firstCardPosition++)
        {
            int firstCardCounter = 1;
            Card firstCard = Cards[firstCardPosition];
            string firstCardname = firstCard.Title;
            for (int secondCardPosition = 0; secondCardPosition < Cards.Length; secondCardPosition++)
            {
                string secondCardName = Cards[secondCardPosition].Title;
                if (firstCardPosition != secondCardPosition)
                {
                    if (secondCardName == firstCardname)
                    {
                        firstCardCounter++;
                    }
                }
            }

            if (firstCard.Subtypes.Contains("Unique"))
            {
                if (firstCardCounter > 1)
                {
                    Console.WriteLine($"{SuperStar.Type} falla en cartas repetidas");

                    deckIsCorrect = false;
                    break;
                }
            }
            else if (!firstCard.Subtypes.Contains("Setup"))
            {
                if (firstCardCounter > 3)
                {
                    Console.WriteLine($"{SuperStar.Type} falla en cartas repetidas");

                    deckIsCorrect = false;
                    break;
                }
            }
        }
        return deckIsCorrect;
    }

    public bool checkHeelAndFaceExistence()
    {
        bool heelExists = checkIfHeelExists();
        bool faceExits = checkIfFaceExists();
        if (heelExists && faceExits)
        {
            Console.WriteLine($"{SuperStar.Type} falla en que tiene heel y face");

            return false;
        }
        else
        {
            return true;
        }
    }

    public bool checkIfHeelExists()
    {
        bool heelExists = false;
        foreach (var card in Cards)
        {
            if (card.Subtypes.Contains("Heel"))
            {
                heelExists = true;
            }
        }
        return heelExists;
    }

    public bool checkIfFaceExists()
    {
        bool faceExists = false;
        foreach (var card in Cards)
        {
            if (card.Subtypes.Contains("Face"))
            {
                faceExists = true;
            }
        }

        return faceExists;
    }

    public bool CheckLogo()
    {
        bool logoIsCorrect = true;
        string superstarName = this.superstarName();
        List<string> allLogosFound = this.allLogosFoundInDeck();
        foreach (var logoName in allLogosFound)
        {
            if (logoName != superstarName)
            {
                Console.WriteLine($"{SuperStar.Type} falla en tener logo incorrecto");

                logoIsCorrect = false;
            }
        }

        return logoIsCorrect;
    }

    public string superstarName()
    {
        string notFilteredSuperstarName = SuperStar.Type;
        if (notFilteredSuperstarName == "STONE COLD STEVE AUSTIN")
        {
            return "StoneCold";
        }
        else if (notFilteredSuperstarName == "THE UNDERTAKER")
        {
            return "Undertaker";
        }
        else if (notFilteredSuperstarName == "CHRIS JERICHO")
        {
            return "Jericho";
        }
        else if (notFilteredSuperstarName == "MANKIND")
        {
            return "Mankind";
        }
        else if (notFilteredSuperstarName == "HHH")
        {
            return "HHH";
        }
        else if (notFilteredSuperstarName == "THE ROCK")
        {
            return "TheRock";
        }
        else
        {
            return "Kane";
        }
        // else if (notFilteredSuperstarName == "KANE")
        // {
        //     return "Kane";
        // }
    }

    public List<string> allLogosFoundInDeck()
    {
        List<string> logosFoundInDeck = new List<string>();
        List<string> allLogosNames = getAllSuperstarsNames();
        foreach (var card in Cards)
        {
            List<string> cardSubtypes = card.Subtypes;
            IEnumerable<string> intersectionName = allLogosNames.Intersect((cardSubtypes));
            foreach (var superstarName in intersectionName)
            {
                logosFoundInDeck.Add(superstarName);
            }
            // Si carta.subtypes contiene cualquier
        }

        return logosFoundInDeck;
    }

    public List<string> getAllSuperstarsNames()
    {
        List<string> superstarsNamesList = new List<string>();
        superstarsNamesList.Add("StoneCold");   
        superstarsNamesList.Add("Undertaker");   
        superstarsNamesList.Add("Jericho");   
        superstarsNamesList.Add("Mankind");   
        superstarsNamesList.Add("HHH");   
        superstarsNamesList.Add("TheRock");   
        superstarsNamesList.Add("Kane");
        return superstarsNamesList;
    }

}