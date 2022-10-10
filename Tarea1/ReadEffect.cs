namespace Tarea1;

public class ReadEffect
{

    public string GetThisNamespace()
    {
        return GetType().Namespace;
    }
    public Skill ReturnEffectClass(string effectClass, List<string> attributes)
    {
        string namespaceDotClass = "Tarea1." + effectClass;
        object myObj;
        if (effectClass == "ReverseSubtypeManeuver")
        {
            myObj = Activator.CreateInstance(Type.GetType(namespaceDotClass), attributes[0]);
        }
        else
        {
            myObj = Activator.CreateInstance(Type.GetType(namespaceDotClass));
        }
        // Console.WriteLine(myObj.GetType().GetProperties());
        
        return (Skill)myObj;
    }

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
