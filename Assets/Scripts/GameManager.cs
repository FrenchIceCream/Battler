using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] Transform playerSpawnPoint;

    GameObject player;
    GameObject enemy;

    Player playerComp;
    Enemy enemyComp;

    BaseCharacter attackingParty;
    void Start()
    {
        player = Instantiate(playerPrefab);
        player.transform.position = playerSpawnPoint.position - new Vector3(5, 0, 0);

        enemy = Instantiate(enemyPrefab);
        enemy.transform.position = playerSpawnPoint.position + new Vector3(5, 0, 0);

        playerComp = player.GetComponent<Player>();
        enemyComp = enemy.GetComponent<Enemy>();

        attackingParty = IsPlayerFirst() ? playerComp : enemyComp;
    }

    // Update is called once per frame
    void Update()
    {
        PerformAttack();
    }

    void PerformAttack()
    {
        attackingParty.Attack();
        ChangeAttackingParty();
    }

    void ChangeAttackingParty()
    {
        if (attackingParty.gameObject == playerComp.gameObject)
            attackingParty = enemyComp;
        else
            attackingParty = playerComp;
    }

    bool IsPlayerFirst()
    {
        var pDex = playerComp.GetStats().Dexterity;
        var eDex = enemyComp.GetStats().Dexterity;
        return pDex > eDex;
    }

}
