using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    float timer = 0f;

    bool flying = false;
    float countHolding = 0f;

    public LineRenderer lr;
    public Vector3 grapplePoint;
    public LayerMask canGrappleable;
    public Transform TentacleTip, camera, player;
    private float maxDistance = 100f;
    public SpringJoint joint;
    public Transform playerPosition;
    public GameObject prediction;

    float distanceFromPoint;

    void Awake()
    {
        lr = this.GetComponent<LineRenderer>();
        prediction.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
            
        }
        else if(Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }

        Pull();

        if (Physics.SphereCast(camera.position, 1f, camera.forward, out RaycastHit hit, maxDistance, canGrappleable))
        {
            prediction.SetActive(true);
            prediction.transform.position = hit.point;
        }
        else
        {
            prediction.SetActive(false);
        }


    }

    void LateUpdate()
    {
        DrawRope();

    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.SphereCast(camera.position, 2f, camera.forward, out hit, maxDistance, canGrappleable)) 
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.7f;
            joint.minDistance = distanceFromPoint * 0f;
            
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            joint.tolerance = 0.01f;

            lr.positionCount = 2;
            currentGrapplePosition = TentacleTip.position;

            
            
        }

    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);

        countHolding = 0f;
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 4f);

        lr.SetPosition(0, TentacleTip.position);
        lr.SetPosition(1, currentGrapplePosition);
        /*
        timer += Time.deltaTime;
        if (currentGrapplePosition == grapplePoint) 
        {
            Debug.Log(timer);
            timer = 0f;
        }*/
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    void Pull()
    {
        if (Input.GetMouseButton(0))
        {
            countHolding += Time.deltaTime;
        }
        /*if (Input.GetMouseButtonDown(0) && !clickOneTime && joint)
        {
            clickOneTime = true;
        }*/
        if (Input.GetMouseButtonDown(1) /*&& clickOneTime*/ && countHolding <= 0.3f && joint)
        {
            //clickOneTime = false;

            flying = true;
            joint.spring = 20f;
            joint.damper = 2f;

        }
        if (flying)
        {
            if (Vector3.Distance(playerPosition.position, grapplePoint) < 7f || countHolding > 1f) 
            {
                joint.spring = 4.5f;
                joint.damper = 7f;
                flying = false;
                Debug.Log("end");
            }
        }
        else if (countHolding > 0.3f)
        {
            //clickOneTime = false;

            //countHolding = 0f;

            joint.spring = 4.5f;

        }
        else if (countHolding > 0.5f)
        {
            joint.damper = 7f;
        }
        
        
    }
}
