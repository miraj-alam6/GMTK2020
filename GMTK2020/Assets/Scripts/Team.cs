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
        //TODO update UI
        //A tie probably can't even happen anymore because only one score added at a time.
        if (Score >= Constants.SCORE_NEEDED_TO_WIN && Score >highestScoreInOtherTeams) {
            //TODO: Make this team win and end the game
        }
    }

    public void RemoveAPoint() {
        Score--;
        if (Score < 0) {
            Score = 0;
        }
        //TODO update UI
    }

    public void ProcessMoveInputForPaddle(Vector2 moveInput, int paddleIndex) {
        var paddleToMove = Paddles[paddleIndex];
        paddleToMove.ProcessMoveInput(moveInput);
    }

    public int GetIndexInTeam(Paddle paddle) {
        for (int i=0; i< Paddles.Length; i++) {
            if (Paddles[i] == paddle) {
                return i;
            }
        }
        return -1;
    }
}
