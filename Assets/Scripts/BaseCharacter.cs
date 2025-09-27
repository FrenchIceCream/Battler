using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseCharacter : MonoBehaviour, IAttack
{
    [SerializeField] float moveSpeed = 1f;
    protected BaseStats stats;
    protected HealthComponent healthComp;

    protected List<AbilitySO> DamageTakenAbilities = new List<AbilitySO>();
    protected List<AbilitySO> ApplyingDamageAbilities = new List<AbilitySO>();

    //This function adds buffs that player uses when they're being attacked
    public void AddDamageTakenAbilities(AbilitySO abilitySO)
    {
        DamageTakenAbilities.Add(abilitySO);
    }

    //This function adds buffs that player uses when they're attacking the enemy
    public void AddApplyingDamageAbilities(AbilitySO abilitySO)
    {
        ApplyingDamageAbilities.Add(abilitySO);
    }


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

    protected virtual void Start()
    {
        //TODO change value
        healthComp.SetMaxHealth(10);

        healthComp.OnHealthChanged += HealthComp_OnHealthChanged;
    }

    protected void HealthComp_OnHealthChanged(object sender, HealthComponent.OnHealthChangedArgs e)
    {
        //TODO visual updates

        if (e.newHealth == 0)
            Die();
    }

    public void AddHealth(int value)
    {
        int damageFromAbilities = 0;
        foreach (AbilitySO abilitySO in DamageTakenAbilities)
        {
            damageFromAbilities += abilitySO.Apply();
        }

        healthComp.AddHealth(value + damageFromAbilities);
    }

    IEnumerator AttackCoroutine(BaseCharacter opponent)
    {
        float moveTime = 0f;
        Vector3 startPos = this.transform.position;
        Vector3 targetPos = opponent.transform.position;;

        //reaching the opponent
        while (Vector3.Distance(transform.position, targetPos) > 1f)
        {
            moveTime += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPos, targetPos, moveTime);

            yield return null;
        }

        DoDamageToOpponent(opponent);
        yield return new WaitForSeconds(1f);

        //getting back to where character started
        targetPos = startPos;
        startPos = this.transform.position;
        moveTime = 0f;
        while (Vector3.Distance(transform.position, targetPos) > 0)
        {
            moveTime += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPos, targetPos, moveTime);
            yield return null;
        }
        if (opponent.healthComp.IsDead())
            GameManager.attackState = GameManager.AttackState.FightFinished;
        else
            GameManager.attackState = GameManager.AttackState.Ready;
    }

    public void Attack(BaseCharacter opponent)
    {
        StartCoroutine(AttackCoroutine(opponent));
    }

    virtual protected void DoDamageToOpponent(BaseCharacter opponent)
    {
        Debug.Log("Attacking (not overriden)");
    }

    void Die()
    {
        
        Debug.Log("Character died");
    }
}
