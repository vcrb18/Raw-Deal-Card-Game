namespace Tarea1;

public class SuperStar
{
    public string Type { get; set; }
    public int HandSize { get; set; }
    public int StarValue { get; set; }
    public string SuperStarAbility { get; set; }
    public SuperStar(string type, int handSize, int starValue, string superStarAbility)
    {
        Type = type;
        HandSize = handSize;
        StarValue = starValue;
        SuperStarAbility = superStarAbility;
    }
}