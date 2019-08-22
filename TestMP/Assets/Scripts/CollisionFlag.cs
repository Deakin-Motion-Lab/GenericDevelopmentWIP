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
                Debug.Log("COLLISION detected between " + gameObject.name + " and " + collision.collider.name);

                // Take ownership of colliding objects (if we don't own it)
                if (!collision.collider.gameObject.GetComponent<PhotonView>().IsMine)        
                {
                    collision.collider.gameObject.GetComponent<PhotonView>().RequestOwnership();        
                }

                if (!gameObject.GetComponent<PhotonView>().IsMine)
                {
                    gameObject.GetComponent<PhotonView>().RequestOwnership();
                }

                
                //PhotonNetwork.Destroy(collision.collider.gameObject);
                PhotonNetwork.Destroy(gameObject);

                GameObject caps = PhotonNetwork.Instantiate("capsule", new Vector3(1f, 1.5f, 0f), Quaternion.identity) ;
                DontDestroyOnLoad(caps);
                //DontDestroyOnLoad(PhotonNetwork.Instantiate("capsule", new Vector3(0f, 1f, 0f), Quaternion.identity));
                //gameObject.SetActive(false);
            }
        }
    }
}
