using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using idgag.GameState;
using System.Linq;

namespace idgag.WordGame {

    public class DirectionChoice : Choice
    {
        public DirectionChoice(string arg1, string arg2) : base() { initialize(); }
        public DirectionChoice(string arg1) : base() { initialize(); }
        public DirectionChoice() : base() { initialize();}

        private void initialize() {
            // Create decreasing option
            Option decreasing;
            decreasing.value = -1;

            options.Add("decreasing", decreasing);

            Option increasing;
            increasing.value = 1;

            options.Add("increasing", increasing);
        }
    }

}
