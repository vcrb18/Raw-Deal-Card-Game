namespace Tarea1;

public class SuperStar
{
    public string Type { get; set; }
    public int HandSize { get; set; }
    public int StarValue { get; set; }
    public string SuperStarAbility { get; set; }
    
    public CardInfo CardInfo { get; set; }
    
    public SuperStarSkill Skill { get; set; }
    public SuperStar(string type, int handSize, int starValue, string superStarAbility, SuperStarSkill skill)
    {
        Type = type;
        HandSize = handSize;
        StarValue = starValue;
        SuperStarAbility = superStarAbility;
        Skill = skill;
    }
}