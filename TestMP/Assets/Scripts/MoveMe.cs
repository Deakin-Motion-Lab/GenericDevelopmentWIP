using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour
{
    private float _Speed = 4f;
    private bool _IsMoving;
    public bool IsMoving
    {
        get { return _IsMoving; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _IsMoving = false;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * _Speed * Time.deltaTime);
            _IsMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * _Speed * Time.deltaTime);
            _IsMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _Speed * Time.deltaTime);
            _IsMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _Speed * Time.deltaTime);
            _IsMoving = true;
        }
    }
}
