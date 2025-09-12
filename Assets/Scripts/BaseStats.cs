using UnityEngine;

public class BaseStats
{ 
    int health = 0;

    int strength = 0;
    int stamina = 0;
    int dexterity = 0;

    public int Health    { get { return health; }    set { health = value; } }
    public int Strength  { get { return strength; }  set { strength = value; } }
    public int Stamina   { get { return stamina; }   set { stamina = value; } }
    public int Dexterity { get { return dexterity; } set { dexterity = value; } }

    public void SetInitialStats()
    {
        Strength = Random.Range(1, 3);
        Stamina = Random.Range(1, 3);
        Dexterity = Random.Range(1, 3);
    }
}
