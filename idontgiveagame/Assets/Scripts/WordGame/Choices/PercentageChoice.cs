using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace idgag.WordGame {

    public class PercentageChoice : Choice
    {

        public PercentageChoice(string max) : base() {
            this.operation = ChoiceOperation.ADD;

            int minimum = 0;
            int maximum = 100;

            Int32.TryParse(max, out maximum);

            maximum = Mathf.Clamp(maximum, 0, 100);

            int numberOfOptions = 10;
            int spreadPerOption = (maximum - minimum) / numberOfOptions;
            for (int i = 1; i < numberOfOptions + 1; i++) {
                Option newOption;
                int label = minimum + (i * spreadPerOption);

                newOption.value = label / 100 * 5;
                if (!options.ContainsKey(label.ToString())) {
                    options.Add(label.ToString(), newOption);
                }
            }
        }

        public PercentageChoice() : base() {
            operation = ChoiceOperation.ADD;

            int minimum = 0;
            int maximum = 100;

            maximum = Mathf.Clamp(maximum, 0, 100);

            int numberOfOptions = 10;
            int spreadPerOption = (maximum - minimum) / numberOfOptions;
            for (int i = 1; i < numberOfOptions + 1; i++) {
                Option newOption;
                int label = minimum + (i * spreadPerOption);

                newOption.value = label / 100 * 5;
                if (!options.ContainsKey(label.ToString())) {
                    options.Add(label.ToString(), newOption);
                }
            }
        }
    }

}
