using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour

{

    public string PlayGame;

    public void SwitchScene()

    {

        SceneManager.LoadScene(PlayGame);

    }

}


   