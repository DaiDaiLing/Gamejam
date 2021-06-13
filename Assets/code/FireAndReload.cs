using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAndReload : MonoBehaviour
{
    public GameObject moveingArrow;
    public Transform camera;
    public float maxDistance = 100f;
    bool loaded = true;
    float loadingTime = 0f;

    public Transform Arrow;
    public Vector3 ArrowLoadedPosition;


    public Vector3 hitPoint;

    // Start is called before the first frame update
    void Awake()
    {
        ArrowLoadedPosition = Arrow.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && loaded)
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance))
            {
                loaded = false;

                

                hitPoint = hit.point;

                GameObject e = Instantiate(moveingArrow) as GameObject;
                e.transform.position = Arrow.position;
                e.transform.rotation = Arrow.rotation;
                e.GetComponent<ArrowMove>().FireAndReload = gameObject.GetComponent<FireAndReload>();

                Arrow.localPosition = Vector3.zero;

            }
        }
        if (Input.GetMouseButton(1) && !loaded)
        {
            loadingTime += Time.deltaTime;
            if (loadingTime >= 2f)
            {
                loaded = true;
                loadingTime = 0f;

                Arrow.localPosition = ArrowLoadedPosition;

            }
            //Debug.Log(loadingTime);
        }
        if (Input.GetMouseButtonUp(1) && !loaded)
        {
            loadingTime = 0f;
            //Debug.Log(loadingTime);
        }
    }
}
