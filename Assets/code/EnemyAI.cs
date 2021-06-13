using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    float fireCooldown = 2f;
    float currentFireCooldown = 0f;

    public Transform Arrow;
    public Vector3 ArrowLoadedPosition;
    public GameObject moveingArrow;

    // Start is called before the first frame update
    void Awake()
    {
        ArrowLoadedPosition = Arrow.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 100f);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.layer == 11)
            {
                transform.LookAt(cols[i].transform);

                if (currentFireCooldown <= 0)
                {
                    GameObject e = Instantiate(moveingArrow) as GameObject;
                    e.transform.position = Arrow.position;
                    e.transform.rotation = Arrow.rotation;
                    e.GetComponent<ArrowMove>().endPoint = cols[i].transform.position;

                    currentFireCooldown = fireCooldown;
                }
                else
                {
                    currentFireCooldown -= Time.deltaTime;
                }
            }
        }
           
    }
}
