﻿using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Scripts
{
    public class MenuController : MonoBehaviour
    {
        public static string SaveGamePath = "Savegame/save.xml";
        private const int LastLevel = 5;
        [SerializeField] private List<Button> levelButtons;
        private Dictionary<Button, int> levelDictionary;
        private int nextAvailableLevel;
        [SerializeField] private Button quitButton;

        private void EnableButtons()
        {
            if (nextAvailableLevel > LastLevel)
                nextAvailableLevel = LastLevel;
            for (int i = 0; i <= nextAvailableLevel; i++)
            {
                levelButtons[i]?.gameObject.SetActive(true);
            }
        }

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

        private void ReadSaveFile()
        {
            if (!File.Exists(SaveGamePath))
                nextAvailableLevel = 0;
            else
            {
                using (XmlReader reader = XmlReader.Create(new StreamReader(SaveGamePath)))
                {
                    while (reader.Read())
                    {
                        if (reader.Name.Equals("NextAvailableLevel"))
                            nextAvailableLevel = int.Parse(reader.ReadString());
                    }
                }
            }
        }

        private void Start()
        {
            levelDictionary = new Dictionary<Button, int>();
            InitializeDictionary();
            levelButtons.ForEach(b => b.onClick.AddListener(() => OnButtonClick(b)));
            quitButton.onClick.AddListener(Application.Quit);
            ReadSaveFile();
            EnableButtons();
            Cursor.visible = true;
        }
    }
}