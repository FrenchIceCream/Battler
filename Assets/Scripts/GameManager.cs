using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<EnemySO> enemiesList;

    Player player;
    Enemy enemy;

    BaseCharacter attackingParty;

    public enum AttackState
    {
        Ready, Busy, Paused
    }

    public static AttackState attackState = AttackState.Paused;
    public static int roundNumber = 1;

    void Start()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(-5, 0, 0), Quaternion.identity);
        int id = Random.Range(0, enemiesList.Count);
        GameObject enemy = Instantiate(enemiesList[id].enemyPrefab, new Vector3(5, 0, 0), Quaternion.identity);

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
            default:
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
        {
            roundNumber++;
            attackingParty = player;
        }
            
    }

    bool IsPlayerFirst()
    {
        return player.GetStats().Dexterity >= enemy.GetStats().Dexterity;
    }

}
