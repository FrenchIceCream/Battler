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
    }

    protected override void Start()
    {
        base.Start();
        foreach (AbilitySO abilitySO in enemySO.abilities)
            abilitySO.ActivateAbility(this);
        healthComp.SetMaxHealth(enemySO.health);
    }

    protected override int GetOverallDamage(BaseCharacter opponent)
    {
        int damageFromAbilities = 0;
        foreach (AbilitySO abilitySO in ApplyingDamageAbilities)
        {
            damageFromAbilities += abilitySO.Apply(opponent as Player, this);
        }
        return enemySO.damage + stats.Strength + damageFromAbilities;
    }

    protected override int GetDamageTakenFromOpponent(BaseCharacter opponent)
    {

        int damageFromAbilities = 0;
        foreach (AbilitySO abilitySO in DamageTakenAbilities)
        {
            damageFromAbilities += abilitySO.Apply(opponent as Player, this);
        }
        return damageFromAbilities;
    }
}
