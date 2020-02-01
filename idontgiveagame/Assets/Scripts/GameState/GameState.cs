using System;
using System.Collections.Generic;
using idgag.WordGame;
using UnityEngine;

namespace idgag.GameState
{
    public class GameState : MonoBehaviour
    {
        private PRStatement statement;

        public readonly Dictionary<FuckBucketTarget, int> fuckBuckets = new Dictionary<FuckBucketTarget, int>();
        [SerializeField] private Lane[] lanes;

        public Lane[] Lanes => lanes;
        public static GameState Singleton { get; private set; }

        private void Awake() {
            Singleton = this;

            this.statement = null;
            PresentPRStatement();

            foreach (FuckBucketTarget fuckBucketTarget in Enum.GetValues(typeof(FuckBucketTarget))) {
                fuckBuckets.Add(fuckBucketTarget, 0);
            }
        }

        private void OnDestroy() {
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

        public void PresentPRStatement() {
            if (statement == null) {
                statement = new PRStatement();

                Sentence sentence = statement.getSentence();
                // do unity loading stuff for UI here
                foreach (Word word in sentence.getWords()) {
                    if (!word.isOption()) {
                        Debug.Log(word.getVanillaWord());
                        continue;
                    }

                    Debug.Log(word.getChoices().Keys);
                }
            }
        }




        public void SubmitPrStatements(List<FucksBucketMod> fucksBucketMods) {
            foreach (FucksBucketMod fucksBucketMod in fucksBucketMods) {
                if (fuckBuckets.ContainsKey(fucksBucketMod.fucksBucketKey))
                    fuckBuckets[fucksBucketMod.fucksBucketKey] += fucksBucketMod.baseChange * fucksBucketMod.modifier;
                else
                    Debug.Log($"{nameof(fucksBucketMod)} provided invalid {nameof(fucksBucketMod.fucksBucketKey)}");
            }
        }
    }
}
