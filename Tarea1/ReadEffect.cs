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
        if (effectClass == "ReverseSubtypeManeuver" || effectClass == "ReverseSubtypeManeuverSpecial" || effectClass == "ReverseCalledCleanBreak")
        {
            myObj = Activator.CreateInstance(Type.GetType(namespaceDotClass), attributes[0]);
        }
        else
        {
            myObj = Activator.CreateInstance(Type.GetType(namespaceDotClass));
        }
        return (Skill)myObj;
    }

}
