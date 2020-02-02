using System.Collections.Generic;
using idgag.AI;
using idgag.GameState.LaneSections;
using UnityEngine;

namespace idgag.GameState
{
    public class Lane : MonoBehaviour
    {
        [SerializeField] private LaneSection[] laneSections;

        //[SerializeField] private GameObject aiPrefab;
        //[SerializeField] private GameObject aiContainer;
        [Min(1)] [SerializeField] private int maxAiCount = 10;

        private AiController[] aiInstances;

        //private List<AiController> aiControllers;
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


            aiInstances = new AiController[maxAiCount];

            GameObject crowdOBJ = Instantiate(CrowdGeneratorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            crowdOBJ.transform.parent = this.transform;
            BusinessAppearLoc = this.transform.position + new Vector3(-1.4f,0,0);
            EnvironmentalAppearLoc = BusinessAppearLoc + new Vector3(0, 0, -10);
            m_CrowdGenerator = crowdOBJ.GetComponent<CrowdGenerator>();
            m_CrowdGenerator.GenerateActiveCrowd(maxAiCount, 40, 60,this);//testing, 40% of business person, 60% of environmental person
            m_CrowdGenerator.Plot(offset_horizontal, offset_vertical, Column_Max, BusinessAppearLoc, EnvironmentalAppearLoc);
        }

        //public void AddAiControllers(IEnumerable<AiController> newAiControllers)
        //{
        //    aiControllers.AddRange(newAiControllers);
        //}

        private CrowdGenerator m_CrowdGenerator;
    }


}
