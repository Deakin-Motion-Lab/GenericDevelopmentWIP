using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public GameObject child;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Debug.LogFormat("World: {0} {1} {2} / Local: {3} {4} {5}", child.transform.position.x, child.transform.position.y, child.transform.position.z, child.transform.localPosition.x, child.transform.localPosition.y, child.transform.localPosition.z);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}
