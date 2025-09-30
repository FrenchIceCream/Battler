using UnityEngine;

public class Enemy : BaseCharacter
{
    [SerializeField] EnemySO enemySO;

    public EnemySO GetEnemySO() { return enemySO; }

    protected override void Awake()
    {
        base.Awake();
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
            damageFromAbilities += abilitySO.Apply(opponent as Player, this);
        }
        return enemySO.damage + stats.Strength + damageFromAbilities;
    }

    int GetDamageTakenFromOpponent(BaseCharacter opponent)
    {

        int damageFromAbilities = 0;
        foreach (AbilitySO abilitySO in DamageTakenAbilities)
        {
            damageFromAbilities += abilitySO.Apply(opponent as Player, this);
        }
        return damageFromAbilities;
    }

    override protected void DoDamageToOpponent(BaseCharacter opponent)
    {
        opponent.AddHealth(-GetOverallDamage(opponent) + GetDamageTakenFromOpponent(opponent));
    }
}
