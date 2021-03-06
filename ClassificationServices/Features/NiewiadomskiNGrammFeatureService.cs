﻿using System;
using System.Collections.Generic;

namespace ClassificationServices
{
    public class NiewiadomskiNGrammFeatureService : IFeatureService
    {
        public double Call(List<string> keyWords, List<string> stemmedWords)
        {
            double result = 0.0;
            foreach (string keyword in keyWords)
            {
                foreach (string word in stemmedWords)
                {
                    result += NiewiadomskiSimilarityFunction(keyword, word);
                }
            }

            return result;
        }

        private double NiewiadomskiSimilarityFunction(string firstWord, string secondWord)
        {
            int firstWordLetterCount = firstWord.Length;
            int secondWordLetterCount = secondWord.Length;
            double maximum = firstWordLetterCount >= secondWordLetterCount ? firstWordLetterCount : secondWordLetterCount;
            if (firstWordLetterCount < secondWordLetterCount)
            {
                string temp = firstWord;
                firstWord = secondWord;
                secondWord = temp;
            }
            double fractional = 2.0 / (Math.Pow(maximum, 2) + maximum);
            double result = 0.0;
            for (int i = 0; i < firstWordLetterCount; i++)
            {
                for (int j = 0; j < firstWordLetterCount - i; j++)
                {
                    if (secondWord.Contains(firstWord.Substring(j, i)))
                    {
                        result++;
                    }
                }
            }
            return fractional * result;
        }

        public override string ToString()
        {
            return "Niewiadomski N Gramm";
        }
    }
}
