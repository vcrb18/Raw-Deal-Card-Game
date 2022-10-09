using System.Security.Cryptography.X509Certificates;

namespace Tarea1;

public abstract class Skill
{
    public abstract void UseAbility();
    public void GetProperties()
    {
        Console.WriteLine(this.GetType().GetProperties());
    }
}

public class Ignore : Skill
{
    public override void UseAbility()
    {
        throw new NotImplementedException();
    }
}

public class SuperStarSkill : Skill
{
    public string UseCondition { get; set; }  // Metodo asociado
    public override void UseAbility()
    {
        throw new NotImplementedException();
    }

    public void IsAvailable()
    {
        
    }
}

public class KaneSkill : SuperStarSkill
{
    // UseCondition = beginning
    public string UseCondition = "beginning";

    public bool IsAvailableInDrawSegment()
    {
        return true;
    }

    public bool IsAvailableInMainSegment()
    {
        return false;
    }

    public void UseAbility(Player opponent)
    {
        Arsenal arsenalFromOpponent = opponent.Arsenal;
        Ringside ringsideFromOpponent = opponent.Ringside;
        Card card = opponent.takeTopCardFromArsenal();
        opponent.Ringside.AddCard(card);







    }
}

public class MoveTwiceSkill : SuperStarSkill
{
    public string MoveFrom { get; set; }
    public string MoveTo { get; set; }
    public string HowMany { get; set; }
    public string MoveFromSecondTime { get; set; }
    public string MoveToSecondTime { get; set; }
    public string HowManySecondTime { get; set; }

    public override void UseAbility()
    {
        throw new NotImplementedException();
    }
}

public class MoveOnceSkill : SuperStarSkill
{
    public string MoveFrom { get; set; }
    public string MoveTo { get; set; }
    public string HowMany { get; set; }
    public override void UseAbility()
    {
        throw new NotImplementedException();
    }
}

public class NoSkill : SuperStarSkill
{
    public override void UseAbility()
    {
        Vista.HasNoAbility();
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

