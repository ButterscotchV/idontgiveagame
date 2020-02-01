using System.Collections.Generic;
using UnityEngine;

namespace idgag.GameState
{
    public class GameState : MonoBehaviour
    {
        public static GameState Singleton;

        public Dictionary<FuckBucketTarget, FuckBucket> fuckBuckets = new Dictionary<FuckBucketTarget, FuckBucket>() {
            { FuckBucketTarget.Economy, new FuckBucket() },
            { FuckBucketTarget.Environment, new FuckBucket() }
        };

        private void Awake()
        {
            Singleton = this;
        }

        private void OnDestroy()
        {
            if (Singleton == this)
                Singleton = null;
        }

        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }

        public void SubmitPrStatement(List<WordGame.WordGame.FucksBucketMod> fucksBucketMods)
        {
            foreach (WordGame.WordGame.FucksBucketMod fucksBucketMod in fucksBucketMods)
            {
                if (fuckBuckets.TryGetValue(fucksBucketMod.fucksBucketKey, out FuckBucket fuckBucket))
                {
                    fuckBucket.numFucks += fucksBucketMod.baseChange * fucksBucketMod.modifier;
                }
            }
        }
    }
}
