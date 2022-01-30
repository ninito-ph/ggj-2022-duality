using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MatchSystem : MonoBehaviour
    {
        #region SceneNames
        public string menuScene;
        public string gameScene;
        #endregion

        #region Timer
        public float timeLeft = 10f;
        public bool startTimer = false;
        #endregion

        public static MatchSystem instance;

        public bool gameHasEnded;
        public GameObject endGameScreen;

        private void Awake()
        {
            instance = this;
        }
        private void Update()
        {

            if(gameHasEnded)
            {
                endGameScreen.SetActive(true);
                startTimer = true;
            }

            #region WhenTimerStarts
            if (startTimer)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    Debug.Log("Timer is over!");
                    timeLeft = 10f;
                    startTimer = false;
                    gameHasEnded = false;
                    LoadMenu();
                }
            }
            #endregion
        }

        public void LoadGame()
        {
            SceneManager.LoadScene(gameScene);
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene(menuScene);
        }
    }
}
