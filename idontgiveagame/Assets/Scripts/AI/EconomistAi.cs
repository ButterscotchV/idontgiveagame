namespace idgag.AI
{
    public class EconomistAi : AiController
    {
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
            if (!GameState.GameState.Singleton.fuckBucketPercentages.TryGetValue(fuckTarget, out float fuckBucketPercent))
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
