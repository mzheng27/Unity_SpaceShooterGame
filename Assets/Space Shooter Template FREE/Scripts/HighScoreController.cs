using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreController : Singleton<HighScoreController>
{
    public long[] highScore = new long[10];

    public bool IsHighScore(long value)
    {
         return (value > highScore[0]) ;
    }
    public bool AddHighScore(long value)
    {
        if (!IsHighScore(value))
        {
            return false;
        }
        else
        {
            for (int i = 9; i > 0; i--)
            {
                highScore[i] = highScore[i - 1];
            }
            highScore[0] = value;
            return true;
        }
        
    }
}
