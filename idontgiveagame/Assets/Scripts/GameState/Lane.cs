using System.Collections.Generic;
using idgag.AI;
using idgag.GameState.LaneSections;
using UnityEngine;

namespace idgag.GameState
{
    public class Lane : MonoBehaviour
    {
        [SerializeField] private LaneSection[] laneSections;

        [SerializeField] private GameObject aiPrefab;
        [SerializeField] private GameObject aiContainer;
        [Min(1)] [SerializeField] private int maxAiCount = 100;

        private AiController[] aiInstances;

        private List<AiController> aiControllers;
        public LaneSection[] LaneSections => laneSections;

        private void Start()
        {
            Debug.Assert(aiPrefab != null, $"{nameof(aiPrefab)} must be assigned");
            Debug.Assert(aiContainer != null, $"{nameof(aiPrefab)} must be assigned");

            aiInstances = new AiController[maxAiCount];

            Debug.Log("Instantiating AI...");
            for (int i = 0; i < aiInstances.Length; i++)
            {
                GameObject prefabInstance = Instantiate(aiPrefab, aiContainer.transform);
                prefabInstance.SetActive(false);

                AiController aiController = prefabInstance.GetComponent<AiController>();

                Debug.Assert(aiController != null, $"{nameof(AiController)} wasn't properly instantiated with the ${aiPrefab}");
                // This case shouldn't happen, but just in case, this will prevent a memory leak
                if (aiController == null)
                {
                    Destroy(prefabInstance);
                    continue;
                }

                aiController.lane = this;
                aiInstances[i] = aiController;
            }
        }

        public void AddAiControllers(IEnumerable<AiController> newAiControllers)
        {
            aiControllers.AddRange(newAiControllers);
        }
    }
}
