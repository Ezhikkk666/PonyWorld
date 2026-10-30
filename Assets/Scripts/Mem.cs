using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Mem()
    {
        SceneManager.LoadScene("MemSecret");
    }

    public void Meny()
    {
        SceneManager.LoadScene("Meny");
    }
}