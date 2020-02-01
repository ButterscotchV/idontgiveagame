using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdGenerator : MonoBehaviour
{
    public int BusinessPoolSize = 100;
    public int EnvironmentalPoolSize = 100;
    public Vector3 PoolPos = new Vector3(-10000, -10000, -10000);
    public Vector3 BusinessAppearLoc = new Vector3(0, 0, 0);
    public Vector3 EnvironmentalAppearLoc = new Vector3(200, 0, 0);
    public List<SinglePerson> m_ActiveEnvironmentalCrowd;
    public List<SinglePerson> m_ActiveBusinessCrowd;
    public int TotalPPLPerWave = 100;
    public float offset_horizontal = 5.0f;
    public float offset_vertical = 5.0f;
    public int Column_Max = 10;
    public GameObject BusinessPersonPrefab;
    public GameObject EnvironmentalPersonPrefab;
    public int BusinessPercentage = 40;
    public int EnvironmentalPercentage = 60;


    // Start is called before the first frame update
    void Start()
    {
        m_BusinessCrowdPool = new List<SinglePerson>();
        m_EnvironmentalCrowdPool = new List<SinglePerson>();
        m_ActiveBusinessCrowd = new List<SinglePerson>();
        m_ActiveEnvironmentalCrowd = new List<SinglePerson>();
        SpawnPool();
    }

    // Update is called once per frame
    void Update()
    {
        
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GenerateActiveCrowd(TotalPPLPerWave);
                Plot();
                m_test = false;
            }
        

        
            if(Input.GetKeyDown(KeyCode.Keypad1))
            {
                BackToBusinessPool();
                m_test1 = false;
            }
        

        
            if(Input.GetKeyDown(KeyCode.Keypad2))
            {
                BackToEnvironmentalPool();
                m_test2 = false;
            }
        


    }

    void SpawnPool()
    {
        for (int i = 0; i < BusinessPoolSize; i++)
        {
            GameObject obj = Instantiate(BusinessPersonPrefab, PoolPos, Quaternion.identity);
            obj.SetActive(false);
            obj.transform.parent = this.transform.GetChild(0).transform;
            m_BusinessCrowdPool.Add(obj.GetComponent<BusinessPerson>());
        }
        for (int i = 0; i < EnvironmentalPoolSize; i++)
        {
            GameObject obj = Instantiate(EnvironmentalPersonPrefab, PoolPos, Quaternion.identity);
            obj.SetActive(false);
            obj.transform.parent = this.transform.GetChild(1).transform;
            m_EnvironmentalCrowdPool.Add(obj.GetComponent<EnvironmentalPerson>());
        }
    }

    void BackToBusinessPool()
    {
        foreach(BusinessPerson p in m_ActiveBusinessCrowd)
        {
            p.gameObject.SetActive(false);
            p.gameObject.transform.position = PoolPos;
        }
        m_ActiveBusinessCrowd.Clear();
       
    }
    void BackToEnvironmentalPool()
    {
        foreach(EnvironmentalPerson p in m_ActiveEnvironmentalCrowd)
        {
            p.gameObject.SetActive(false);
            p.gameObject.transform.position = PoolPos;
        }
        m_ActiveEnvironmentalCrowd.Clear();
    }

    SinglePerson GetObjFromBusinessPool()
    {
        foreach (SinglePerson p in m_BusinessCrowdPool)
        {
            if (!p.gameObject.activeInHierarchy)
            {
                return p;
            }
        }
        return null;
    }

    SinglePerson GetObjFromEnvironmentalPool()
    {
        foreach (SinglePerson p in m_EnvironmentalCrowdPool)
        {
            if (!p.gameObject.activeInHierarchy)
            {
                return p;
            }
        }
        return null;
    }

    // call this func every wave
    public void GenerateActiveCrowd(int totalPPLSize)
    {
        int randNum;
        int total = 0;


        while (total < totalPPLSize)
        {
            randNum = Random.Range(1, 101);

            if (BusinessPercentage < EnvironmentalPercentage)
            {
                // grab a business person
                if (randNum > 0 && randNum <= BusinessPercentage)
                {
                    SinglePerson p = GetObjFromBusinessPool();
                    p.gameObject.SetActive(true);
                    p.gameObject.transform.parent = this.transform.GetChild(2).transform;
                    m_ActiveBusinessCrowd.Add(p);
                }
                // grab a environmental person
                else if (randNum > BusinessPercentage && randNum < 100)
                {
                    SinglePerson p = GetObjFromEnvironmentalPool();
                    p.gameObject.SetActive(true);
                    p.gameObject.transform.parent = this.transform.GetChild(3).transform;
                    m_ActiveEnvironmentalCrowd.Add(p);
                }
                else continue;
            }
            else
            {
                // grab a environmental person
                if (randNum > 0 && randNum <= EnvironmentalPercentage)
                {
                    SinglePerson p = GetObjFromEnvironmentalPool();
                    p.gameObject.SetActive(true);
                    m_ActiveEnvironmentalCrowd.Add(p);
                }
                // grab a business person
                else if (randNum > EnvironmentalPercentage && randNum < 100)
                {
                    SinglePerson p = GetObjFromBusinessPool();
                    p.gameObject.SetActive(true);
                    m_ActiveBusinessCrowd.Add(p);
                }
                else continue;
            }
            total++;
        }
    }

    public void Plot()
    {
        PlotBusinessPeople();
        PlotEnvironmentalPeople();
    }
    public void PlotBusinessPeople()
    {
        Vector3 offsetHorizontal = new Vector3(offset_horizontal, 0, 0);
        Vector3 offsetVertical = new Vector3(0, offset_vertical, 0);
        int steps_x = 0;
        int steps_y = 0;
        foreach (SinglePerson p in m_ActiveBusinessCrowd)
        {
            p.transform.position = BusinessAppearLoc + offsetHorizontal*steps_x + offsetVertical*steps_y;
            steps_x++;
            if(steps_x == Column_Max-1)
            {
                steps_x = 0;
                steps_y++;
            }
        }
    }

    public void PlotEnvironmentalPeople()
    {
        Vector3 offsetHorizontal = new Vector3(offset_horizontal, 0, 0);
        Vector3 offsetVertical = new Vector3(0, offset_vertical, 0);
        int steps_x = 0;
        int steps_y = 0;
        foreach (SinglePerson p in m_ActiveEnvironmentalCrowd)
        {
            p.transform.position = EnvironmentalAppearLoc + offsetHorizontal*steps_x + offsetVertical*steps_y;
            steps_x++;
            if(steps_x == Column_Max-1)
            {
                steps_x = 0;
                steps_y++;
            }
        }
    }


    List<SinglePerson> m_CurrentWave;
    List<SinglePerson> m_BusinessCrowdPool;
    List<SinglePerson> m_EnvironmentalCrowdPool;
    bool m_test = true;
    bool m_test1 = true;
    bool m_test2 = true;



}
