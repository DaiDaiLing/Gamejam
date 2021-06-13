using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform player;

    public Tentacle tentacle;
    public PlayerMovement playerMovement;

    public Transform playerCamPlace;

    bool switching = false;
    public bool isBinding = false;

    GameObject Warrior;

    float switchTime = 0f;
    float firstSpeed = 0f;
    public float speed = 1f;

    //float count = 0f;

    void Update() 
    {
        if (!switching)
        {
            transform.position = player.transform.position;
            if (isBinding)
            {
                tentacle.grapplePoint = transform.position - new Vector3(0, 0.24f, 0);
                
            }
        }
        else
        {
            Binding(Warrior);
        }

        if (Physics.SphereCast(transform.position, 1f, transform.forward, out RaycastHit hitWarrior, tentacle.maxDistance, 1 << 9))
        {
            if (Input.GetMouseButtonDown(0))
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
    }

    void Binding(GameObject hitWarrior)
    {
        transform.position = Vector3.Lerp(transform.position, hitWarrior.transform.GetChild(1).position, speed * Time.deltaTime * 5);

        speed = calculateNewSpeed(hitWarrior);
    }

    float calculateNewSpeed(GameObject hitWarrior)
    {
        float tmp = Vector3.Distance(transform.position, hitWarrior.transform.GetChild(1).position);

        if (tmp == 0)
        {

            //playerMovement.enabled = true;
            //tentacle.enabled = true;
            hitWarrior.GetComponent<Warrior>().enabled = true;
            player = hitWarrior.transform.GetChild(1);
            transform.localRotation = hitWarrior.transform.GetChild(2).localRotation;

            switching = false;
            return 1f;
        }
        else
        {
            return (firstSpeed / tmp);
        }

    }
}
