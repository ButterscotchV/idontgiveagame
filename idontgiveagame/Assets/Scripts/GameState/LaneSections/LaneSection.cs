using UnityEngine;

namespace idgag.GameState.LaneSections
{
    public abstract class LaneSection : MonoBehaviour
    {
        [SerializeField] protected GameObject aiDestination;
        public uint numAi;

        private void Awake()
        {
            Debug.Assert(aiDestination != null, $"{nameof(aiDestination)} must be assigned");
        }

        public abstract bool IsAllowedToPass();

        public Vector3 GetAiPosition()
        {
            return aiDestination.transform.position;
        }
    }
}
