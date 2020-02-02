using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdGenerator : MonoBehaviour
{
    public int BusinessPoolSize = 100;
    public int EnvironmentalPoolSize = 100;
    public Vector3 PoolPos = new Vector3(-10000, -10000, -10000);

    public List<SinglePerson> m_ActiveEnvironmentalCrowd;
    public List<SinglePerson> m_ActiveBusinessCrowd;
    public int TotalPPLPerWave = 100;

    public GameObject BusinessPersonPrefab;
    public GameObject EnvironmentalPersonPrefab;



    // Start is called before the first frame update
    void Awake()
    {
        m_BusinessCrowdPool = new List<SinglePerson>();
        m_EnvironmentalCrowdPool = new List<SinglePerson>();
        m_ActiveBusinessCrowd = new List<SinglePerson>();
        m_ActiveEnvironmentalCrowd = new List<SinglePerson>();
        SpawnPool();
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
    public void GenerateActiveCrowd(int totalPPLSize,int BusinessPercentage, int EnvironmentalPercentage, idgag.GameState. Lane currentLane)
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
                    idgag.AI.AiController aiController = p.GetComponent<idgag.AI.AiController>();
                    aiController.lane = currentLane;
                    currentLane.AddAiController(aiController, transform.GetChild(2).transform.position);
                    m_ActiveBusinessCrowd.Add(p);
                }
                // grab a environmental person
                else if (randNum > BusinessPercentage && randNum < 100)
                {
                    SinglePerson p = GetObjFromEnvironmentalPool();
                    p.gameObject.SetActive(true);
                    idgag.AI.AiController aiController = p.GetComponent<idgag.AI.AiController>();
                    aiController.lane = currentLane;
                    currentLane.AddAiController(aiController, transform.GetChild(3).transform.position);
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
                    idgag.AI.AiController aiController = p.GetComponent<idgag.AI.AiController>();
                    aiController.lane = currentLane;
                    currentLane.AddAiController(aiController, transform.GetChild(3).transform.position);
                    m_ActiveEnvironmentalCrowd.Add(p);
                }
                // grab a business person
                else if (randNum > EnvironmentalPercentage && randNum < 100)
                {
                    SinglePerson p = GetObjFromBusinessPool();
                    p.gameObject.SetActive(true);
                    idgag.AI.AiController aiController = p.GetComponent<idgag.AI.AiController>();
                    aiController.lane = currentLane;
                    currentLane.AddAiController(aiController, transform.GetChild(2).transform.position);
                    m_ActiveBusinessCrowd.Add(p);
                }
                else continue;
            }
            total++;
        }
    }

    public void Plot(float offset_horizontal, float offset_vertical, int Column_Max, Vector3 BusinessAppearLoc,  Vector3 EnvironmentalAppearLoc)
    {
        PlotBusinessPeople(offset_horizontal,offset_vertical,Column_Max, BusinessAppearLoc);
        PlotEnvironmentalPeople(offset_horizontal, offset_vertical, Column_Max, EnvironmentalAppearLoc);
    }
    public void PlotBusinessPeople(float offset_horizontal, float offset_vertical , int Column_Max, Vector3 BusinessAppearLoc)
    {
        Vector3 offsetHorizontal = new Vector3(offset_horizontal, 0, 0);
        Vector3 offsetVertical = new Vector3(0, 0, -offset_vertical);
        int steps_x = 0;
        int steps_z = 0;
        foreach (SinglePerson p in m_ActiveBusinessCrowd)
        {
            p.transform.position = BusinessAppearLoc + offsetHorizontal*steps_x + offsetVertical*steps_z;
            
            if(steps_x == Column_Max-1)
            {
                steps_x = 0;
                steps_z++;
                continue;
            }
            steps_x++;
        }
    }

    public void PlotEnvironmentalPeople(float offset_horizontal, float offset_vertical, int Column_Max, Vector3 EnvironmentalAppearLoc)
    {
        Vector3 offsetHorizontal = new Vector3(offset_horizontal, 0, 0);
        Vector3 offsetVertical = new Vector3(0,0 , -offset_vertical);
        int steps_x = 0;
        int steps_z = 0;
        foreach (SinglePerson p in m_ActiveEnvironmentalCrowd)
        {
            p.transform.position = EnvironmentalAppearLoc + offsetHorizontal*steps_x + offsetVertical*steps_z;
            
            if(steps_x == Column_Max-1)
            {
                steps_x = 0;
                steps_z++;
                continue;
            }
            steps_x++;
        }
    }


    List<SinglePerson> m_CurrentWave;
    List<SinglePerson> m_BusinessCrowdPool;
    List<SinglePerson> m_EnvironmentalCrowdPool;


}
