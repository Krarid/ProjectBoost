using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch( other.gameObject.tag )
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
            break;

            case "Finish":
                Debug.Log("Congrats, yo, you finished!");
            break;

            case "Fuel":
                Debug.Log("You picked up fuel");
            break;

            default:
                Debug.Log("Sorry, you blew up!");
            break;
        }
    }
}
