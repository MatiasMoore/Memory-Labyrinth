using MemoryLabyrinth.Level.Objects.CheckpointLib;

namespace MemoryLabyrinth.Player
{
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
}