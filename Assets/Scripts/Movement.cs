using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region properties

        [SerializeField] private float speed = 1000.0f;
        [SerializeField] private float rotationSpeed = 100.0f;
        [SerializeField] AudioClip mainEngine;
        [SerializeField] public ParticleSystem mainEngineParticles;
        [SerializeField] public ParticleSystem leftEngineParticles;
        [SerializeField] public ParticleSystem rightEngineParticles;
        private bool upPressed;
        private bool leftPressed;
        private bool rightPressed;

        private Rigidbody rocketRigidbody;
        private AudioSource audioSource;

    #endregion

    #region methods
      // Start is called before the first frame update
        void Start()
        {
            rocketRigidbody = GetComponent<Rigidbody>();
            rocketRigidbody.mass = 1.0f;

            audioSource = GetComponent<AudioSource>();
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
                rocketRigidbody.AddRelativeForce(Vector3.up * speed * Time.deltaTime); // Boost the rocket

                if( !audioSource.isPlaying ) // Avoid audio playing several times in once
                {
                    audioSource.PlayOneShot(mainEngine);
                } 

                if( !mainEngineParticles.isPlaying )
                {
                    mainEngineParticles.Play();                    
                }
                
            } else {
                audioSource.Stop();
                mainEngineParticles.Stop();
            }
        }

        void ProcessRotation()
        {
            leftPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
            rightPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

            if( leftPressed && !rightPressed  )
            {
                ApplyRotation( Vector3.forward );

                if( !rightEngineParticles.isPlaying )
                {
                    rightEngineParticles.Play();                    
                }
            } else 
            {
                rightEngineParticles.Stop(); 
            }
            
            if( rightPressed && !leftPressed )
            {
                ApplyRotation( Vector3.back );

                if( !leftEngineParticles.isPlaying )
                {
                    leftEngineParticles.Play();                    
                }
            }
            else
            {
                leftEngineParticles.Stop();    
            }
        }

        void ApplyRotation( Vector3 orientation )
        {
            rocketRigidbody.freezeRotation = true;
            transform.Rotate(orientation * rotationSpeed * Time.deltaTime);
            rocketRigidbody.freezeRotation = false;
        }
    #endregion
}
