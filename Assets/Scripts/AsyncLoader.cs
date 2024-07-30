using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour
{
    public static AsyncLoader Instance;
    
    [SerializeField] private GameObject loadingScreenPrefab;
    private GameObject loadingScreen;

    private Slider loadingSlider;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        loadingScreen = Instantiate(loadingScreenPrefab);
        loadingScreen.SetActive(false);
        DontDestroyOnLoad(loadingScreen);

        loadingSlider = loadingScreen.GetComponentInChildren<Slider>();
    }

    public void LoadLevel(string levelName)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(levelName));
    }

    private IEnumerator LoadLevelAsync(string levelName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);

            loadingSlider.value = progressValue;
            yield return null;
        }

        loadingScreen.SetActive(false);
    }
}
