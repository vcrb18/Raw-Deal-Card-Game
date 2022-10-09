using System.Reflection;
using System.Text.Json;

namespace Tarea1;


public class CardInfo
{
    public string ClassName { get; set; }
    
    // Atributos siempre en orden de la respectiva Clase
    public List<object> Attributes { get; set; }

    public Skill CreateSkillInstance()
    {
        ReadEffect leeEfecto = new ReadEffect();
        Skill claseCreada = leeEfecto.ReturnEffectClass(ClassName);
        return claseCreada;
    }
    
    public Skill createEffect()
    {
        // JsonSerializer.Deserialize<Object[]>(Attributes);
        int counter = 0;
        ReadEffect leeEfecto = new ReadEffect();
        Skill claseCreada = leeEfecto.ReturnEffectClass(ClassName);
        PropertyInfo[] attributesList = claseCreada.GetType().GetProperties();
        List<string> attributesInStrings = setAttributesToString();
        foreach (PropertyInfo property in attributesList)
        {
            property.SetValue(claseCreada, attributesInStrings[counter]);
            counter++;
        }

        return claseCreada;
    }

    public List<string> setAttributesToString()
    {
        List<string> attributesInStrings = Attributes.Select(s => s.ToString()).ToList();
        return attributesInStrings;
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
    public Skill CardSkill = new Ignore();
    // Esta generando problemas.

    public Card(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect, CardInfo cardInfo)
    {
        Title = title;
        Types = types;
        Subtypes = subtypes;
        Fortitude = fortitude;
        Damage = damage;
        StunValue = stunValue;
        CardEffect = cardEffect;
        CardInfo = cardInfo;
    }

    public void setSkill()
    {
        Skill cardSkillToSet = CardInfo.createEffect();
        CardSkill = cardSkillToSet;
    }
}