﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{

    public static GameUI Instance;

    //Green
    public TMP_Text Team1Label;
    public TMP_Text Team1Score;

    //Blue
    public TMP_Text Team2Label;
    public TMP_Text Team2Score;
    
    //Red
    public TMP_Text Team3Label;
    public TMP_Text Team3Score;

    private void Awake() {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void UpdateScore(int newScore, TeamColor team) {
        if(team == TeamColor.Green)
        {
            Team1Score.text = newScore.ToString();
        }
        else if (team == TeamColor.Blue)
        {
            Team2Score.text = newScore.ToString();
        }
        else if (team == TeamColor.Red)
        {
            Team3Score.text = newScore.ToString();
        }
    }
}
