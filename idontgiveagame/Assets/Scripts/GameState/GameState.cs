using System;
using System.Collections.Generic;
using UnityEngine;

namespace idgag.GameState
{
    public class GameState : MonoBehaviour
    {
        public static GameState Singleton;

        public Dictionary<FuckBucketTarget, int> fuckBuckets = new Dictionary<FuckBucketTarget, int>() {
            { FuckBucketTarget.Economy, 0 },
            { FuckBucketTarget.Environment, 0 }
        };

        private void Awake()
        {
            Singleton = this;

            foreach (FuckBucketTarget fuckBucketTarget in Enum.GetValues(typeof(FuckBucketTarget)))
            {
                fuckBuckets.Add(fuckBucketTarget, 0);
            }
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
                if (fuckBuckets.ContainsKey(fucksBucketMod.fucksBucketKey))
                    fuckBuckets[fucksBucketMod.fucksBucketKey] += fucksBucketMod.baseChange * fucksBucketMod.modifier;
                else
                    Debug.Log($"{nameof(fucksBucketMod)} provided invalid {nameof(fucksBucketMod.fucksBucketKey)}");
            }
        }
    }
}
