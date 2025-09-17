using UnityEngine;

public class Enemy : BaseCharacter
{
    override protected void DoDamageToOpponent(BaseCharacter opponent)
    {
        opponent.AddHealth(-1);
        Debug.Log("Attack (enemy)");
    }
}
