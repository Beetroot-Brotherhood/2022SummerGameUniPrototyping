using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace Krezme {
    public static class QualityOfLife {
        /// <summary>
        /// DeepClone allows the data of Objects to be duplicated without linking the objects
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj) {
            using (MemoryStream stream = new MemoryStream()) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;
    
                return (T) formatter.Deserialize(stream);
            }
        }

        public static GameObject FindGameObjectInChildrenWithTag(Transform parent, string tag) {
            if (parent.childCount > 0) {
                for (int i = parent.childCount-1; i >= 0; i--) {
                    GameObject gO = FindGameObjectInChildrenWithTag(parent.GetChild(i), tag);
                    if (gO != null) {
                        return gO;
                    }
                }
                if (CompareTags(parent.tag, tag)) {
                    return parent.gameObject;
                }
            }
            else {
                if (CompareTags(parent.tag, tag)) {
                    return parent.gameObject;
                }
            }
            return null;
        }

        public static bool CompareTags(string firstTag, string secondTag) {
            return firstTag == secondTag ? true : false;
        }

        //function for UI sprite to look at the player
        public static void LookAtPlayer(Transform spriteTransform, Transform playerTransform) {
            spriteTransform.LookAt(playerTransform);
        }

        /// <summary>
        /// Generates all possible random numbers without repeating 
        /// </summary>
        /// <param name="min">The minimum generated number (including)</param>
        /// <param name="max">The maximum generated number (including)</param>
        /// <param name="amount">Amount of numbers you want to generate. Entering a greater amount than the numbers between "min" and "max" will not repeat numbers.</param>
        /// <returns>Integer array of the random numbers without repetition</returns>
        public static int[] RandomNumberArrayWithoutRepeating(int min, int max) {
            List<int> numbers = new List<int>();
            for (int i = min; i <= max; i++) {
                numbers.Add(i);
            }
            int [] randomNumbers = new int[numbers.Count];
            int amount = numbers.Count;
            for (int i = 0; i < amount; i++) {
                int randomIndex = UnityEngine.Random.Range(0, numbers.Count);
                randomNumbers[i] = numbers[randomIndex];
                numbers.RemoveAt(randomIndex);
            }
            return randomNumbers;
        }

        /// <summary>
        /// Generate an amount of random numbers without repeating
        /// </summary>
        /// <param name="min">The minimum generated number (including)</param>
        /// <param name="max">The maximum generated number (including)</param>
        /// <param name="amount">Amount of numbers you want to generate. Entering a greater amount than the numbers between "min" and "max" will not repeat numbers.</param>
        /// <returns>Integer array of the random numbers without repetition</returns>
        public static int[] RandomNumberArrayWithoutRepeating(int min, int max, int amount) {
            int [] randomNumbers = new int[amount];
            List<int> numbers = new List<int>();
            for (int i = min; i <= max; i++) {
                numbers.Add(i);
            }
            if (numbers.Count < amount) {
                amount = numbers.Count;
            }
            for (int i = 0; i < amount; i++) {
                int randomIndex = UnityEngine.Random.Range(0, numbers.Count);
                randomNumbers[i] = numbers[randomIndex];
                numbers.RemoveAt(randomIndex);
            }
            return randomNumbers;
        }

        /// <summary>
        /// Generates all possible random numbers without repeating 
        /// </summary>
        /// <param name="min">The minimum generated number (including)</param>
        /// <param name="max">The maximum generated number (including)</param>
        /// <param name="amount">Amount of numbers you want to generate. Entering a greater amount than the numbers between "min" and "max" will not repeat numbers.</param>
        /// <returns>Integer list of the random numbers without repetition</returns>
        public static List<int> RandomNumberListWithoutRepeating(int min, int max) {
            List<int> randomNumbers = new List<int>();
            List<int> numbers = new List<int>();
            for (int i = min; i <= max; i++) {
                numbers.Add(i);
            }
            int amount = numbers.Count;
            for (int i = 0; i < amount; i++) {
                int randomIndex = UnityEngine.Random.Range(0, numbers.Count);
                randomNumbers.Add(numbers[randomIndex]);
                numbers.RemoveAt(randomIndex);
            }
            return randomNumbers;
        }

        /// <summary>
        /// Generate an amount of random numbers without repeating
        /// </summary>
        /// <param name="min">The minimum generated number (including)</param>
        /// <param name="max">The maximum generated number (including)</param>
        /// <param name="amount">Amount of numbers you want to generate. Entering a greater amount than the numbers between "min" and "max" will not repeat numbers.</param>
        /// <returns>Integer list of the random numbers without repetition</returns>
        public static List<int> RandomNumberListWithoutRepeating(int min, int max, int amount) {
            List<int> randomNumbers = new List<int>();
            List<int> numbers = new List<int>();
            for (int i = min; i <= max; i++) {
                numbers.Add(i);
            }
            if (numbers.Count < amount) {
                amount = numbers.Count;
            }
            for (int i = 0; i < amount; i++) {
                int randomIndex = UnityEngine.Random.Range(0, numbers.Count);
                randomNumbers.Add(numbers[randomIndex]);
                numbers.RemoveAt(randomIndex);
            }
            return randomNumbers;
        }
    }

    public enum Bool3D {
        Null,
        False,
        True
    }
}
