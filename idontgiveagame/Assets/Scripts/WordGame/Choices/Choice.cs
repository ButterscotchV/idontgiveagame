using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace idgag.WordGame {

    public enum ChoiceOperation
    {
        MULT,
        ADD,
        DIRECTION,
        KEY
    }

    public struct Option
    {
        public int value;
    }

    public abstract class Choice
    {
        public Dictionary<string, Option> options;
        protected ChoiceOperation operation;

        public Choice() {
            options = new Dictionary<string, Option>();
        }
    }

}
