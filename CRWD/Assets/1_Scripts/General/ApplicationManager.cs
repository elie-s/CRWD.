using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CRWD
{
    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField, Range(0,1)] private int otherScene = 1;
        [SerializeField] private KeyCode changeSceneKey = KeyCode.Return;
        [SerializeField] private LevelManager levelManager = default;
        [SerializeField] private KeyCode startGameKey = KeyCode.Space;

        private void Update()
        {
            if (Input.GetKeyDown(changeSceneKey)) ChangeScene();
            else if (Input.GetKeyDown(KeyCode.Escape)) Quit();
            else if (Input.GetKeyDown(startGameKey)) StartGame();
        }

        private void ChangeScene()
        {
            SceneManager.LoadSceneAsync(otherScene);
        }

        private void Quit()
        {
            Application.Quit();
        }

        private void StartGame()
        {
            if (!levelManager) return;

            levelManager.StartWorld();
            levelManager.StartLevel();
        }
    }
}