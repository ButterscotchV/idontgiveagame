using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace idgag.WordGame {

    public class UIntegerChoice : Choice
    {

        public UIntegerChoice(string min, string max) : base() {
            this.operation = ChoiceOperation.ADD;

            int minimum = 0;
            int maximum = 100;

            Int32.TryParse(min, out minimum);
            Int32.TryParse(max, out maximum);

            int numberOfOptions = 10;
            int spreadPerOption = (maximum - minimum) / numberOfOptions;

            for (int i = 0; i < numberOfOptions; i++) {
                Option newOption;
                float label = (float)Mathf.Clamp(minimum + (i * spreadPerOption), minimum, maximum);
                newOption.value = (int)((label /maximum) * 5.0f);


                if (!options.ContainsKey(label.ToString())) {
                    options.Add(label.ToString(), newOption);
                }
            }


            if (!options.ContainsKey(maximum.ToString())) {
                Option newOption;
                newOption.value = 5;

                options.Add(maximum.ToString(), newOption);
            }
        }

        public UIntegerChoice(string min) : base() {
            this.operation = ChoiceOperation.ADD;

            int minimum = 0;
            int maximum = 100;

            Int32.TryParse(min, out minimum);

            int numberOfOptions = 10;
            int spreadPerOption = (maximum - minimum) / numberOfOptions;

            for (int i = 0; i < numberOfOptions; i++) {
                Option newOption;
                float label = (float)Mathf.Clamp(minimum + (i * spreadPerOption), minimum, maximum);
                newOption.value = (int)((label / maximum) * 5.0f);


                if (!options.ContainsKey(label.ToString())) {
                    options.Add(label.ToString(), newOption);
                }
            }


            if (!options.ContainsKey(maximum.ToString())) {
                Option newOption;
                newOption.value = 5;

                options.Add(maximum.ToString(), newOption);
            }
        }

        public UIntegerChoice() : base() {
            this.operation = ChoiceOperation.ADD;

            int minimum = 0;
            int maximum = 100;

            int numberOfOptions = 10;
            int spreadPerOption = (maximum - minimum) / numberOfOptions;

            for (int i = 0; i < numberOfOptions; i++) {
                Option newOption;
                float label = (float)Mathf.Clamp(minimum + (i * spreadPerOption), minimum, maximum);
                newOption.value = (int)((label / maximum) * 5.0f);

                if (!options.ContainsKey(label.ToString())) {
                    options.Add(label.ToString(), newOption);
                }
            }

            if (!options.ContainsKey(maximum.ToString())) {
                Option newOption;
                newOption.value = 5;

                options.Add(maximum.ToString(), newOption);
            }
        }
    }

}
