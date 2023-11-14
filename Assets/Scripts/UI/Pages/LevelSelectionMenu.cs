using UnityEngine;

public class LevelSelectionMenu : MonoBehaviour
{
    public void LoadLevel(ResourceManager.Level level)
    {
        // TODO: load level prefab
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);

        LevelManager.FireLevelLoadAction();
    }
}
