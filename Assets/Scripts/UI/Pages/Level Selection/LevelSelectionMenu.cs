using UnityEngine;

public class LevelSelectionMenu : MonoBehaviour
{
    public void LoadLevel(ResourceManager.Level level)
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
    }
}
