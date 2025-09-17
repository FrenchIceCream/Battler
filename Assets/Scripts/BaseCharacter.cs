using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour, IAttack
{
    [SerializeField] float moveSpeed = 1f;
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
        //TODO
        Debug.Log("Character died");
    }
}
