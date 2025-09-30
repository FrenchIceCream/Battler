using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<EnemySO> enemiesList;
    [SerializeField] WeaponSelectionUI weaponSelectionUI;
    [SerializeField] ClassSelectionUI classSelectionUI;
    [SerializeField] GameObject deathScreenObject;

    bool readyForNewBattle;
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
        deathScreenObject.SetActive(true);
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
            case AttackState.FightFinished:
                if (!readyForNewBattle)
                    break;
                attackState = AttackState.Paused;
                StartNewBattle();
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

    void StartNewBattle()
    {
        if (player.GetPlayerLevel() < 3)
        {
            classSelectionUI.SetCharacterClasses(() => { ResetBattle(); });
            classSelectionUI.Show();
        }
        else
        {
            ResetBattle();
            attackState = AttackState.Ready;
        }
    }

    void ResetBattle()
    {
        Debug.Log("Reset battle");
        player.ResetHealth();
        Destroy(this.enemy.gameObject);

        roundNumber = 1;
        int id = Random.Range(0, enemiesList.Count);
        GameObject enemy = Instantiate(enemiesList[id].enemyPrefab, new Vector3(5, 0, 0), Quaternion.identity);
        this.enemy = enemy.GetComponent<Enemy>();
        this.enemy.OnCharacterDied += Enemy_OnCharacterDied;

        attackingParty = IsPlayerFirst() ? this.player : this.enemy;
    }

    bool IsPlayerFirst()
    {
        return player.GetStats().Dexterity >= enemy.GetStats().Dexterity;
    }

    public void SetReadyForNewBattle()
    {
        readyForNewBattle = true;
    }

    public void RestartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    { 
        Application.Quit();
    }
}
