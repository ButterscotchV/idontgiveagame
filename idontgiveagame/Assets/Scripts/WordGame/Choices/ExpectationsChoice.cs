using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using idgag.GameState;
using System.Linq;

namespace idgag.WordGame {

    public class ExpectationsChoice : Choice
    {
        public ExpectationsChoice(string arg1, string arg2) : base() { initialize(); }
        public ExpectationsChoice(string arg1) : base() { initialize(); }
        public ExpectationsChoice() : base() { initialize();}

        private void initialize() {
            Option great;
            great.value = 2;
            options.Add("great", great);

            Option good;
            good.value = 1;
            options.Add("good", good);

            Option okay;
            okay.value = 0;
            options.Add("okay", okay);

            Option poor;
            poor.value = -1;
            options.Add("poor", poor);

            Option terrible;
            terrible.value = -2;
            options.Add("terrible", terrible);
        }
    }

}
