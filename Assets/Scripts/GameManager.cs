using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] Transform playerSpawnPoint;

    Player player;
    Enemy enemy;

    BaseCharacter attackingParty;

    public enum AttackState
    {
        Ready, Busy
    }

    public static AttackState attackState = AttackState.Ready;

    void Start()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(-5, 0, 0), Quaternion.identity);
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(5, 0, 0), Quaternion.identity);

        this.player = player.GetComponent<Player>();
        this.enemy = enemy.GetComponent<Enemy>();

        attackingParty = IsPlayerFirst() ? this.player : this.enemy;
    }

    // Update is called once per frame
    void Update()
    {
        switch (attackState)
        { 
            case AttackState.Ready:
                attackState = AttackState.Busy;
                PerformAttack();
                break;
            case AttackState.Busy:
                break;
        }
    }

    void PerformAttack()
    {
        attackingParty.Attack( IsPlayer() ? enemy : player);
        ChangeAttackingParty();
    }

    bool IsPlayer()
    {
        return attackingParty.gameObject == player.gameObject;
    }

    void ChangeAttackingParty()
    {
        if (IsPlayer())
            attackingParty = enemy;
        else
            attackingParty = player;
    }

    bool IsPlayerFirst()
    {
        var pDex = player.GetStats().Dexterity;
        var eDex = enemy.GetStats().Dexterity;
        return pDex > eDex;
    }

}
