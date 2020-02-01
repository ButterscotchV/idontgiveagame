using System;
using idgag.GameState;
using idgag.GameState.LaneSections;
using UnityEngine;
using UnityEngine.AI;

namespace idgag.AI
{
    public class EnvironmentalistAi : AiController
    {
        public override void RunAiLogic()
        {
            TryMoveForward();
        }
    }
}
