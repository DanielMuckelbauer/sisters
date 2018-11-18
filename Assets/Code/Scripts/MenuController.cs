using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Scripts
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button quitButton;
        [SerializeField] private List<Button> levelButtons;
        private Dictionary<Button, int> levelDictionary;

        private void InitializeDictionary()
        {
            for (int i = 0; i < levelButtons.Count; i++)
            {
                levelDictionary.Add(levelButtons[i], i + 1);
            }
        }

        private void OnButtonClick(Button pressedButton)
        {
            SceneManager.LoadScene(levelDictionary[pressedButton]);
        }

        private void Start()
        {
            levelDictionary = new Dictionary<Button, int>();
            InitializeDictionary();
            levelButtons.ForEach(b => b.onClick.AddListener(() => OnButtonClick(b)));
            quitButton.onClick.AddListener(Application.Quit);
        }
    }
}