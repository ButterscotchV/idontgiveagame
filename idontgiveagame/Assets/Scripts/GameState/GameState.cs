using System;
using System.Collections.Generic;
using System.Linq;
using idgag.AI;
using idgag.WordGame;
using UnityEngine;

namespace idgag.GameState
{
    public class GameState : MonoBehaviour
    {
        private PRStatement statement;

        public readonly Dictionary<FuckBucketTarget, int> fuckBuckets = new Dictionary<FuckBucketTarget, int>();
        public readonly Dictionary<FuckBucketTarget, float> fuckBucketPercentages = new Dictionary<FuckBucketTarget, float>();
        [SerializeField] private Lane[] lanes;

        public Lane[] Lanes => lanes;

        public GameObject menuPrefab;
        public Canvas menuCanvas;

        public static GameState Singleton { get; private set; }

        private void Awake() {
            Singleton = this;

            GameObject menu = Instantiate(menuPrefab);
            menuCanvas = menu.GetComponent<Canvas>();

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

        public void GenerateFuckBucketPercentages()
        {
            int totalFucks = fuckBuckets.Sum(fuckBucket => fuckBucket.Value);

            foreach (KeyValuePair<FuckBucketTarget, int> fuckBucket in fuckBuckets)
            {
                fuckBucketPercentages[fuckBucket.Key] = totalFucks > 0 ?fuckBucket.Value / (float)totalFucks : 0;
            }
        }

        public int CountAiAtStage()
        {
            int aiAtStage = 0;
            
            foreach (Lane lane in lanes)
            {
                LaneSection[] laneSections = lane.LaneSections;

                if (laneSections.Length <= 0)
                    continue;

                aiAtStage += laneSections[laneSections.Length - 1].numAi;
            }

            return aiAtStage;
        }

        public void RunAiTick()
        {
            GenerateFuckBucketPercentages();

            foreach (Lane lane in lanes)
            {
                foreach (AiController laneAiController in lane.AiControllers)
                {
                    laneAiController.RunAiLogic();
                }
            }
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
