using UnityEngine;

namespace idgag.GameState
{
    public class GameState : MonoBehaviour
    {
        public static GameState Singleton;

        private void Awake()
        {
            Singleton = this;
        }

        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }

        public void SubmitPRStatement()
        {

        }
    }
}
