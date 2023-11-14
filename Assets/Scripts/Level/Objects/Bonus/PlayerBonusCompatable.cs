using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonusCompatible : BonusCompatible
{
    public override void getBonus(Bonus bonus)
    {
        MainCharacter mainCharacter = GetComponent<MainCharacter>();
        if (mainCharacter != null)
        {
            mainCharacter.getBonus(bonus);
        }
        else
        {
            Debug.Log($"Bonus {gameObject.name} failed");
        }
    }
}
