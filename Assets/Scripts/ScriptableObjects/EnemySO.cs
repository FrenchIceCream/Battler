using UnityEngine;

[CreateAssetMenu()]
public class EnemySO : ScriptableObject
{
    public GameObject enemyPrefab;
    public int health;
    public int damage;
    public int strength;
    public int dexterity;
    public int stamina;
    //TODO add abilities
    public WeaponSO weaponAward;
}
