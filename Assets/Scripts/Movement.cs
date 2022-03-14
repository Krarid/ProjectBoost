using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region properties

        private bool upPressed;
        private bool leftPressed;
        private bool rightPressed;

        private Rigidbody rocketRigidbody;

        [SerializeField] private float speed = 1000.0f;
        [SerializeField] private float rotationSpeed = 100.0f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        rocketRigidbody.mass = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        upPressed = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow);

       if( upPressed )
       {
           rocketRigidbody.AddRelativeForce(Vector3.up * speed * Time.deltaTime);
       }
    }

    void ProcessRotation()
    {
        leftPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        rightPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

       if( leftPressed && !rightPressed  )
       {
           ApplyRotation( Vector3.forward );
       }  
       
       if( rightPressed && !leftPressed )
       {
           ApplyRotation( Vector3.back );
       } 
    }

    void ApplyRotation( Vector3 orientation )
    {
        transform.Rotate(orientation * rotationSpeed * Time.deltaTime);
    }
}
