using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask canGrappleable;
    public Transform TentacleTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    public Transform playerPosition;

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
        
        if (Input.GetMouseButtonDown(0)&&Input.GetMouseButton(1))
        {
            HookForward();
            Debug.Log(0);
        }
    }

    void LateUpdate()
    {
        DrawRope();

    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, canGrappleable)) 
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = TentacleTip.position;

            
            
        }

    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, TentacleTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
    
    void HookForward()
    {
        playerPosition.transform.position = Vector3.MoveTowards(playerPosition.position, grapplePoint, 10.0f * Time.deltaTime);
    }
}
