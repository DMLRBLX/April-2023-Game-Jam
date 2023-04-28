using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapScene : MonoBehaviour
{
    public void LoadMainScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }
}
