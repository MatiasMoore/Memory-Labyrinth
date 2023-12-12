using UnityEngine;

namespace MemoryLabyrinth.SaveLoad
{

    public interface ISaveLoader
    {
        void LoadData();
        void SaveData();
    }

    public sealed class SaveLoadManager : MonoBehaviour
    {
        private readonly ISaveLoader[] saveLoaders = {
        new BonusesSaveLoader(),
        new LevelsSaveLoader(),
        new SettingsSaveLoader() 
    };

        public static SaveLoadManager Instance;

        public void Init()
        {
            if (Instance != null)
                return;
            Instance = this;
        }


        [ContextMenu("Load")]
        public void LoadGame()
        {
            Repository.LoadState();
            foreach (var saveLoader in this.saveLoaders)
            {
                saveLoader.LoadData();
            }
        }

        [ContextMenu("Save")]
        public void SaveGame()
        {
            foreach (var saveLoader in this.saveLoaders)
            {
                saveLoader.SaveData();
            }
            Repository.SaveState();
        }

        [ContextMenu("Delete")]
        public void DeleteSave()
        {
            Repository.ClearSave();
        }
    }
}