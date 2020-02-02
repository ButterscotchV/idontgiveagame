using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FuckBomb : MonoBehaviour
{
    public float delay = 2.0f;
    public float radius = 5.0f;
    public float force = 2000.0f;
    public GameObject explosionEffect;


    bool hasExploded = false;
    float countdown;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown<=0.0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }


    void Explode()
    {
        // show effect
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // get nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyOBJ in colliders)
        {
            Rigidbody rb = nearbyOBJ.GetComponent<Rigidbody>();
            NavMeshAgent agent = nearbyOBJ.GetComponent<NavMeshAgent>();
            if (rb!=null)
            {
                if(agent)
                {
                    //if(m_TargetPeople == nearbyOBJ.gameObject.tag)
                    {
                        nearbyOBJ.transform.position += new Vector3(0, 1.5f, 0);
                        agent.enabled = false;
                    }
                }
                
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

       

        //remove grenade
        Destroy(gameObject);
    }

    void setTarget(string t)
    {
        m_TargetPeople = t;
    }


    string m_TargetPeople;
}
