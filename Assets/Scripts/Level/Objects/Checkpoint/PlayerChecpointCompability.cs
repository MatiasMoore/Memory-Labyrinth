using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecpointCompability : CheckpointCompatable
{
    public override void getCheckpoint(Checkpoint chekpoint)
    {
        MainCharacter mainCharacter = GetComponent<MainCharacter>();
        if (mainCharacter != null)
        {
            mainCharacter.getCheckpoint(chekpoint);
        }        
    }
}
