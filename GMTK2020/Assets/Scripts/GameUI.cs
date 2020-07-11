using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{

    public static GameUI Instance;

    public TMP_Text Team1Label;
    public TMP_Text Team1Score;

    public TMP_Text Team2Label;
    public TMP_Text Team2Score;
    
    public TMP_Text Team3Label;
    public TMP_Text Team3Score;

    private void Awake() {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
                
    }
}
