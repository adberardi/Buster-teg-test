using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardEntry : MonoBehaviour
{

    public string userId;
    public int score = 0;

    public LeaderBoardEntry(string userId, int score)
    {
        this.userId = userId;
        this.score = score;
    }

    public Dictionary<string, System.Object> ListScore()
        {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["userId"] = userId;
        result["score"] = score;
        return result;
        }
}
