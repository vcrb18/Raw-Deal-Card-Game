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

    public override void UseAbility(Player player, Player opponent)
    {
        opponent.ReceiveDamage(1, true);
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

public class UndertakerSkill : SuperStarSkill
{
    public UndertakerSkill(string useCondition, string whenCondition)
        : base(useCondition, whenCondition)
    {
    }

    public override void UseAbility(Player player, Player opponent)
    {
        // Player discard TWO cards
        List<Card> playerHand = player.MyHand.Cards;
        for (int i = 2; i > 0; i--)
        {
            Vista.InformThatPlayerMustDiscard(player, i);
            int choosenIdPlayer = Vista.ChooseCardIDToDiscard(playerHand);
            Card playerChoosenCardToDiscard = playerHand[choosenIdPlayer];
            player.DiscardCard(playerChoosenCardToDiscard);
            
            // TENGO QUE VOLVER A INFROMAR
        }
        
        // Player choose 1 card from Ringside
        // and put it on the hand
        List<Card> playerRingsideCards = player.Ringside.Cards;
        int choosenIdCard = Vista.PlayerCanTakeACard(player, playerRingsideCards, 1);
        Card choosenCard = playerRingsideCards[choosenIdCard];
        player.TakeCardFromRingside(choosenCard);
        player.AddCardToHand(choosenCard);
    }
}

public class TheRockSkill : SuperStarSkill
{
    public TheRockSkill(string useCondition, string whenCondition)
        : base(useCondition, whenCondition)
    {
    }

    public override void UseAbility(Player player, Player opponent)
    {
        // Player choose 1 card from Ringside
        List<Card> playerRingsideCards = player.Ringside.Cards;
        int choosenIdCard = Vista.PlayerCanTakeACard(player, playerRingsideCards, 1);
        Card choosenCard = playerRingsideCards[choosenIdCard];
        player.TakeCardFromRingside(choosenCard);
        
        // and put it on the BOTTOM of the arsenal
        player.MoveCardToBottomArsenal(choosenCard);

    }
}

public class MankindSkill : SuperStarSkill
{
    public MankindSkill(string useCondition, string whenCondition)
        : base(useCondition, whenCondition)
    {
    }

    public override void UseAbility(Player player, Player opponent)
    {
        Console.WriteLine("I am MANKIND, my ability is always active");
    }
}

public class ActionSKill : Skill
{
    public override void UseAbility(Player player, Player opponent)
    {
        throw new NotImplementedException();
    }
}

public class HmmmSkill : Skill
{
    public override void UseAbility(Player player, Player opponent)
    {
        throw new NotImplementedException();
    }
}

public abstract class ReverseSkill : Skill
{
    public abstract bool fullfillConditionOne(Card opponentCard);
    public override void UseAbility(Player player, Player opponent)
    {
        throw new NotImplementedException();
    }
}

public class ReverseSubtypeManeuver : ReverseSkill
{
    public string SubtypeManeuver;

    public ReverseSubtypeManeuver(string subtypeManeuver)
    {
        SubtypeManeuver = subtypeManeuver;
    }

    public override void UseAbility(Player player, Player opponent)
    {
        Vista.ReversalWithNoAdditionalEffect();
    }

    public override bool fullfillConditionOne(Card opponentCard)
    {
        if (opponentCard.Subtypes.Contains(SubtypeManeuver))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class ReverseAnyAction : ReverseSkill
{
    public override bool fullfillConditionOne(Card opponentCard)
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

