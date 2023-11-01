using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFollowPath : MonoBehaviour
{
    public MainCharacter Player;
    public List<Vector3> path;
    void OnMouseDown()
    {
        Player.FollowPath(path);
        Debug.Log("Follow path!");
    }
}
