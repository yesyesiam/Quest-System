using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    //[SerializeField] private LoadingScreen _loadingScreen;
    //public LoadingScreen LoadingScreen => _loadingScreen;
    public static GameInstance Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void LoadLevel(string name)
    {
        StartCoroutine(LoadingLevel(name));
    }

    [ContextMenu("Test Load Room")]
    public void LoadSecretRoom()
    {
        StartCoroutine(LoadingLevel("SecretRoom"));
    }

    private IEnumerator LoadingLevel(string sceneName = "SampleScene")
    {
        //_loadingScreen.Show();

        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

        while (loadSceneAsync.isDone == false)
        {
            yield return null;
            //_loadingScreen.ChangeLoadingPercent(Mathf.Clamp01(loadSceneAsync.progress));
        }

        //yield return new WaitForSeconds(1f);

        /*_currentLevelInstance = FindObjectOfType<BaseLevelInstance>();
        if (_currentLevelInstance != null)
        {
            _currentLevelInstance.Init(this);
        }*/

        //_loadingScreen.Hide();
    }
}
