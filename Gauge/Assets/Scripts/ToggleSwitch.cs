using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    private Vector3 _OpenPosition;
    private Vector3 _ClosedPosition;
    private bool _IsOpen;
    public bool IsOpen
    {
        get { return _IsOpen; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _OpenPosition = new Vector3(-1.065f, 0.373f, 0f);
        _ClosedPosition = new Vector3(-1.065f, -0.394f, 0f);
        _IsOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _IsOpen = !_IsOpen;
            ActivateSwitch();
        }
    }

    private void ActivateSwitch()
    {
        if (_IsOpen)
        {
            transform.position = _OpenPosition;
        }
        else
        {
            transform.position = _ClosedPosition;
        }
    }
}
