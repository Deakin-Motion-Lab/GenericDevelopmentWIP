using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace CrossPlatformVR
{
    public class BallSpawn : MonoBehaviourPunCallbacks
    {
        public GameObject ball;

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    CreateBall();
            //}

        }

        public void CreateBall()
        {
            PhotonNetwork.InstantiateSceneObject(ball.name, new Vector3(0f, 1f, 0f), Quaternion.identity);
        }
    }
}
