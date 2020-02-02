using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace idgag.WordGame
{
    public class Word
    {

        private static string TOKEN_REGEX = "\\${[A-Z]+((,)*[0-9]+)*}";
        bool option;
        string word;
        Choice choice;


        public bool isOption() {
            return this.option;
        }

        public string getVanillaWord() {
            return word;
        }

        public Choice getChoice() {
            return this.choice;
        }

        public Word(string word) {
            Regex regex = new Regex(TOKEN_REGEX);
            if (regex.IsMatch(word)) {
                this.option = true;

                choice = ChoiceFactory.getChoice(word);

                this.word = "";
            } else {

                option = false;
                this.word = word;
            }
        }
    }
}
