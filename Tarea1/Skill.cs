namespace Tarea1;

public class Skill
{
    // AUn no se que atributos en comun pueden tener todos los skill
}

// Podrian haber dos clases de move Skill,
// Las que mueven de un lado a otro x cartas y era
// Las que dp mueven denuevo x cartas de un lado a otro y era
public class MoveSkill : Skill
{
    public DeckElement MoveTo;
    public DeckElement MoveFrom;
    public Int32 HowManyCards;

    public MoveSkill(DeckElement moveTo, DeckElement moveFrom, Int32 howManyCards)
    {
        MoveTo = moveTo;
        MoveFrom = moveFrom;
        HowManyCards = howManyCards;
    }
}

// Habilidades de draw o discard
public abstract class MoveSuperstarSkill : MoveSkill
{
    abstract public void Move();
    public String When;

    public MoveSuperstarSkill(DeckElement moveTo, DeckElement moveFrom, Int32 howManyCards, String when)
        : base(moveTo, moveFrom, howManyCards)
    {
        When = when;
    }
}

// Por ahora esto es discard.
public class MoveSuperstarSkllOnce : MoveSuperstarSkill
{
    public MoveSuperstarSkllOnce(DeckElement moveTo, DeckElement moveFrom, Int32 howManyCards, String when)
        : base(moveTo, moveFrom, howManyCards, when)
    {
        
    }

    public override void Move()
    {
        throw new NotImplementedException();
    }
}

// Por ahora esto es draw.
public class MoveSuperstarSkillTwice : MoveSuperstarSkill
{
    public DeckElement MoveToSecondTime;
    public DeckElement MoveFromSecondTime;

    public MoveSuperstarSkillTwice(DeckElement moveTo, DeckElement moveFrom, Int32 howManyCards, String when,
        DeckElement moveToSecondTime, DeckElement moveFromSecondTime)
        : base(moveTo, moveFrom, howManyCards, when)
    {
        MoveFromSecondTime = moveFromSecondTime;
        MoveToSecondTime = moveToSecondTime;
    }

    public override void Move()
    {
        throw new NotImplementedException();
    }
}