using UnityEngine;

public class Enemy : BaseCharacter
{
    [SerializeField] EnemySO enemySO;

    private void Awake()
    {
        healthComp = new HealthComponent();
        stats = new BaseStats();
        GetStats().Dexterity = enemySO.dexterity;
        GetStats().Strength = enemySO.strength;
        GetStats().Stamina = enemySO.stamina;
    }

    protected override void Start()
    {
        healthComp.SetMaxHealth(enemySO.health);
        healthComp.OnHealthChanged += HealthComp_OnHealthChanged;
    }

    override protected void DoDamageToOpponent(BaseCharacter opponent)
    {
        opponent.AddHealth(-enemySO.damage);
        Debug.Log("Attack (enemy)");
    }
}
