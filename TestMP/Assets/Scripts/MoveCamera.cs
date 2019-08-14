using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace CrossPlatformVR
{
    public class MoveCamera : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    //transform.forward = new Vector3()
                }
            }
        }
    }
}
