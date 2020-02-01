using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using idgag.GameState;

namespace idgag.WordGame
{
    public class WordGame : MonoBehaviour
    {
        public struct FucksBucketMod
        {
            int baseChange;
            int modifier;
            FuckBucketTarget fucksBucketKey;
        }

        List<FucksBucketMod> modifications;

        // Start is called before the first frame update
        void Start() {
            foreach (FuckBucketTarget foo in
                System.Enum.GetValues(typeof(FuckBucketTarget))) {


            }
        }

        // Update is called once per frame
        void Update() {

        }
    }
}
