using UnityEngine;

public class Enemy : BaseCharacter
{
    void Awake()
    {
        stats = new BaseStats();
        stats.SetInitialStats();
    }

    override public void Attack()
    {
        Debug.Log("Attack (enemy)");
    }
}
