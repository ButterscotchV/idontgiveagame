using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace idgag.WordGame
{
    public class Word
    {

        private static string TOKEN_REGEX = "\\${[A-Z]+((,)*[0-9]+)*}";
        bool option;
        string word;
        Dictionary<string, Choice> choices;


        public bool isOption() {
            return this.option;
        }

        public string getVanillaWord() {
            return word;
        }

        public Dictionary<string, Choice> getChoices() {
            return this.choices;
        }

        public Word(string word) {
            Regex regex = new Regex(TOKEN_REGEX);
            if (regex.IsMatch(word)) {
                this.option = true;
                if (choices == null) choices = new Dictionary<string, Choice>();

                choices.Add(word, ChoiceFactory.getChoice(word));

                this.word = "";
            } else {

                option = false;
                this.word = word;
            }
        }
    }
}
