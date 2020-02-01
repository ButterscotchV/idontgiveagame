using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace idgag.WordGame
{
    public class PRStatement
    {
        private Sentence prSentence;

        public PRStatement() {
            List<Sentence> sentences = Sentence.getSentences();

            System.Random r = new System.Random();
            int sentence = r.Next(0, sentences.Count);
            Debug.Log("Chose PR Sentence!");
            Debug.Log(sentence);
            Debug.Log(sentences.ElementAt(sentence));

            prSentence = sentences.ElementAt(sentence);
        }

        public Sentence getSentence() {
            Debug.Log("Get PR Sentence!");
            return prSentence;
        }

    }
}
