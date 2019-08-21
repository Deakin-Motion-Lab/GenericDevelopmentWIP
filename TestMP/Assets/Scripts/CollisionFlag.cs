using UnityEngine;
using Photon.Pun;

namespace CrossPlatformVR
{
    public class CollisionFlag : MonoBehaviourPun
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Part")
            {
                Debug.Log("Detected collision between " + gameObject.name + " and " + collision.collider.name);

                collision.collider.gameObject.GetComponent<Renderer>().material.color = Color.black;

                PhotonNetwork.Destroy(this.gameObject);

                PhotonNetwork.Instantiate("capsule", Vector3.zero, Quaternion.identity);
            }
        }
    }
}
