using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace CrossPlatformVR
{
    public class PlayerMgr : MonoBehaviourPunCallbacks, IPunObservable
    {
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;
        public GameObject ball;

        private Vector3 otherPlayerPosition;
        private Quaternion otherPlayerRotation;

        // Awake is called at instantiation
        private void Awake()
        {
            // #Important
            // used in RoomMgr.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;

                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.LogFormat("{0} is the master client...", PhotonNetwork.LocalPlayer.NickName);
                    GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    Debug.LogFormat("{0} is standard client...", PhotonNetwork.LocalPlayer.NickName);
                    GetComponent<Renderer>().material.color = Color.yellow;
                }
            }

            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine)
            {
                // Control our player's avatar only
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
                // Leave Room
                else if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    RoomMgr.LeaveRoom();
                }

            }
            else
            {
                // Update other player(s) avatar positions
                transform.position = otherPlayerPosition;
                transform.rotation = otherPlayerRotation;
            }
        }

        /// <summary>
        /// Allows individual networked players to spawn balls in the scene
        /// </summary>
        private void SpawnBall()
        {
            Debug.LogFormat("Ball instantiated from inside player mgr by {0}", PhotonNetwork.NickName);

            // PhotonNetwork.InstantiateSceneObject(ball.name, new Vector3(0f, 1f, 0f), Quaternion.identity);       // Master client controls this (can be changed to other players) 

            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            // The player who instantiates this object also controls it by default (ownership can be transfered)
            DontDestroyOnLoad(PhotonNetwork.Instantiate(ball.name, new Vector3(0f, 1f, 0f), Quaternion.identity));
        }

        /// <summary>
        /// Utilising serialize view to 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="info"></param>
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
            }
            else if (stream.IsReading)
            {
                otherPlayerPosition = (Vector3)stream.ReceiveNext();
                otherPlayerRotation = (Quaternion)stream.ReceiveNext();
            }
        }
    }
}
