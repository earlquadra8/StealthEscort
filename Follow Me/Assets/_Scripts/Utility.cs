using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Utility
{
    #region Static method
    public static T[] ShuffleArray<T>( T[] array, int seed )
    {
        System.Random prng = new System.Random (seed);
        for (int i = 0; i < array.Length - 1; i++) {
            int randomIndex = prng.Next (i, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }
        return array;
    }

    public static List<T> ShuffleList<T>( List<T> list, int seed )
    {
        T[] newArry = ShuffleArray<T> (list.ToArray (), seed);
        return new List<T> (newArry);
    }

    public static IEnumerator RunAllActionsCoroutine( List<Action<Action>> actionsToRun, Action onAllFinished )
    {
        foreach (Action<Action> action in actionsToRun) {
            bool isAnActionFinished = false;
            Action onAnActionFinished = () =>
            {
                isAnActionFinished = true;
            };
            action (onAnActionFinished);
            while (!isAnActionFinished) {
                yield return null;
            }
        }

        if (onAllFinished != null) {
            onAllFinished ();
        }
    }
    #endregion    
}
