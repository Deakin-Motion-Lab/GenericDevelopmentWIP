using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace CrossPlatformVR
{
    /// <summary>
    /// Basic transfer of ownership of a scene object (ball) to the local player (when a collision event trigger occurs)
    /// </summary>
    public class OwnerTransfer : MonoBehaviourPunCallbacks, IPunObservable
    {
        private bool changeColour;
        private Vector3 ballPosition;
        private Color orig;

        // Transfer ownership to local player when they "touch" the ball with their vr hand
        private void OnTriggerEnter(Collider other)
        {
            if (photonView.IsMine)
            {
                if (other.tag == "VRGrab")
                {
                    photonView.RequestOwnership();
                    changeColour = true;

                    // Bind transform to player who triggered collision
                    GetComponent<Rigidbody>().isKinematic = true;     // Disables external forces that apply to the ball
                    transform.SetParent(other.GetComponentInParent<Transform>());
                }
            }
        }

        // Transfer ownership back to scene when a vr user "releases" the ball
        private void OnTriggerExit(Collider other)
        {
            if (photonView.IsMine)
            {
                if (other.tag == "VRGrab")
                {
                    // Transfer ownership back to scene
                    photonView.TransferOwnership(0);
                    changeColour = false;
                    GetComponent<Rigidbody>().isKinematic = false;      // Disables external forces that apply to the ball
                    transform.SetParent(null);
                    
                }
            }
        }

        void Start()
        {
            orig = GetComponent<Renderer>().material.color;
        }

        // Update is called once per frame
        void Update()
        {
            ChangeColour();
        }

        /// <summary>
        /// Changes the colour of the ball to display ownership transfer between players and scene
        /// </summary>
        private void ChangeColour()
        {
            if (changeColour)
            {
                GetComponent<Renderer>().material.color = Color.blue;
            }
            else
            {
                //GetComponent<Renderer>().material.color = orig;
            }
        }

        /// <summary>
        /// Sends a boolean state across the network regarding the ball's ownership colour
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="info"></param>
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(changeColour);
            }
            else
            {
                changeColour = (bool)stream.ReceiveNext();
            }
        }
    }
}
