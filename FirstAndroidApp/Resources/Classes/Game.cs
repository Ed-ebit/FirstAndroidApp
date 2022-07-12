using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using System.Collections.Generic;
using Android.Content;

namespace FirstAndroidApp
{
    public class Game
    {

        public int Count { get; }
        public string LottoNumbers { get; private set; }

        private readonly byte[] GuteZahlen = { 45, 13, 28, 21, 20, 8, 12, 15, 46, 14, 30, 34, 37, 23, 40, 29, 35, 10, 17, 39, 44, 5, 24, 48 };
        private readonly byte[] NormaleZahlen = { 1, 2, 3, 4, 6, 7, 9, 11, 15, 16, 17, 18, 19, 22, 25, 26, 27, 31, 32, 33, 35, 36, 38, 41, 42, 43, 47, 49 };

        public Game(int count)
        {
            Count = count;
            LottoNumbers = CreateLottoNumbers(GuteZahlen, NormaleZahlen);
        }

        private string CreateLottoNumbers(byte[] guteZahlen, byte[] normaleZahlen)
        {
            const byte goodNumberCount = 4;
            const byte normalNumberCount = 2;
            HashSet<byte> goodNumbers = NumberSelector(guteZahlen, goodNumberCount);
            HashSet<byte> normalNumbers = NumberSelector(normaleZahlen, normalNumberCount);
            HashSet<byte> combinedNumbers = goodNumbers;
            combinedNumbers.UnionWith(normalNumbers);

            return string.Join(" | ", combinedNumbers);
        }
        private HashSet<byte> NumberSelector(byte[] array, byte desiredNumbers)
        {
            HashSet<byte> selectedNumbers = new HashSet<byte>();
            bool numberAdded = false;
            Random randomNumber = new Random();

            for (byte i = 0; i < desiredNumbers; i++)
            {
                do
                {
                    int randomIndex = randomNumber.Next(0, (array.Length - 1));
                    numberAdded = selectedNumbers.Add(array[randomIndex]);
                }
                while (numberAdded == false);
            }
            return selectedNumbers;
        }


        public static List<Game> InitializeGames(int gamesNumber)
        {
            List<Game> games = new List<Game>();
            for (int i = 1; i <= gamesNumber; i++)
            {
                games.Add(new Game(i));
            }
            return games;
        }
    }
}