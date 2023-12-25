using UnityEngine;

namespace MemoryLabyrinth.UI
{
    public static class MenuManager
    {
        public enum Page
        {
            MAIN,
            OPTIONS,
            ACHIEVEMENTS,
            PAUSE,
            WIN,
            LOSE,
            LEVEL_SELECTION,
            LEVEL_EDITOR_UPPER,
            LEVEL_EDITOR_SAVE,
            LEVEL_EDITOR_OBJECTS
        }

        public static void OpenPage(Page page)
        {
            SetActivePage(GetPageGameObject(page));
        }

        public static void ClosePage(Page page)
        {
            SetInactivePage(GetPageGameObject(page));
        }

        private static void SetActivePage(GameObject menuObject)
        {
            if (menuObject != null)
                menuObject.SetActive(true);
            else
                throw new System.Exception("MENU MANAGER: SetActivePage -> menuObject is null");
        }

        private static void SetInactivePage(GameObject menuObject)
        {
            if (menuObject != null)
                menuObject.SetActive(false);
            else
                throw new System.Exception("MENU MANAGER: SetInactivePage -> menuObject is null");
        }

        private static GameObject GetPageGameObject(Page page)
        {
            GameObject UI = GameObject.Find("UI");
            if (UI == null)
                throw new System.Exception("MENU MANAGER: GetPageGameObject -> No UI object in scene");

            switch (page)
            {
                case Page.MAIN:
                    return UI.transform.Find(GetPageName(Page.MAIN)).gameObject;
                case Page.OPTIONS:
                    return UI.transform.Find(GetPageName(Page.OPTIONS)).gameObject;
                case Page.ACHIEVEMENTS:
                    return UI.transform.Find(GetPageName(Page.ACHIEVEMENTS)).gameObject;
                case Page.PAUSE:
                    return UI.transform.Find(GetPageName(Page.PAUSE)).gameObject;
                case Page.WIN:
                    return UI.transform.Find(GetPageName(Page.WIN)).gameObject;
                case Page.LOSE:
                    return UI.transform.Find(GetPageName(Page.LOSE)).gameObject;
                case Page.LEVEL_SELECTION:
                    return UI.transform.Find(GetPageName(Page.LEVEL_SELECTION)).gameObject;
                case Page.LEVEL_EDITOR_UPPER:
                    return UI.transform.Find(GetPageName(Page.LEVEL_EDITOR_UPPER)).gameObject;
                case Page.LEVEL_EDITOR_OBJECTS:
                    return UI.transform.Find(GetPageName(Page.LEVEL_EDITOR_OBJECTS)).gameObject;
                case Page.LEVEL_EDITOR_SAVE:
                    return UI.transform.Find(GetPageName(Page.LEVEL_EDITOR_SAVE)).gameObject;
                default:
                    throw new System.Exception("MENU MANAGER: GetPageGameObject -> page is not defined in switch");
            }
        }

        private static string GetPageName(Page page)
        {
            switch (page)
            {
                case Page.MAIN:
                    return new string("Main Menu");
                case Page.OPTIONS:
                    return new string("Options Menu");
                case Page.ACHIEVEMENTS:
                    return new string("Achievements Menu");
                case Page.PAUSE:
                    return new string("Pause Panel");
                case Page.WIN:
                    return new string("Win Panel");
                case Page.LOSE:
                    return new string("Lose Panel");
                case Page.LEVEL_SELECTION:
                    return new string("Level Selection Menu");
                case Page.LEVEL_EDITOR_UPPER:
                    return new string("Upper Panel");
                case Page.LEVEL_EDITOR_OBJECTS:
                    return new string("Upper Panel/Safe Area/Available Objects List");
                case Page.LEVEL_EDITOR_SAVE:
                    return new string("Save Level Panel");
                default:
                    throw new System.Exception("MENU MANAGER: GetPageName -> page is not defined in switch");
            }
        }
    }
}