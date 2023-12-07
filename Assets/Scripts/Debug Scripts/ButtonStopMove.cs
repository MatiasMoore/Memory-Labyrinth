using MemoryLabyrinth.Player;
using UnityEngine;

public class ButtonStopMove : MonoBehaviour
{
    public MainCharacter Player;
    void OnMouseDown()
    {
        Player.StopMoving();
        Debug.Log("Stop moving!");
    }

}



