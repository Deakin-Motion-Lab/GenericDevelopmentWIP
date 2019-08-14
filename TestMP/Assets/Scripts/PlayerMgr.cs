﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace CrossPlatformVR
{
    public class PlayerMgr : MonoBehaviourPunCallbacks
    {
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        //public Material masterMaterial;
        //public Material remoteMaterial;


        // Awake is called at instantiation
        private void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
            }

            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            //if ()
            //{
            //    gameObject.GetComponent<Renderer>().material.color = Color.blue;
            //}
            //else
            //{
            //    gameObject.GetComponent<Renderer>().material.color = Color.red;
            //}
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 2 * Time.deltaTime, transform.position.z);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 2 * Time.deltaTime, transform.position.z);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    transform.position = new Vector3(transform.position.x - 2 * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    transform.position = new Vector3(transform.position.x + 2 * Time.deltaTime, transform.position.y, transform.position.z);
                }
                // Spawn a ball
                //if (Input.GetKeyDown(KeyCode.Space))
                //{
                //    BallSpawn.CreateBall();
                //}

            }
        }
    }
}