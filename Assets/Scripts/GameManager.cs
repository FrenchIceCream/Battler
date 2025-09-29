using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<EnemySO> enemiesList;
    [SerializeField] WeaponSelectionUI weaponSelectionUI;
    [SerializeField] ClassSelectionUI classSelectionUI;


    Player player;
    Enemy enemy;

    BaseCharacter attackingParty;

    public enum AttackState
    {
        Ready, Busy, Paused, FightFinished
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

        this.player.OnCharacterDied += Player_OnCharacterDied; ;
        this.enemy.OnCharacterDied += Enemy_OnCharacterDied;

        attackingParty = IsPlayerFirst() ? this.player : this.enemy;
    }

    private void Enemy_OnCharacterDied(object sender, System.EventArgs e)
    {
        weaponSelectionUI.SetWeaponOnCards(player.GetWeaponSO(), enemy.GetEnemySO().weaponAward);
        weaponSelectionUI.Show();
    }

    private void Player_OnCharacterDied(object sender, System.EventArgs e)
    {
        Debug.Log("You're dead");
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
            //case AttackState.FightFinished:
                
            //    attackState = AttackState.Paused;
            //    break;
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
