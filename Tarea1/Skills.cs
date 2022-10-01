using System.Security.Cryptography.X509Certificates;

namespace Tarea1;

public abstract class Skill
{
    public void GetProperties()
    {
        Console.WriteLine(this.GetType().GetProperties());
    }

    public abstract void UseAbility();

}

public class Ignore : Skill
{
    public override void UseAbility()
    {
        throw new NotImplementedException();
    }
}
public abstract class ReverseSkill : Skill
{
    abstract public bool fullfillConditionOne(Card opponentCard);
    public override void UseAbility()
    {
        throw new NotImplementedException();
    }
}

public class ReverseNotImplemented : ReverseSkill
{
    public override bool fullfillConditionOne(Card opponentCard)
    {
        return false;
    }
}
public class ReverseSpecificCard : ReverseSkill
{
    public string CardTitle { get; set; }

    // public ReverseSpecificCard(string cardTitle)
    // {
    //     CardTitle = cardTitle;
    // }

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

    public override void UseAbility()
    {
        Vista.ThisCardHasNoExtraEffect();
    }
}

