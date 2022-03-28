using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    Movement rocket;

    bool isTransitioning = false;

    void Start() 
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        audioSource = GetComponent<AudioSource>();

        rocket = GetComponent<Movement>();
    }

    void OnCollisionEnter(Collision other)
    {
        if( isTransitioning ) return;

        switch( other.gameObject.tag )
        {
            case "Friendly":
                // Debug.Log("This thing is friendly");
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
        isTransitioning = true;
        
        audioSource.Stop();
        audioSource.PlayOneShot( success );

        StopRocketAnimations();

        successParticles.Play();
        
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
    
        audioSource.Stop();
        audioSource.PlayOneShot( crash );
        
        StopRocketAnimations();

        crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        currentSceneIndex = ++currentSceneIndex == SceneManager.sceneCountInBuildSettings ? 0 : currentSceneIndex;

        SceneManager.LoadScene( currentSceneIndex );
    }

    void StopRocketAnimations()
    {
        rocket.leftEngineParticles.Stop();
        rocket.rightEngineParticles.Stop();
        rocket.mainEngineParticles.Stop();
    }
}
