using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace CrossPlatformVR
{
    /// <summary>
    /// Basic transfer of ownership of a scene object to the local player (when a collision event trigger occurs)
    /// </summary>
    public class OwnerTransfer : MonoBehaviourPunCallbacks, IPunObservable
    {
        private bool changeColour;
        private Vector3 ballPosition;
        private Color orig;

        void Awake()
        {
            orig = GetComponent<Renderer>().material.color;
        }

        // Transfer ownership to local player when they "touch" the ball with their vr hand
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger Entered..." + other.gameObject.name);

            if (other.tag == "VRGrab")
            {
                //// Take ownership of object if we don't own it
                //if (!other.gameObject.GetComponent<PhotonView>().IsMine)        // Takes ownership of 'other' game object (eg. vr hands)
                //{
                //    Debug.Log("Old Owner: " + other.gameObject.GetComponent<PhotonView>().Owner.NickName);
                //    other.gameObject.GetComponent<PhotonView>().RequestOwnership();        // Update 22/8/19: Not required for a networked player to interact with an object
                //    Debug.Log("New Owner: " + other.gameObject.GetComponent<PhotonView>().Owner.NickName);
                //}

                changeColour = true;
                ChangeColour(changeColour);

                // Bind transform to player who triggered collision
                //GetComponent<Rigidbody>().isKinematic = true;     // Disables external forces that apply to the ball
                transform.SetParent(other.GetComponentInParent<Transform>());
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
                    //photonView.TransferOwnership(0);              // Update 22/8/19: Not required for a networked player to interact with an object
                    changeColour = false;
                    ChangeColour(changeColour);
                    //GetComponent<Rigidbody>().isKinematic = false;      // Disables external forces that apply to the ball
                    transform.SetParent(null);
                }
            }
        }



        /// <summary>
        /// Changes the colour of the ball to display ownership transfer between players and scene
        /// </summary>
        private void ChangeColour(bool isChanged)
        {
            if (isChanged)
            {
                GetComponent<Renderer>().material.color = Color.blue;
            }
            else
            {
                GetComponent<Renderer>().material.color = orig;
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
                ChangeColour((bool)stream.ReceiveNext());
            }
        }
    }
}
