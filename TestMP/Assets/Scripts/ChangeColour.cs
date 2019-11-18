using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace CrossPlatformVR
{
    public class ChangeColour : MonoBehaviourPunCallbacks
    {
        private Color[] colours;
        private GameObject _Floor;
        private bool _FloorOn;

        private void Awake()
        {
            colours = new Color[4];

            colours[0] = new Color(1f, 0.5f, 0.0039f);
            colours[1] = Color.yellow;
            colours[2] = Color.blue;
            colours[3] = Color.white;

            _Floor = GameObject.Find("Floor");
            _FloorOn = true;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    //myColour.material.color = colours[0];
                    photonView.RPC("ChangeMyColour", RpcTarget.AllBuffered, 0);
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    //myColour.material.color = colours[1];
                    photonView.RPC("ChangeMyColour", RpcTarget.AllBuffered, 1);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _FloorOn = !_FloorOn;
                    _Floor.SetActive(_FloorOn);
                }
            }
        }

        [PunRPC]
        private void ChangeMyColour(int choice)
        {
            gameObject.GetComponent<Renderer>().material.color = colours[choice];
        }
    }
}
