using System;
using idgag.GameState;
using idgag.GameState.LaneSections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace idgag.AI
{
    public class EconomistAi : AiController
    {
        public float fucksPercentThreshold;

        private void Awake()
        {
            fucksPercentThreshold = Random.Range(0.50f, 0.75f);
        }

        public override void RunAiLogic()
        {
            if (GameState.GameState.Singleton.GetFuckBucketPercent(FuckBucketTarget.Economy) < fucksPercentThreshold)
            {
                TryMoveForward();
            }
            else
            {
                gameObject.SetActive(false);

                if (lane != null)
                    lane.RemoveAiController(this);
            }
        }
    }
}
