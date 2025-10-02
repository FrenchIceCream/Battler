using UnityEngine;

public class BaseStats
{ 
    int strength = 0;
    int stamina = 0;
    int dexterity = 0;

    int minDefault = 1;
    int maxDefault = 3;

    public int Strength  { get { return strength; }  set { strength = value; } }
    public int Stamina   { get { return stamina; }   set { stamina = value; } }
    public int Dexterity { get { return dexterity; } set { dexterity = value; } }

    public void SetInitialStats()
    {
        Strength = Random.Range(minDefault, maxDefault + 1);
        Stamina = Random.Range(minDefault, maxDefault + 1);
        Dexterity = Random.Range(minDefault, maxDefault + 1);
    }
}
