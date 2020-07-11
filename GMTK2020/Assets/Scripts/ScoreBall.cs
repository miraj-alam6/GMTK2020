using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBall : Ball
{
    public TeamColor StartingColor;
    private TeamColor _MyColor;




    public void ChangeColor(Paddle paddleThatHitMe) {
        if (paddleThatHitMe.GetTeamColor() != _MyColor) {
            _MyColor = paddleThatHitMe.GetTeamColor();
            _MySpriteRenderer.color = StaticFunctions.GetUnityColor(_MyColor);
        }
    }


    protected override void Awake() {
        base.Awake();
        _MyColor = StartingColor;
        _MySpriteRenderer.color = StaticFunctions.GetUnityColor(_MyColor);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    protected void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag.Equals(Constants.PADDLE_TAG)) {
            var whoHitMe = collision.collider.GetComponent<Paddle>();
            ChangeColor(whoHitMe);
        }
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
                Debug.Log("SCORE");
            }
        }
    }
}
