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

    public Dictionary<string, object> ListScore()
        {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["userId"] = userId;
        result["score"] = score;
        return result;
        }
}
