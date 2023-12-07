using MemoryLabyrinth.Level.Objects.FinishLib;
using UnityEngine;

namespace MemoryLabyrinth.Player
{
    public class PlayerFinishCompatible : FinishCompatible
    {
        public override void OnFinish(Collider2D collider)
        {
            MainCharacter mainCharacter = collider.gameObject.GetComponent<MainCharacter>();
            if (mainCharacter != null)
            {
                mainCharacter.Finish();
            }
            else
            {
                Debug.Log($"{collider.gameObject.name} not a main character");
            }
        }
    }
}