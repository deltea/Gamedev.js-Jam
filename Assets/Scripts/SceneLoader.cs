using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    #region Singleton
    
    static public SceneLoader Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneWithDelay(string sceneName, float delay) {
        StartCoroutine(LoadSceneWithDelayRoutine(sceneName, delay));
    }

    private IEnumerator LoadSceneWithDelayRoutine(string sceneName, float delay) {
        yield return new WaitForSeconds(delay);
        LoadScene(sceneName);
    }

}
