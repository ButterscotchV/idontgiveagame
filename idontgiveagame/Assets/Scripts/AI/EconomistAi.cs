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

        private new void Awake()
        {
            fucksPercentThreshold = Random.Range(0.50f, 0.75f);
            base.Awake();
        }

        protected override void AnimateRiot()
        {
            //Debug.Log("I'm very angry");
        }

        protected override void AnimateWalk()
        {
            //Debug.Log("Aight imma head out");
        }

        public override void RunAiLogic()
        {
            if (!GameState.GameState.Singleton.fuckBucketPercentages.TryGetValue(FuckBucketTarget.Economy, out float fuckBucketPercent))
                return;

            if (fuckBucketPercent < fucksPercentThreshold)
            {
                TryMoveForward();
            }
            else
            {
                Remove();
            }
        }
    }
}
