using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonusCompatible : BonusCompatible
{
    public override void getBonus(int value)
    {
        MainCharacter mainCharacter = GetComponent<MainCharacter>();
        if (mainCharacter != null)
        {
            mainCharacter.getBonus(value);
        }
        else
        {
            Debug.Log($"Bonus {gameObject.name} failed");
        }
    }
}
