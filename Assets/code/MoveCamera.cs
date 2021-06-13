using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform player;
    public Transform playerFacing;
    public Tentacle tentacle;
    public PlayerMovement playerMovement;

    public Transform playerCamPlace;

    bool switching = false;
    public bool isBinding = false;

    GameObject Warrior;

    float switchTime = 0f;
    float firstSpeed = 0f;
    public float speed = 0.2f;

    float tmp;
    //float count = 0f;

    void Update() 
    {
        if (!switching)
        {
            transform.position = playerCamPlace.transform.position;
            if (isBinding)
            {
                tentacle.grapplePoint = transform.position - new Vector3(0, 0.24f, 0);
                
            }
        }
        else
        {
            
            if(player != playerCamPlace)
            {
                UnBinding(Warrior);
            }
            else if (isBinding)
            {
                Binding(Warrior);
            }
        }

        if (Physics.SphereCast(transform.position, 1f, transform.forward, out RaycastHit hitWarrior, tentacle.maxDistance, 1 << 9))
        {
            if (Input.GetMouseButtonDown(1))
            {
                switching = true;
                isBinding = true;

                playerMovement.enabled = false;
                //tentacle.enabled = false;

                Warrior = hitWarrior.transform.gameObject;

                firstSpeed = Vector3.Distance(transform.position, hitWarrior.transform.GetChild(1).position) * speed;

                tentacle.prediction.SetActive(false);
            }
        }

        if (isBinding && !switching) 
        {
            if (Input.GetKeyDown(KeyCode.F) || Vector3.Distance(Warrior.transform.position, playerFacing.position) > 60f) 
            {
                switching = true;

                Warrior.GetComponent<FireAndReload>().enabled = false;
                Warrior.GetComponent<Warrior>().enabled = false;

                firstSpeed = Vector3.Distance(transform.position, player.transform.position) * speed;

                transform.LookAt(playerFacing);
            }
        }
    }

    void Binding(GameObject hitWarrior)
    {
        transform.position = Vector3.Lerp(transform.position, hitWarrior.transform.GetChild(1).position, speed * Time.deltaTime * 15);

        speed = calculateNewSpeed(hitWarrior, false);
    }

    void UnBinding(GameObject hitWarrior)
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime * 15);

        speed = calculateNewSpeed(hitWarrior, true);
    }

    float calculateNewSpeed(GameObject hitWarrior, bool isBackingFromBinding)
    {
        if (!isBackingFromBinding)
        {
            tmp = Vector3.Distance(transform.position, hitWarrior.transform.GetChild(1).position);
        }
        else
        {
            tmp = Vector3.Distance(transform.position, player.transform.position);
        }


        if (tmp == 0)
        {

            //playerMovement.enabled = true;
            //tentacle.enabled = true;
            if (!isBackingFromBinding)
            {
                hitWarrior.GetComponent<FireAndReload>().enabled = true;
                hitWarrior.GetComponent<Warrior>().enabled = true;
                playerCamPlace = hitWarrior.transform.GetChild(1);
                transform.localRotation = hitWarrior.transform.localRotation;
            }
            else
            {
                
                playerCamPlace = player;
                transform.localRotation = playerFacing.localRotation;

                isBinding = false;
                playerMovement.enabled = true;

                tentacle.StopGrapple();
            }

            switching = false;
            return 0.2f;
        }
        else
        {
            return (firstSpeed / tmp);
        }

    }
    
}
