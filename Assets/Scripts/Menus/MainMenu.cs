using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    private const string KeyboardAndMouseId = "Keyboard&Mouse";
    private const string ControllerId = "Gamepad";
    
    
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartButtonClicked);
        quitButton.onClick.AddListener(Quit);

        PlayerInputSystem.Instance.ChangeInputType(PlayerInputSystem.InputType.UI);
    }

    private void StartButtonClicked()
    {
        startButton.onClick.RemoveListener(StartButtonClicked);
        gameObject.SetActive(false);
        
        AsyncLoader.Instance.LoadLevel("Main");
        // SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
