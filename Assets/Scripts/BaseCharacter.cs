using UnityEngine;

public class BaseCharacter : MonoBehaviour, IAttack
{
    protected BaseStats stats;
    protected HealthComponent healthComp;
    public BaseStats GetStats()
    {
        return stats;
    }

    void Awake()
    {
        stats = new BaseStats();
        stats.SetInitialStats();
        healthComp = new HealthComponent();
    }

    void Start()
    {
        //TODO change value
        healthComp.SetMaxHealth(10);

        healthComp.OnHealthChanged += HealthComp_OnHealthChanged;
    }

    private void HealthComp_OnHealthChanged(object sender, HealthComponent.OnHealthChangedArgs e)
    {
        //TODO visual updates

        if (e.newHealth == 0)
            Die();
    }

    virtual public void Attack()
    {
        Debug.Log("Attack");
    }

    void Die()
    {
        //TODO
    }
}
