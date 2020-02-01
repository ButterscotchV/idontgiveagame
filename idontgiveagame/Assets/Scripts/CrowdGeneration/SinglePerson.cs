using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SinglePerson : MonoBehaviour
{


    //public virtual void Init()
    //{
    //    m_CrowdGeneratorRef = GameObject.FindGameObjectWithTag("CrowdGenerator").GetComponent<CrowdGenerator>();
    //    m_PoolPos = m_CrowdGeneratorRef.PoolPos;
    //}

    public abstract void shout();

    protected CrowdGenerator m_CrowdGeneratorRef;
    protected Vector3 m_PoolPos;


}
