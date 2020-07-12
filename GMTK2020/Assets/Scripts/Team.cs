using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour{
    public TeamColor MyTeamType;
    //This array should only ever just be a size of two
    public Paddle[] Paddles;
    public int Score;

    public void AddAPoint(Team[] otherTeams) {
        int highestScoreInOtherTeams = 0;
        for (int i=0; i < otherTeams.Length; i++) {
            if (otherTeams[i].Score > highestScoreInOtherTeams) {
                highestScoreInOtherTeams = otherTeams[i].Score;
            }
        }
        Score++;

        GameUI.Instance.UpdateScore(Score, MyTeamType);

        //A tie probably can't even happen anymore because only one score added at a time.
        if (Score >= Constants.SCORE_NEEDED_TO_WIN && Score >highestScoreInOtherTeams) {
            //TODO: Make this team win and end the game
            GameController.Instance.EndGame(this);
        }
    }

    public void RemoveAPoint() {
        Score--;
        if (Score < 0) {
            Score = 0;
        }
        GameUI.Instance.UpdateScore(Score, MyTeamType);

        //TODO update UI
    }

    public void ProcessMoveInputForPaddle(Vector2 moveInput, int paddleIndex) {
        var paddleToMove = Paddles[paddleIndex];
        paddleToMove.ProcessMoveInput(moveInput);
    }

    public int GetIndexInTeam(Paddle paddle) {
        Debug.Log("Getting index for" +paddle.name +" in " + MyTeamType.ToString() + " team");
        for (int i=0; i< Paddles.Length; i++) {
            if (Paddles[i] == paddle) {
                return i;
            }
        }
        return -1;
    }
}
