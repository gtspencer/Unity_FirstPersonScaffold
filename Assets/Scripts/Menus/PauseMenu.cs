using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);
    }

    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(Resume);
        quitButton.onClick.RemoveListener(Quit);
    }

    private void Resume()
    {
        GameStateManager.Instance.ResumeGame();
    }

    private void Quit()
    {
        quitButton.onClick.RemoveListener(Quit);
        AsyncLoader.Instance.LoadLevel("StartMenu");
    }
}
