using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        
        PlayerInputSystem.Instance.OnEscape += EscapePressed;
        PlayerInputSystem.Instance.OnUIBack += EscapePressed;
        
        PlayerInputSystem.Instance.ChangeInputType(PlayerInputSystem.InputType.Movement);
    }

    private void OnDestroy()
    {
        PlayerInputSystem.Instance.OnEscape -= EscapePressed;
        PlayerInputSystem.Instance.OnUIBack -= EscapePressed;
    }

    private void EscapePressed()
    {
        if (pauseMenu.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        PlayerInputSystem.Instance.ChangeInputType(PlayerInputSystem.InputType.UI);
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        PlayerInputSystem.Instance.ChangeInputType(PlayerInputSystem.InputType.Movement);
        pauseMenu.SetActive(false);
    }
}
