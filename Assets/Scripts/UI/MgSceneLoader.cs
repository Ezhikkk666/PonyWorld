using UnityEngine;
using UnityEngine.SceneManagement;

public class MgSceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Имя сцены пустое");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
