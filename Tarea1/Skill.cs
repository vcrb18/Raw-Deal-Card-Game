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
    abstract public void UseAbility(Player p);
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

    public override void UseAbility(Player p)
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

    public override void UseAbility(Player p)
    {
        // Programemos el draw a card
        //---------------------------//
        
        // Selecciono la carta a mover
        Card cardToMove = MoveFrom.Cards[0];
        // Se la tengo que sacar al MoveFrom
        MoveFrom.Cards.RemoveAt(0);
        // Se la tengo que agregar al Move to
        MoveTo.Cards.Add(cardToMove);
        // Listo, ya robe la carta
        
        // DILEMAS: Los ignoraremos x ahora, veamos si funciona priemero.
            // (1): Es al BOTTOM del arsenla'
            // (2): Tengo que preguntarle al usaurio que carta desea botar.
        // Segunda accion
        Card cardToMoveNumberTwo = MoveFrom.Cards[0];  // Supuesto 2 IGNORADO. CREAR FUNCION VISTA.
        // Se la saco al MoveFrom
        MoveFromSecondTime.Cards.Remove(cardToMoveNumberTwo);
        // Se la agrego al MoveTo
        MoveToSecondTime.Cards.Add(cardToMoveNumberTwo);  // Se la agrego al final
    }
}