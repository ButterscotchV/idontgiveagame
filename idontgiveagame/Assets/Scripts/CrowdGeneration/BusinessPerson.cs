using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessPerson : SinglePerson
{
    //public override void Init()
    //{
    //    base.Init();
    //    Instantiate(m_CrowdGeneratorRef.SinglePersonPrefab, m_PoolPos, Quaternion.identity);
    //    this.gameObject.GetComponent<Renderer>().material.color = Color.red;
    //}



    public override void shout()
    {
        Debug.Log("I care about the business!");
    }
}
