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
    public class OwnerTransfer : MonoBehaviourPunCallbacks, IPunObservable
    {
        private bool changeColour;

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
                    changeColour = true;
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
                    changeColour = false;
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
            ChangeColour();
        }

        private void ChangeColour()
        {
            if (changeColour)
            {
                GetComponent<Renderer>().material.color = Color.blue;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.white;
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(changeColour);
                Debug.Log("Sending color update");
            }
            else
            {
                changeColour = (bool)stream.ReceiveNext();
                Debug.Log("Receiving color update");
            }
        }
    }
}
