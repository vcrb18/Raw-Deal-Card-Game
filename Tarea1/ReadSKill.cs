namespace Tarea1;

public abstract class ReadSKill
{
    // CreateObjectSkill() tiene que devolver un Skill. Cambiar dp
    public abstract void CreateObjectSKill();
    public String Skill;

    public ReadSKill(String skill)
    {
        Skill = skill;
    }
    
}

public class ReadSuperstarSkill : ReadSKill
{
    public ReadSuperstarSkill(String skill)
        : base(skill)
    {
    }

    // IDEAS:
    // arr = line.Split(',')
        // arr[0]: Puede contener 4 opciones:
            // (1): Once
            // (2): May
            // (3): Must
            // (4): None
            // Podrian ser cuatro clases distintas? Sino, seria un atributo me imagino.
        // arr[1]: 
        // Si es opcion/clase (1)
            // Puede contener "draw" o "discard"
            // 
            // ("discard"): Es el ultimo elemento de la lista
                //
            // MoveSuperstarSkillTwice.Object()
            // ("draw"): Accion 1 = MoveFrom Arsenal(arriba) to hand
                // Pasar a mirar el arr[2]:
                // Estos por ahora no tienen numero, asique lo dejaremos asi nomas. OJO
                //      Contiene la siguiente info: MoveFrom y MoveTo. 
                //      La logica es buscar ElementosMesa:
                //      El primero que aparezca es MoveFrom
                //      El segundo que aparezca es MoveTo
                //      if (aparece Arsenal): 
                //          Ver si es top o bottom.
                
        
        public override void CreateObjectSKill()
    {
        // Codigo que instancia una clase segun el string
        // enum Tipo
        // {
        //     "Once",
        //     "None",
        //     "You",
        //     "At"
        // }
        String during = Skill.Substring(0, 4);  // Also works for "None"
        String start = Skill.Substring(0, 2);
        String always = Skill.Substring(0, 3);
        if (during == "once")
        {
            
        }
        else if (during == "None")
        {
            
        }
        else if (start == "At")
        {
            
        }
        else if (always == "You")
        
        throw new NotImplementedException();
    }
}