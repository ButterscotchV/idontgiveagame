using System;
using System.Collections.Generic;
using System.Linq;
using idgag.AI;
using idgag.GameState.LaneSections;
using idgag.WordGame;
using UnityEngine;
using UnityEngine.UI;

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
        [NonSerialized] public Canvas menuCanvas;

        public GameObject crowdGeneratorPrefab;
        public CrowdGenerator CrowdGenerator { get; private set; }

        public PresenterAnimator presenterAnimator;

        public static GameState Singleton { get; private set; }

        private void Awake() {
            Singleton = this;

            GameObject menu = Instantiate(menuPrefab);
            menuCanvas = menu.GetComponent<Canvas>();

            GameObject crowdObj = Instantiate(crowdGeneratorPrefab, transform);
            CrowdGenerator = crowdObj.GetComponent<CrowdGenerator>();

            presenterAnimator = FindObjectOfType<PresenterAnimator>();

            foreach (FuckBucketTarget fuckBucketTarget in Enum.GetValues(typeof(FuckBucketTarget))) {
                fuckBuckets.Add(fuckBucketTarget, 50);
            }

            RunRound();
            PresentPRStatement();

        }

        private void OnDestroy() {
            if (Singleton == this)
                Singleton = null;
        }

        public void GenerateFuckBucketPercentages()
        {
            if (fuckBuckets.Count <= 0)
                return;

            int totalFucks = fuckBuckets.Sum(fuckBucket => fuckBucket.Value);

            foreach (KeyValuePair<FuckBucketTarget, int> fuckBucket in fuckBuckets)
            {
                fuckBucketPercentages[fuckBucket.Key] = totalFucks > 0 ? fuckBucket.Value / (float)totalFucks : 1 / (float)fuckBuckets.Count;
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
            foreach (Lane lane in lanes)
            {
                foreach (AiController laneAiController in lane.AiControllers)
                {
                    laneAiController.RunAiLogic();
                }
            }
        }

        public void SpawnAi()
        {
            foreach (Lane lane in lanes)
            {
                if (!lane.MoreAiAllowed)
                    continue;

                CrowdGenerator.GenerateActiveCrowd(CrowdGenerator.TotalPPLPerWave, fuckBucketPercentages[FuckBucketTarget.Economy], fuckBucketPercentages[FuckBucketTarget.Environment], lane);

                // Plotting doesn't seem to be working, it's disabled for now
                CrowdGenerator.m_ActiveBusinessCrowd.Clear();
                CrowdGenerator.m_ActiveEnvironmentalCrowd.Clear();
                //CrowdGenerator.Plot(lane.offset_horizontal, lane.offset_vertical, lane.Column_Max, lane.BusinessAppearLoc, lane.EnvironmentalAppearLoc);
            }
        }

        private readonly WaitForSeconds presenterWait = new WaitForSeconds(3);
        public IEnumerator<object> PresenterAnimationCoroutine()
        {
            presenterAnimator.Present(true);

            yield return presenterWait;

            presenterAnimator.Present(false);
        }

        public void RunRound()
        {
            if (presenterAnimator != null)
                StartCoroutine(PresenterAnimationCoroutine());

            GenerateFuckBucketPercentages();
            RunAiTick();
            SpawnAi();
        }

        public void PresentPRStatement() {
            if (statement == null) {
                statement = new PRStatement();

                Sentence sentence = statement.getSentence();

                string prefabPath = "WordGame/Word";
                string teleprompterPath = "WordGame/Teleprompter";
                string dropdownPath = "WordGame/Choice";

                GameObject teleprompter = Instantiate(Resources.Load<GameObject>(teleprompterPath), menuCanvas.gameObject.transform);
                Transform wordParent = teleprompter.transform.Find("Panel/Viewport/Content/WordPanel");

                // do unity loading stuff for UI here

                List<Word> words = sentence.getWords();
                for (int i = 0; i < words.Count; i++) {
                    Word word = words.ElementAt(i);

                    // Display vanilla word
                    if (!word.isOption()) {
                        GameObject newVanillaWordObject = Instantiate(Resources.Load<GameObject>(prefabPath), wordParent.transform);
                        WordUI ui = newVanillaWordObject.GetComponent<WordUI>();

                        ui.setText(word.getVanillaWord());
                        
                        continue;
                    }

                    // Add choice dropdown
                    GameObject newChoiceObject = Instantiate(Resources.Load<GameObject>(dropdownPath), wordParent.transform);
                    ChoiceUI choiceObject = newChoiceObject.GetComponent<ChoiceUI>();

                    if (word.isOption()) {
                        choiceObject.SetOptions(word.getChoice().options.Keys.ToList(), i);
                    }

                    choiceObject.enabled = true;
                }

                Transform ok = teleprompter.transform.Find("ButtonPanel").Find("OK");
                Button okButton = ok.gameObject.GetComponent<Button>();

                okButton.onClick.AddListener(delegate {
                    okButton.onClick.RemoveAllListeners();
                    this.SubmitPrStatements();
                    this.RunRound();
                    Destroy(teleprompter);
                    statement = null;
                    PresentPRStatement();

                });
            }
        }

        public void SubmitPrStatements() {
            List<FucksBucketMod> fucksBucketMods = statement.getSentence().CalculateFuckBuckets();


            foreach (FucksBucketMod fucksBucketMod in fucksBucketMods) {
                if (fuckBuckets.ContainsKey(fucksBucketMod.fucksBucketKey)) { 
                    fuckBuckets[fucksBucketMod.fucksBucketKey] += fucksBucketMod.baseChange * fucksBucketMod.modifier;

                    Debug.Log(fuckBuckets[fucksBucketMod.fucksBucketKey]);
                }
                else 
                    Debug.Log($"{nameof(fucksBucketMod)} provided invalid {nameof(fucksBucketMod.fucksBucketKey)}");

            }
        }

        public void AlterPRStatements(int index, string value) {
            if (statement != null) {
                statement.getSentence().ChooseOption(index, value);
            }
        }
    }
}
