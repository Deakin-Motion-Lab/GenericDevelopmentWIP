using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private Color alphaColor;
    private float _Alpha;
    private float _Red;
    private Material myMaterial;


    public void Start()
    {
        alphaColor = GetComponent<MeshRenderer>().material.color;
        alphaColor = new Color(1, 0, 0);
        myMaterial = GetComponent<MeshRenderer>().material;
        myMaterial.color = alphaColor;
        _Alpha = 1f;
    }

    public void Update()
    {
        if (_Alpha <= 0)
        {
            return;
        }

        _Alpha -= 0.005f;
        alphaColor.a = _Alpha;

        myMaterial.color = alphaColor;

        //Debug.Log(alphaColor);
    }
}