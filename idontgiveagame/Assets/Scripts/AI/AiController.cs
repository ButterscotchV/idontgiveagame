using System;
using idgag.GameState;
using idgag.GameState.LaneSections;
using UnityEngine;
using UnityEngine.AI;

namespace idgag.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class AiController : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;

        public Lane lane;
        private int laneSectionIndex = -1;

        public bool testMoving;

        // Start is called before the first frame update
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            Debug.Assert(navMeshAgent != null, $"{nameof(NavMeshAgent)} could not be found on the {nameof(GameObject)}");
        }

        private void LateUpdate()
        {
            if (testMoving)
            {
                TryMoveForward();
                testMoving = false;
            }
        }

        public abstract void RunAiLogic();

        public bool TryMoveForward()
        {
            LaneSection[] laneSections = lane.LaneSections;

            if (laneSectionIndex < laneSections.Length - 1)
            {
                if (laneSectionIndex >= 0)
                {
                    LaneSection curSection = laneSections[laneSectionIndex];

                    if (!curSection.IsAllowedToPass())
                        return false;

                    curSection.numAi--;
                }

                laneSectionIndex++;
                LaneSection newSection = laneSections[laneSectionIndex];
                newSection.numAi++;

                Vector3 destPos = newSection.GetAiPosition();

                if (!SetDestination(destPos))
                    Warp(destPos);

                return true;
            }

            return false;
        }

        public void ResetController(Vector3 newPosition)
        {
            Warp(newPosition);
            SetDestination(newPosition);
            navMeshAgent.isStopped = true;

            laneSectionIndex = -1;
        }

        public void Warp(Vector3 newPosition)
        {
            navMeshAgent.Warp(newPosition);
        }

        public bool SetDestination(Vector3 newDestination)
        {
            return navMeshAgent.SetDestination(newDestination);
        }
    }
}
