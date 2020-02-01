using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace idgag.GameState.LaneSection
{
    public abstract class LaneSection : MonoBehaviour
    {
        protected Vector3 aiPosition;
        public uint numAi = 0;

        public abstract bool IsAllowedToPass();

        public Vector3 GetAiPosition()
        {
            return aiPosition;
        }
    }
}
