using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace idgag.WordGame {

    public class PercentageChoice : Choice
    {
        public PercentageChoice(string min, string max) : base() {
            this.operation = ChoiceOperation.ADD;

            int minimum = 0;
            int maximum = 100;

            Int32.TryParse(max, out maximum);

            maximum = Mathf.Clamp(maximum, 0, 100);

            int numberOfOptions = 10;
            int spreadPerOption = (maximum - minimum) / numberOfOptions;
            for (int i = 1; i < numberOfOptions + 1; i++) {
                Option newOption = new Option();
                int label = minimum + (i * spreadPerOption);

                newOption.value = label;
                if (!options.ContainsKey(label.ToString())) {
                    options.Add(label.ToString(), newOption);
                }
            }

            if (!options.ContainsKey(maximum.ToString())) {
                Option newOption = new Option();
                newOption.value = 5;

                options.Add(maximum.ToString(), newOption);
            }
        }

        public PercentageChoice(string max) : base() {
            this.operation = ChoiceOperation.ADD;

            int minimum = 0;
            int maximum = 100;

            Int32.TryParse(max, out maximum);

            maximum = Mathf.Clamp(maximum, 0, 100);

            for (int i = 1; i < maximum + 1; i++) {
                Option newOption = new Option();
                int label = 0 + (i);

                newOption.value = label / 100 * 5;
                if (!options.ContainsKey(label.ToString())) {
                    options.Add(label.ToString(), newOption);
                }
            }

            if (!options.ContainsKey(maximum.ToString())) {
                Option newOption = new Option();
                newOption.value = 5;

                options.Add(maximum.ToString(), newOption);
            }
        }

        public PercentageChoice() : base() {
            operation = ChoiceOperation.ADD;

            int minimum = 0;
            int maximum = 100;

            int numberOfOptions = 10;
            int spreadPerOption = (maximum - minimum) / numberOfOptions;
            for (int i = 0; i < numberOfOptions + 1; i++) {
                Option newOption = new Option();
                int label = 0 + (i * spreadPerOption);

                newOption.value = label / 100 * 5;
                if (!options.ContainsKey(label.ToString())) {
                    options.Add(label.ToString(), newOption);
                }
            }

            if (!options.ContainsKey(maximum.ToString())) {
                Option newOption = new Option();
                newOption.value = 5;

                options.Add(maximum.ToString(), newOption);
            }
        }
    }

}
