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
    public class OwnerTransfer : MonoBehaviourPunCallbacks
    {
        private bool changeColour;
        private Vector3 ballPosition;
        private Color orig;

        // Transfer ownership to local player when they "touch" the ball with their vr hand
        private void OnTriggerEnter(Collider other)
        {
            // NOTE:    "other" is the scene object we are interacting with!

            Debug.LogFormat("Trigger Entered with ball (owned by {0}) and player (owned by {1})", this.gameObject.GetPhotonView().Owner, other.gameObject.GetPhotonView().Owner);

            if (other.tag == "Tool")
            {
                // Store (original) colour of other object
                orig = gameObject.GetComponent<Renderer>().material.color;

                // Request PhotonView ownership of other object
                //photonView.TransferOwnership(PhotonNetwork.LocalPlayer);      // Not correct
                PhotonView ph = other.gameObject.GetPhotonView();
                photonView.TransferOwnership(ph.Owner);

                Debug.Log("My new owner is " + photonView.Owner);

                // Change colour to show interaction
                changeColour = true;
                ChangeColour();

                // Bind transform to player who triggered collision
                //other.GetComponent<Rigidbody>().isKinematic = true;     // Disables external forces that apply to the ball
                transform.SetParent(other.transform);
            }
        }

        /// <summary>
        /// Transfer ownership back to scene when a vr user "releases" the ball 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Trigger exited...");

            transform.SetParent(null);
            changeColour = false;
            ChangeColour();

            photonView.TransferOwnership(0);
        }

        /// <summary>
        /// Changes the colour of the ball to display ownership transfer between players and scene
        /// </summary>
        private void ChangeColour()
        {
            if (changeColour)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = orig;
            }
        }
    }
}
