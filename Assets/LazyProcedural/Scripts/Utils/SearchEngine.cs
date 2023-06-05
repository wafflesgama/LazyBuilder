using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sceelix.Core.Procedures;

namespace LazyProcedural
{

    public class SearchEngine
    {
        public static List<ProcedureInfo> FuzzySearch(IEnumerable<ProcedureInfo> source, string key)
        {
            // Char match - then - all char match - then - Label char match

            return source.
                Where(x => CharMatch_Combined(x, key) > (int)(key.Length/2)).
                OrderByDescending(x => CharMatch_Label(x, key)).
                ThenByDescending(x => WordMatch_Combined(x, key)).
                ThenBy(x => DistanceToMatch_Label(x, key)).
                ToList();
        }

        public static int DistanceToMatch_Label(ProcedureInfo source,string key)
        {
            var index = source.Label.ToLower().IndexOf(key.ToLower());
            return index == -1 ? int.MaxValue: index;
        }

        public static int WordMatch_Combined(ProcedureInfo source, string key)
        {
            int LabelMatches = WordMatch(source.Label, key);
            return LabelMatches + WordMatch_Tag(source, key);
        }

        public static int WordMatch_Tag(ProcedureInfo source, string key)
        {
            int matches = 0;
            List<string> usedWords = new List<string>();
            foreach (var tag in source.Tags)
                matches += WordMatch(tag, key, ref usedWords);
            return matches;
        }

        public static int WordMatch(string source, string key)
        {
            List<string> discard = new List<string>();
            return WordMatch(source, key, ref discard);
        }

        public static int WordMatch(string source, string key, ref List<string> usedWord)
        {
            //Source ex: BigPineTree
            //Key ex: Forest Tree

            if (usedWord == null)
                usedWord = new List<string>();

            int wordMatches = 0;

            string[] sourceSplitted = source.Split(' ');
            string[] keySplitted = key.Split(' ');

            foreach (var sourceSplit in sourceSplitted)
            {
                if (sourceSplit == "") continue;

                foreach (var keySplit in keySplitted)
                {
                    if (sourceSplit.ToUpper() == keySplit.ToUpper() && !usedWord.Contains(sourceSplit.ToUpper()))
                    {
                        usedWord.Add(keySplit.ToUpper());
                        wordMatches++;
                    }
                }
            }
            return wordMatches;
        }



        public static int CharMatch_Combined(ProcedureInfo source, string key)
        {
            int LabelMatches = CharMatch(source.Label, key);
            return LabelMatches + CharMatch_Tag(source, key);
        }

        public static int CharMatch_Label(ProcedureInfo source, string key)
        {
            var LabelMatch = CharMatch(source.Label, key);
            return LabelMatch;
        }
        public static int CharMatch_Tag(ProcedureInfo source, string key)
        {
            int matches = 0;
            foreach (var tag in source.Tags)
                matches += CharMatch(tag, key);
            return matches;
        }



        public static int CharMatch(string source, string key)
        {
            int maxMatches = 0, sequenceMatches = 0, i = 0;

            var charactersSource = source.ToCharArray();
            var charactersKey = key.ToCharArray();

            while (i < charactersSource.Length)
            {
                if (sequenceMatches < charactersKey.Length &&
                    sequenceMatches + i < charactersSource.Length
                    && charactersKey[sequenceMatches].Upper() == charactersSource[i + sequenceMatches].Upper())
                    sequenceMatches++;
                else
                {
                    maxMatches = sequenceMatches > maxMatches ? sequenceMatches : maxMatches;
                    sequenceMatches = 0;
                    i++;
                }
            }

            maxMatches = sequenceMatches > maxMatches ? sequenceMatches : maxMatches;

            ////Ignore single character matchs (in case of a multi char word)
            //if (ignoreSingleMatch && key.Length > 1 && maxMatches < 2)
            //    maxMatches = 0;
            return maxMatches;
        }
    }
}
