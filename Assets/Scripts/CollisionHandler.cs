using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;

    void Start() 
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void OnCollisionEnter(Collision other)
    {
        switch( other.gameObject.tag )
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
            break;

            case "Finish":
                LoadNextLevel();
            break;

            case "Fuel":
                Debug.Log("You picked up fuel");
            break;

            default:
                ReloadLevel();
            break;
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene( ++currentSceneIndex == SceneManager.sceneCountInBuildSettings ? 0 : currentSceneIndex );
    }
}
