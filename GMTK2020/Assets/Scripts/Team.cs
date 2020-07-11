using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour{
    public GameColor MyTeamType;
    //This array should only ever just be a size of two
    public Paddle[] Paddles;
    public int Score;

    public void ProcessMoveInputForPaddle(Vector2 moveInput, int paddleIndex) {
        var paddleToMove = Paddles[paddleIndex];
        paddleToMove.ProcessMoveInput(moveInput);
    }
}
