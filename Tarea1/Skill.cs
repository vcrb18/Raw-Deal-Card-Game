namespace Tarea1;

public class Skill
{
    // AUn no se que atributos en comun pueden tener todos los skill
}

public class MoveSkill : Skill
{
    public DeckElement MoveTo;
    public DeckElement MoveFrom;
    public Int32 HowMany;

    public MoveSkill(DeckElement moveTo, DeckElement moveFrom, Int32 howMany)
    {
        MoveTo = moveTo;
        MoveFrom = moveFrom;
        HowMany = howMany;
    }
}

public class MoveSuperstarSkill : MoveSkill
{
    public String When;

    public MoveSuperstarSkill(DeckElement moveTo, DeckElement moveFrom, Int32 HowMany, String when)
        : base(moveTo, moveFrom, HowMany)
    {
        When = when;
    }

    public void Move()
    {
        
    }
}