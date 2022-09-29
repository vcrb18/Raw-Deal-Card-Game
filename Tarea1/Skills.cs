using System.Security.Cryptography.X509Certificates;

namespace Tarea1;

public class Skill
{
    public void GetProperties()
    {
        Console.WriteLine(this.GetType().GetProperties());
    }
    

}

public class Ignore : Skill
{
    
}
public abstract class ReverseSKill : Skill
{
    abstract public bool fullfillConditionOne(Card opponentCard);

}

public class ReverseSpecificCard : ReverseSKill
{
    public string CardTitle;

    public ReverseSpecificCard(string cardTitle)
    {
        CardTitle = cardTitle;
    }

    public override bool fullfillConditionOne(Card cardToReverse)
    {
        if (cardToReverse.Title == CardTitle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

