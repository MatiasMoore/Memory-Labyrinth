using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModel : MonoBehaviour
{
    [SerializeField]
    MainCharacter _mainCharacter;

    public void Start()
    {
        _mainCharacter.AddOnDamageAction(onPlayerDamage);
        _mainCharacter.AddOnDeathAction(onPlayerDeath);
        Debug.Log("LevelModel Awake");
    }

    public void onPlayerDeath()
    {
        Debug.Log("Player died");
    }

    public void onPlayerWin()
    {
        Debug.Log("Player won");
    }

    public void onPlayerDamage()
    {
        Debug.Log($"Player damaged");
    }

    public void onPlayerGetBonus()
    {
        Debug.Log($"Player get bonus");
    }
}
