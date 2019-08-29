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
            //if (collision.collider.tag == "Part")
            //{
            //    Debug.Log("COLLISION detected between " + gameObject.name + " and " + collision.collider.name);

            //    // Take ownership of colliding objects (if we don't own it)
            //    if (!collision.collider.gameObject.GetComponent<PhotonView>().IsMine)        
            //    {
            //        collision.collider.gameObject.GetComponent<PhotonView>().RequestOwnership();        
            //    }

            //    if (!gameObject.GetComponent<PhotonView>().IsMine)
            //    {
            //        gameObject.GetComponent<PhotonView>().RequestOwnership();
            //    }
            //}

            // Only allow one collision event to destroy both objects (not both)
            // Prevent two instances of game objects spawning as identical objects (balls) get destroyed
            if (collision.collider.tag == tag)
            {
                if (ignoreCollision)
                {
                    return;
                }

                // Set flag to disable "other" collider function (prevent both objects running this method)
                collision.gameObject.GetComponent<CollisionFlag>().ignoreCollision = true;

                // Destroy "other' game object
                PhotonNetwork.Destroy(collision.gameObject);

                // Instantiate new game object (assembled part)
                //GameObject caps = PhotonNetwork.Instantiate("Capsule", new Vector3(1f, 1.0f, 0f), Quaternion.identity);
                DontDestroyOnLoad(PhotonNetwork.InstantiateSceneObject(newAssembly.name, new Vector3(0f, 1.0f, 0f), Quaternion.identity));

                // Destroy this game object
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
