using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.UI.ButtonsLib;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MemoryLabyrinth.UI.LevelSelectionLib
{
    public class LevelSelectionMenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _parentObject;
        [SerializeField]
        private GameObject _buttonPrefab;

        private void Start()
        {
            GenerateLevelsButtons();
        }

        private void GenerateLevelsButtons()
        {
            GameObject levelButton = new GameObject();
            List<LevelData> levels = LevelDataStorage.Instance.GetLevelDataList();

            Debug.Log(levels.Count);

            for(int i = 1; i <= levels.Count; i++)
            {
                levelButton = Instantiate(_buttonPrefab);

                levelButton.transform.parent = _parentObject.transform;
                levelButton.transform.localScale = new Vector3(1f, 1f, 1f);
                levelButton.GetComponent<LevelDisplayingData>().SetName(levels[i-1].name);
                levelButton.GetComponent<LevelSelectButton>()._levelName = levels[i-1].name;
            }
        }
    }
}