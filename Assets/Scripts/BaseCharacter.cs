using UnityEngine;

public class BaseCharacter : MonoBehaviour, IAttack
{
    protected BaseStats stats;
    public BaseStats GetStats()
    {
        return stats;
    }

    virtual public void Attack()
    {
        Debug.Log("Attack");
    }
}
