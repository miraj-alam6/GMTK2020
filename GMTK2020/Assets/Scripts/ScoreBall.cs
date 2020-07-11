using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBall : Ball
{
    private TeamColor _MyColor;

    public void ChangeColor(Paddle paddleThatHitMe) {
        _MyColor = paddleThatHitMe.GetTeamColor();
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

    protected override void OnTriggerEnter2D(Collider2D collision){
        base.Update();
        if (collision.tag.Equals(Constants.GOAL_TAG)) {
            Goal goalComponent = collision.GetComponent<Goal>();
            if (goalComponent!= null && goalComponent.MyColor != _MyColor) {
                var teamOfBall = GameController.Instance.GetSpecificTeam(_MyColor);
                if (goalComponent.MyColor == _MyColor) {
                    teamOfBall.RemoveAPoint();
                }
                else {
                    var otherTeams = GameController.Instance.GetAllTeamsExceptTarget(_MyColor);
                    teamOfBall.AddAPoint(otherTeams);
                }
            }
        }
    }
}
