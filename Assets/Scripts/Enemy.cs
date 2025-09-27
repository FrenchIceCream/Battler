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

        foreach (AbilitySO abilitySO in enemySO.abilities)
            abilitySO.ActivateAbility(this);
    }

    protected override void Start()
    {
        healthComp.SetMaxHealth(enemySO.health);
        healthComp.OnHealthChanged += HealthComp_OnHealthChanged;
    }

    int GetOverallDamage(BaseCharacter opponent)
    {
        int damageFromAbilities = 0;
        foreach (AbilitySO abilitySO in ApplyingDamageAbilities)
        {
            damageFromAbilities += abilitySO.Apply();
        }
        return enemySO.damage + stats.Strength + damageFromAbilities;
    }

    override protected void DoDamageToOpponent(BaseCharacter opponent)
    {

        opponent.AddHealth(-GetOverallDamage(opponent));
        Debug.Log("Attack (enemy)");
    }
}
