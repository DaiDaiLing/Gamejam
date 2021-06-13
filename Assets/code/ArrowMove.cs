using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public FireAndReload FireAndReload;

    public float speed = 0.1f;

    Vector3 startPoint;
    Vector3 endPoint;

    void Start()
    {
        //transform.position = FireAndReload.ArrowLoadedPosition;
        startPoint = transform.position;
        endPoint = FireAndReload.hitPoint;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
    }
}
