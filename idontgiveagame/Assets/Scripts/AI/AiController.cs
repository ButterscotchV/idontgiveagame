using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace idgag.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AiController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 testDestination;
        private NavMeshAgent navMeshAgent;

        // Start is called before the first frame update
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            Debug.Assert(navMeshAgent != null, $"{nameof(NavMeshAgent)} could not be found on the {nameof(GameObject)}");

            if (navMeshAgent.isOnNavMesh)
                navMeshAgent.destination = testDestination;
        }

        private void LateUpdate()
        {
            if (testDestination != navMeshAgent.destination && navMeshAgent.isOnNavMesh)
                navMeshAgent.destination = testDestination;
        }
    }
}
