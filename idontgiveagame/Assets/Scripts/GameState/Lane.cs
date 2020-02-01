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

        [SerializeField] private GameObject aiPrefab;
        [SerializeField] private GameObject aiContainer;
        [Min(1)] [SerializeField] private int maxAiCount = 100;

        public float randomSpawnRange = 1;

        private List<AiController> aiControllers = new List<AiController>();
        public LaneSection[] LaneSections => laneSections;

        private void Start()
        {
            Debug.Assert(aiPrefab != null, $"{nameof(aiPrefab)} must be assigned");
            Debug.Assert(aiContainer != null, $"{nameof(aiPrefab)} must be assigned");

            AiController[] aiInstances = new AiController[maxAiCount];

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

            AddAiControllers(aiInstances);
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
    }
}
