using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paddle : MonoBehaviour{
    public GameColor MyTeam;
    public float MovementSpeed;
    public void ProcessMoveInput(Vector2 inputVector) {
        Debug.Log("If movement was implemented, this would be the input vector:"+inputVector);
    }
}
