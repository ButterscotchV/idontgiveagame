using idgag.GameState;
using UnityEngine;
using UnityEngine.AI;

namespace idgag.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AiController : MonoBehaviour
    {
        [SerializeField] private Vector3 destination;
        private NavMeshAgent navMeshAgent;

        public Lane lane;
        public int laneSection = 0;

        // Start is called before the first frame update
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            Debug.Assert(navMeshAgent != null, $"{nameof(NavMeshAgent)} could not be found on the {nameof(GameObject)}");
        }

        private void LateUpdate()
        {
            if (navMeshAgent.isOnNavMesh && destination != navMeshAgent.destination)
                navMeshAgent.destination = destination;
        }

        public void Warp(Vector3 newPosition)
        {
            navMeshAgent.Warp(newPosition);
        }

        public void SetDestination(Vector3 aiDestination)
        {
            destination = aiDestination;
        }
    }
}
