using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    public MoveCamera moveCamera;
    public PlayerMovement playerMovement;
    public GameObject deadText;
    public Tentacle tentacle;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && !other.GetComponent<ArrowMove>().hitGround)
        {
            tentacle.enabled = false;

            if (moveCamera.Warrior != null)
            {
                moveCamera.Warrior.GetComponent<Warrior>().enabled = false;
                moveCamera.Warrior.GetComponent<FireAndReload>().enabled = false;
            }

            moveCamera.enabled = false;

            deadText.SetActive(true);

            playerMovement.enabled = false;
        }
    }
}
