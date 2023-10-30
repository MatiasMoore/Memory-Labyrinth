using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStopMove : MonoBehaviour
{
    public MainCharacter Player;
    void OnMouseDown()
    {
        Player.StopMoving();
        Debug.Log("Stop moving!");
    }

}



