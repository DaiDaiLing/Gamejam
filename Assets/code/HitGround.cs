using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGround : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            other.GetComponent<ArrowMove>().hitGround = true;
        }
    }
}
