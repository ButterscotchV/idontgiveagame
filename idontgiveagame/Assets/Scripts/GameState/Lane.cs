using System;
using System.Collections.Generic;
using idgag.AI;
using idgag.GameState.LaneSections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace idgag.GameState
{
    public class Lane : MonoBehaviour
    {
        [SerializeField] private LaneSection[] laneSections;

        //[SerializeField] private GameObject aiPrefab;
       // [SerializeField] private GameObject aiContainer;
        [Min(1)] [SerializeField] private int maxAiCount = 10;

        public float randomSpawnRange = 1;

        private List<AiController> aiControllers = new List<AiController>();
        public LaneSection[] LaneSections => laneSections;

        public GameObject CrowdGeneratorPrefab;

        public float laneWidth = 5.0f;
        public float offset_horizontal = 1.5f;
        public float offset_vertical = 2.0f;
        public int Column_Max = 3;
        public Vector3 BusinessAppearLoc;
        public Vector3 EnvironmentalAppearLoc;
        private void Start()
        {


            m_aiInstances = new AiController[maxAiCount];

            GameObject crowdOBJ = Instantiate(CrowdGeneratorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            crowdOBJ.transform.parent = this.transform;
            BusinessAppearLoc = this.transform.position + new Vector3(-1.4f, 3, 0);
            EnvironmentalAppearLoc = BusinessAppearLoc + new Vector3(0, 0, -10);
            m_CrowdGenerator = crowdOBJ.GetComponent<CrowdGenerator>();
            m_CrowdGenerator.GenerateActiveCrowd(maxAiCount, 40, 60, this);//testing, 40% of business person, 60% of environmental person
            m_CrowdGenerator.Plot(offset_horizontal, offset_vertical, Column_Max, BusinessAppearLoc, EnvironmentalAppearLoc);

            AddAiControllers(m_aiInstances);
        }

        public void AssignaiInstances(AiController[] ai, int index,AiController ac)
        {
            ai[index] = ac;
        }

        public void AddAiController(AiController newAiController)
        {
            if (newAiController == null)
                return;

            newAiController.lane = this;
            newAiController.gameObject.SetActive(true);

            Vector3 spawnOffset = new Vector3(Random.Range(-randomSpawnRange, randomSpawnRange), 0, Random.Range(-randomSpawnRange, randomSpawnRange));
            newAiController.ResetController(laneSections.Length > 0 ? laneSections[0].GetAiPosition() + spawnOffset : Vector3.zero);

            newAiController.TryMoveToStart();

            aiControllers.Add(newAiController);
        }

        public void AddAiControllers(params AiController[] newAiControllers)
        {
            foreach (AiController aiController in newAiControllers)
            {
                try
                {
                    AddAiController(aiController);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        public void AddAiControllers(IEnumerable<AiController> newAiControllers)
        {
            foreach (AiController aiController in newAiControllers)
            {
                try
                {
                    AddAiController(aiController);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        public void RemoveAiController(AiController aiControllerToRemove)
        {
            if (aiControllerToRemove == null)
                return;

            aiControllers.Remove(aiControllerToRemove);
        }



        public AiController[] m_aiInstances;
        private CrowdGenerator m_CrowdGenerator;
    }
}
