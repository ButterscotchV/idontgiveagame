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
        }

        private void LateUpdate()
        {
            if (navMeshAgent.isOnNavMesh)
                navMeshAgent.destination = testDestination;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Floor") && !navMeshAgent.isOnNavMesh) return;

            Debug.Log("Collided");

            navMeshAgent.enabled = false;
            navMeshAgent.enabled = true;

            navMeshAgent.destination = testDestination;
        }
    }
}
