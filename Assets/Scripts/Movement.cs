﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region properties

        private bool upPressed;
        private bool leftPressed;
        private bool rightPressed;

        private Rigidbody rocketRigidbody;
        private AudioSource audioSource;

        [SerializeField] private float speed = 1000.0f;
        [SerializeField] private float rotationSpeed = 100.0f;

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
                    audioSource.Play();
                } 
                
            } else {
                audioSource.Stop();
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
            rocketRigidbody.freezeRotation = true;
            transform.Rotate(orientation * rotationSpeed * Time.deltaTime);
            rocketRigidbody.freezeRotation = false;
        }
    #endregion
}
