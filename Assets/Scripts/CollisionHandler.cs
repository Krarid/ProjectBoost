using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] float levelLoadDelay = 2f;

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
                StartSuccessSequence();
            break;

            default:
                StartCrashSequence();
            break;
        }
    }

    void StartSuccessSequence()
    {
        // To do add SFX upon crash
        // To do add paticle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        // To do add SFX upon crash
        // To do add paticle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
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
