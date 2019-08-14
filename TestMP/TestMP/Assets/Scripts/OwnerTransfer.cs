using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace CrossPlatformVR
{
    /// <summary>
    /// Basic transfer of ownership of a scene object (ball) to the local player (when 
    /// </summary>
    public class OwnerTransfer : MonoBehaviourPunCallbacks
    {
        private void OnMouseDown()
        {
            photonView.RequestOwnership();
        }

        // TBC: attempting to transfer ownership to local player when they "touch" the ball with their vr hand
        private void OnTriggerEnter(Collider other)
        {
            if (photonView.IsMine)
            {
                if (other.tag == "VRGrab")
                {
                    photonView.RequestOwnership();
                }
            }
        }

        // TBC: attempting to transfer ownership back to scene when a vr user "releases" the ball
        private void OnTriggerExit(Collider other)
        {
            if (photonView.IsMine)
            {
                if (other.tag == "VRGrab")
                {
                    // Transfer ownership back to scene
                    photonView.TransferOwnership(0);
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            photonView.TransferOwnership(0);
        }

        // Update is called once per frame
        void Update()
        {
            // Check ownership transfer request and transfer as required
            if (photonView.Owner == PhotonNetwork.LocalPlayer)
            {
                Debug.Log("I own the ball");
                GetComponent<Renderer>().material.color = Color.blue;
            }
            else
            {
                if (photonView.Owner == null)
                {
                    Debug.Log("Ball owned by SCENE");
                }
                else
                {
                    Debug.Log("Ball owned by: " + photonView.Owner);
                }
                GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}
