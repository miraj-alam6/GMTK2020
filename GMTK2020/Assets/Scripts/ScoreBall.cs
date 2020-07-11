using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBall : Ball
{
    private GameColor _MyColor;

    public void ChangeColor(Paddle paddleThatHitMe) {
        _MyColor = paddleThatHitMe.MyTeam;
        Debug.Log("Just changed my color BRAH");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }
}
