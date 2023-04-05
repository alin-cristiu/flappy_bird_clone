using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static int score;
    public static int highestScore;

    public static void SaveScore()
    {
        if (score > highestScore)
        {
            highestScore = score;
            PlayerPrefs.SetInt("HighestScore", highestScore);
        }
    }

    public static void LoadHighscore()
    {
        highestScore = PlayerPrefs.GetInt("HighestScore");
    }
}
