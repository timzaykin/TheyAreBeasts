using UnityEngine;

namespace TAB
{
    public class SceneManager : MonoBehaviour
    {
        public GameObject MenuPanel;

        public void LoadSurvivalScene() {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GraveYardSurvival");
        }

        public void LoadMainMenu() {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        public void Quit() {
            Application.Quit();
        }

        void Update()
        {
            if (MenuPanel != null) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    if (MenuPanel.activeInHierarchy) MenuPanel.SetActive(false);
                    else MenuPanel.SetActive(true);
                }
            }
        }
    }
}
