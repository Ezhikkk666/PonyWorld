
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSecret()
    {
        SceneManager.LoadScene("MemSecret");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Meny");
    }
}