using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controller
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private GameObject nextLevelPanel;
        [SerializeField] private GameObject restartPanel;

        private void Start()
        {
            var currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            levelText.text = "Level " + (currentLevel + 1);
        }

        public void OnWin()
        {
            nextLevelPanel.SetActive(true);
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
        }

        public void OnLose()
        {
            restartPanel.SetActive(true);
        }

        public void OpenLevel()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
