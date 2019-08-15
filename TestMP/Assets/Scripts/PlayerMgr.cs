using System.Collections;
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
        public GameObject ball;

        // Awake is called at instantiation
        private void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;

                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.Log("I am the master client...");
                    GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    Debug.Log("I am the remote client...");
                    GetComponent<Renderer>().material.color = Color.yellow;
                }
            }

            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(gameObject);

            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(ball);
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
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    SpawnBall();
                }

            }
        }

        /// <summary>
        /// Allows individual networked players to spawn balls in the scene
        /// </summary>
        private void SpawnBall()
        {
            Debug.Log("Ball instantiated from inside player mgr");
            // PhotonNetwork.InstantiateSceneObject(ball.name, new Vector3(0f, 1f, 0f), Quaternion.identity);       // Master client (can be changed to another player) controls this
            PhotonNetwork.Instantiate(ball.name, new Vector3(0f, 1f, 0f), Quaternion.identity);                     // Any player who instantiates controls this (ownership can be transfered)
        }
    }
}
