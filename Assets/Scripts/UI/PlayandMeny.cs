using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoade : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Play");
    }

    public void Meny()
    {
        SceneManager.LoadScene("Meny");
    }
}