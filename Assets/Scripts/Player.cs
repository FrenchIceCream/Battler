using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : BaseCharacter
{
    override protected void DoDamageToOpponent(BaseCharacter opponent)
    {
        opponent.AddHealth(-2);
        Debug.Log("Attack (player)");
    }

    void ActivateEffects()
    {
        //TODO;
    }
}
