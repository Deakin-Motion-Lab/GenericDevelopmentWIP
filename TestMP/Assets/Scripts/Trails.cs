using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trails : MonoBehaviour
{
    public float interval = 0.25f;
    public int maxTrails = 5;
    public GameObject trailPrefab;
    private float _ElapsedTime;
    private int _Count;
    private Queue _TrailsQueue;
    private MoveMe moveObj;


    // Start is called before the first frame update
    void Start()
    {
        _ElapsedTime = 0f;
        _Count = 0;
        _TrailsQueue = new Queue();
        moveObj = GetComponent<MoveMe>();
    }

    // Update is called once per frame
    void Update()
    {

            _ElapsedTime += Time.deltaTime;
            Debug.Log(_ElapsedTime.ToString());

            if (_ElapsedTime >= interval)
            {
                GameObject tmp = Instantiate(trailPrefab, transform);
                tmp.transform.SetParent(null);
                _TrailsQueue.Enqueue(tmp);
                _ElapsedTime = 0f;
                _Count++;
            }

            if (_TrailsQueue.Count == maxTrails)
            {
                GameObject tmp = _TrailsQueue.Dequeue() as GameObject;
                Destroy(tmp);
            }
     
    }
}
