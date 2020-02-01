using UnityEngine;

namespace idgag.GameState.LaneSections
{
    public abstract class LaneSection : MonoBehaviour
    {
        [SerializeField] protected Vector3 aiPosition;
        public uint numAi;

        public abstract bool IsAllowedToPass();

        public Vector3 GetAiPosition()
        {
            return aiPosition;
        }
    }
}
