using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateNeedle : MonoBehaviour
{
    public float maximumValue;
    public float setValue;
    public float rateOfChange;

    private float _CurrentAngle;
    private float _SetAngle;
    private const float _MIN_ANGLE = -345f;     // Equivalent of 30deg from 180deg starting point
    private const float _MAX_ANGLE = -645f;

    private ToggleSwitch _Switch;

    private void Awake()
    {
        _Switch = GameObject.Find("Toggle").GetComponent<ToggleSwitch>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, _MIN_ANGLE);
        _CurrentAngle = _MIN_ANGLE;
        CalculateSetAngle();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Switch.IsOpen)
        {
            if (_CurrentAngle >= _SetAngle)
            {
                _CurrentAngle += -rateOfChange * Time.deltaTime;
                transform.eulerAngles = new Vector3(0f, 0f, _CurrentAngle);
                Debug.Log("Current angle: " + _CurrentAngle + " Set angle: " + _SetAngle);

                Debug.Log("Rate: " + rateOfChange);
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, _SetAngle);
                Debug.Log(transform.eulerAngles);
                //enabled = false;        // Stop Update()
            }
        }
        else
        {
            if (_CurrentAngle < _MIN_ANGLE)
            {
                _CurrentAngle -= -rateOfChange * Time.deltaTime;
                transform.eulerAngles = new Vector3(0f, 0f, _CurrentAngle);
            }
        }
    }

    /// <summary>
    /// Calculates the appropriate angle of the "Set Value" between the minimum and maximum values defined on the gauge
    /// </summary>
    private void CalculateSetAngle()
    {
        float range = _MAX_ANGLE - _MIN_ANGLE;
        float anglePerValue = range / maximumValue;
        _SetAngle = setValue * anglePerValue + _MIN_ANGLE;
    }
}
