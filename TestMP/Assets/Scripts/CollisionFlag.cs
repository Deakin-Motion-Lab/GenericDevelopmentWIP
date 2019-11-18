using UnityEngine;
using Photon.Pun;

namespace CrossPlatformVR
{
    /// <summary>
    /// This class manages collision events occuring between two game objects:
    /// if the objects have the same "tag", they are able to be "assembled" into a new part
    /// </summary>
    public class CollisionFlag : MonoBehaviourPun
    {
        [HideInInspector]
        public bool ignoreCollision;

        // Utilise "generic" gameobjects to allow this script to apply to multiple objects / assemblies
        public GameObject newAssembly;

        private void OnCollisionEnter(Collision collision)
        {
            // Only allow one collision event to destroy both objects (not both)
            // Prevent two instances of game objects spawning as identical objects (balls) get destroyed
            if (collision.collider.tag == tag)
            {
                if (ignoreCollision)
                {
                    return;
                }

                // Takeover ownership of both objects (to allow local player to destroy both objects - cannot destroy an object which we do not own)
                photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
                collision.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);

                // Set flag to disable "other" collider function (prevent both objects running this method)
                collision.gameObject.GetComponent<CollisionFlag>().ignoreCollision = true;

                // Destroy "other' game object
                PhotonNetwork.Destroy(collision.gameObject);

                // Instantiate new game object (assembled part)
                // Request the master spawns the new assembly (scene object)
                photonView.RPC("CreateAssembly", RpcTarget.MasterClient, newAssembly.name);

                // Destroy this game object
                PhotonNetwork.Destroy(gameObject);
            }
        }

        /// <summary>
        /// Master client instantiates scene object (upon request via RPC)
        /// </summary>
        /// <param name="kinematic"></param>
        [PunRPC]
        public void CreateAssembly(string name)
        {
            DontDestroyOnLoad(PhotonNetwork.InstantiateSceneObject(newAssembly.name, new Vector3(0f, 1.0f, 0f), Quaternion.identity));
        }
    }
}
