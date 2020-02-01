using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace idgag.GameState.LaneSection
{
    public class StageSection : LaneSection
    {
        public override bool IsAllowedToPass()
        {
            return false;
        }
    }
}
