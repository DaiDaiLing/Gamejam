using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowMove : MonoBehaviour
{
    public float speed = 0.1f;

    public Vector3 endPoint;

    float passTime = 0f;
    public bool hitGround = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!hitGround)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
        }

        passTime += Time.deltaTime;
        if (passTime > 60)
        {
            Destroy(gameObject);
        }
    }

    
}
