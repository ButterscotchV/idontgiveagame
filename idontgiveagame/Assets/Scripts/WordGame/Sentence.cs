using System;
using System.Collections.Generic;
using System.Linq;
using idgag.GameState;
using System.IO;
using UnityEngine;

namespace idgag.WordGame
{
    public struct FucksBucketMod
    {
        public int baseChange;
        public int modifier;
        public FuckBucketTarget fucksBucketKey;
    }

    public class Sentence
    {
        public static string FILE_PATH = "Assets/Config/whatigiveafuckabout.txt";

        List<FucksBucketMod> modifications;

        List<Word> words = new List<Word>();

        public Sentence(string sentence) {
            if (sentence == null) return;

            words = new List<Word>();

            foreach (string word in parseWords(sentence)) {
                if (word == null) continue;

                // handle word
                // is it a token
                words.Add(new Word(word));
                Debug.Log(word);
            }

        }

        public List<Word> getWords() {
            return words;
        }

        private static List<string> parseWords(string sentence) {
            string[] words = sentence.Split(' ');
            return words.OfType<string>().ToList();
        }

        public static List<Sentence> getSentences() {
            FileInfo source = new FileInfo(FILE_PATH);
            StreamReader reader = source.OpenText();
            string text = "";

            List<Sentence> allSentences = new List<Sentence>();

            while (text != null) {
                text = reader.ReadLine();

                Sentence newSentence = new Sentence(text);
                if (newSentence != null) allSentences.Add(newSentence);
            }

            return allSentences;
        }
    }
}