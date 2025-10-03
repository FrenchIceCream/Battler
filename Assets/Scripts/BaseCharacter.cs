using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class BaseCharacter : MonoBehaviour, IAttack
{
    [SerializeField] float moveSpeed = 1f;
    protected BaseStats stats;
    protected HealthComponent healthComp;

    protected List<AbilitySO> DamageTakenAbilities = new List<AbilitySO>();
    protected List<AbilitySO> ApplyingDamageAbilities = new List<AbilitySO>();

    public event EventHandler OnCharacterDied;

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

    protected virtual void Awake()
    {
        stats = new BaseStats();
        healthComp = new HealthComponent();
    }

    protected virtual void Start()
    {
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
        healthComp.AddHealth(value);
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

        if (!opponent.healthComp.IsDead())
            GameManager.attackState = GameManager.AttackState.Ready;
        else
            GameManager.attackState = GameManager.AttackState.FightFinished;
    }

    public void Attack(BaseCharacter opponent)
    {
        StartCoroutine(AttackCoroutine(opponent));
    }


    protected virtual int GetDamageTakenFromOpponent(BaseCharacter opponent)
    {
        Debug.Log("GetDamageTakenFromOpponent - not overriden");
        return 0;
    }
    protected virtual int GetOverallDamage(BaseCharacter opponent)
    {
        Debug.Log("GetOverallDamage = not overriden");
        return 0;

    }
    virtual protected void DoDamageToOpponent(BaseCharacter opponent)
    {
        int rand = UnityEngine.Random.Range(1, this.stats.Dexterity + opponent.stats.Dexterity + 1);
        if (rand <= opponent.stats.Dexterity)
        {
            AudioManager.Instance.PlayMissSoundOneShot();
            return;
        }

        opponent.AddHealth(-GetOverallDamage(opponent) + GetDamageTakenFromOpponent(opponent));
        AudioManager.Instance.PlayHitSoundOneShot();
}

    void Die()
    {
        OnCharacterDied?.Invoke(this, EventArgs.Empty);

        GameManager.attackState = GameManager.AttackState.Paused;
    }
}
