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

            Debug.Log("Trigger Entered with..." + other.name);

            if (other.tag == "Part" || other.tag == "sub-assy" || other.tag == "final-assy")        // Consider removing this condition: blanket trigger
            {
                // Store (original) colour of other object
                orig = other.gameObject.GetComponent<Renderer>().material.color;
                
                // Request PhotonView ownership of other object
                other.gameObject.GetComponent<PhotonView>().RequestOwnership();

                // Change colour to show interaction
                changeColour = true;
                ChangeColour(other.gameObject);

                // Bind transform to player who triggered collision
                //other.GetComponent<Rigidbody>().isKinematic = true;     // Disables external forces that apply to the ball
                other.transform.SetParent(transform);
            }
        }

        /// <summary>
        /// Transfer ownership back to scene when a vr user "releases" the ball 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Trigger exited...");

            other.transform.SetParent(null);
            changeColour = false;
            ChangeColour(other.gameObject);

            other.gameObject.GetComponent<PhotonView>().TransferOwnership(0);
        }

        /// <summary>
        /// Changes the colour of the ball to display ownership transfer between players and scene
        /// </summary>
        private void ChangeColour(GameObject otherObject)
        {
            if (changeColour)
            {
                otherObject.GetComponent<Renderer>().material.color = Color.blue;
            }
            else
            {
                otherObject.GetComponent<Renderer>().material.color = orig;
            }
        }
    }
}
