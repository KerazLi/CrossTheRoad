using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public List<ScoreRecord> scoreRecords;
    private List<int> scoreList;

    private void OnEnable()
    {
        scoreList = GameManager.instance.scoreList;
    }

    private void Start()
    {
        SetLeaderboardData();
    }

    private void SetLeaderboardData()
    {
        for (int i = 0; i < scoreRecords.Count; i++)
        {
            if (i<scoreList.Count)
            {
                scoreRecords[i].SetScoreText(scoreList[i]);
                scoreRecords[i].gameObject.SetActive(true);
            }
            else
            {
                scoreRecords[i].gameObject.SetActive(false);
            }
        }
    }
}
