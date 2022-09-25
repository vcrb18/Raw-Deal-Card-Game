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