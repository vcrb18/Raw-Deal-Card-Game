using System.Reflection;

namespace Tarea1;


public class CardInfo
{
    public string ClassName { get; set; }
    
    // Atributos siempre en orden de la respectiva Clase
    public List<object> Attributes { get; set; }

    public Skill createEffect()
    {
        int counter = 0;
        ReadEffect leeEfecto = new ReadEffect();
        Skill claseCreada = leeEfecto.ReturnEffectClass(ClassName);
        PropertyInfo[] attributesList = claseCreada.GetType().GetProperties();
        foreach (PropertyInfo property in attributesList)
        {
            property.SetValue(claseCreada, Attributes[counter]);
        }

        return claseCreada;
    }
}


public class Card
{
    public string Title { get; set; }
    public List<string> Types { get; set; }
    public List<string> Subtypes { get; set; }
    public string Fortitude { get; set; }
    public string Damage { get; set; }
    public string StunValue { get; set; }
    public string CardEffect { get; set; }
    
    public CardInfo CardInfo { get; set; }
    
    // Me falta el skill!
    public Skill CardSkill;

    public Card(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect, CardInfo cardInfo, Skill cardSkill)
    {
        Title = title;
        Types = types;
        Subtypes = subtypes;
        Fortitude = fortitude;
        Damage = damage;
        StunValue = stunValue;
        CardEffect = cardEffect;
        CardInfo = cardInfo;
        CardSkill = cardSkill;
    }
}