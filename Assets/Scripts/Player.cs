using UnityEngine;

public class Player : BaseCharacter
{
    void Awake()
    {
        stats = new BaseStats();
        stats.SetInitialStats();
    }

    override public void Attack()
    {
        Debug.Log("Attack (player)");
    }
 
}
