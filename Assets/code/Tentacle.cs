using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    float timer = 0f;

    bool clickOneTime = false;
    float countHolding = 0f;

    public LineRenderer lr;
    public Vector3 grapplePoint;
    public LayerMask canGrappleable;
    public Transform TentacleTip, camera, player;
    private float maxDistance = 100f;
    public SpringJoint joint;
    public Transform playerPosition;

    float distanceFromPoint;

    void Awake()
    {
        lr = this.GetComponent<LineRenderer>();
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

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0f;

            joint.spring = 0f;
            joint.damper = 0f;
            joint.massScale = 4.5f;

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


            joint.spring = 100f;


        }
        if (countHolding > 0.3f)
        {
            //clickOneTime = false;

            //countHolding = 0f;

            joint.spring = 4.5f;

        }
        if (countHolding > 0.5f)
        {
            joint.damper = 7f;
        }
        Debug.Log(countHolding);
    }
}
