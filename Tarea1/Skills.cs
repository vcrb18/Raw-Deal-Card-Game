using System.Security.Cryptography.X509Certificates;

namespace Tarea1;

public abstract class Skill
{
    public abstract void UseAbility(Player player, Player opponent);
    public void GetProperties()
    {
        Console.WriteLine(this.GetType().GetProperties());
    }
}

public class Ignore : Skill
{
    public override void UseAbility(Player player, Player opponent)
    {
        throw new NotImplementedException();
    }
}

public class SuperStarSkill : Skill
{
    public string UseCondition; // Metodo asociado
    public string WhenCondition;

    public SuperStarSkill(string useCondition, string whenCondition)
    {
        UseCondition = useCondition;
        WhenCondition = whenCondition;
    }
    public override void UseAbility(Player player, Player opponent)
    {
        throw new NotImplementedException();
    }
}

public class HHHSkill : SuperStarSkill
{

    public HHHSkill(string useCondition, string whenCondition)
    : base(useCondition, whenCondition)
    {
    }
    public override void UseAbility(Player player, Player opponent)
    {
        throw new NotImplementedException();
    }
}

public class KaneSkill : SuperStarSkill
{
    // UseCondition = beginning

    public KaneSkill(string useCondition, string whenCondition)
    : base(useCondition, whenCondition)
    {
    }
    public bool IsAvailableInDrawSegment()
    {
        return true;
    }

    public bool IsAvailableInMainSegment()
    {
        return false;
    }

    public override void UseAbility(Player player, Player opponent)
    {
        opponent.ReceiveDamage(1);
    }
}

public class JerichoSkill : SuperStarSkill
{
    public JerichoSkill(string useCondition, string whenCondition)
        : base(useCondition, whenCondition)
    {
    }

    public override void UseAbility(Player player, Player opponent)
    {
        // Player discard card
        List<Card> playerHand = player.MyHand.Cards;
        Vista.InformThatPlayerMustDiscard(player, 1);
        int choosenIdPlayer = Vista.ChooseCardIDToDiscard(playerHand);
        Card playerChoosenCardToDiscard = playerHand[choosenIdPlayer];
        player.DiscardCard(playerChoosenCardToDiscard);
        
        // Opponent discard card
        List<Card> opponentHand = opponent.MyHand.Cards;
        Vista.InformThatPlayerMustDiscard(opponent, 1);
        int choosenIdOpponent = Vista.ChooseCardIDToDiscard(opponentHand);
        Card opponentChoosenCardToDiscard = opponentHand[choosenIdOpponent];
        opponent.DiscardCard(opponentChoosenCardToDiscard);
    }
}

public class StoneColdSkill : SuperStarSkill
{
    public StoneColdSkill(string useCondition, string whenCondition)
        : base(useCondition, whenCondition)
    {
    }

    public override void UseAbility(Player player, Player opponent)
    {
        // Player draw a card
        Vista.InformThatPlayerCanDrawCard(player, 1);
        player.DrawCards(1);
        
        // Player discard card TO BOTTOM ARSENAL
        List<Card> playerHand = player.MyHand.Cards;
        Vista.InformThatPlayerMustDiscard(opponent, 1);
        int choosenIdPlayer = Vista.ChooseCardIDToDiscard(playerHand);
        Card playerChoosenCardToDiscard = playerHand[choosenIdPlayer];
        player.DiscardCard(playerChoosenCardToDiscard);
        player.MoveCardToBottomArsenal(playerChoosenCardToDiscard);
    }
}

// public class MoveTwiceSkill : SuperStarSkill
// {
//     public string MoveFrom { get; set; }
//     public string MoveTo { get; set; }
//     public string HowMany { get; set; }
//     public string MoveFromSecondTime { get; set; }
//     public string MoveToSecondTime { get; set; }
//     public string HowManySecondTime { get; set; }
//
//     public override void UseAbility(Player opponent)
//     {
//         throw new NotImplementedException();
//     }
// }
//
// public class MoveOnceSkill : SuperStarSkill
// {
//     public string MoveFrom { get; set; }
//     public string MoveTo { get; set; }
//     public string HowMany { get; set; }
//     public override void UseAbility(Player opponent)
//     {
//         throw new NotImplementedException();
//     }
// }
//
// public class NoSkill : SuperStarSkill
// {
//     public override void UseAbility(Player opponent)
//     {
//         Vista.HasNoAbility();
//     }
// }


public abstract class ReverseSkill : Skill
{
    abstract public bool fullfillConditionOne(Card opponentCard);
    public override void UseAbility(Player player, Player opponent)
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

    public override void UseAbility(Player player, Player opponent)
    {
        Vista.ThisCardHasNoExtraEffect();
    }
}

