using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paddle : MonoBehaviour{
    [SerializeField]
    private TeamColor _MyTeam;
    public float MovementSpeed;
    public void ProcessMoveInput(Vector2 inputVector) {
        Debug.Log("If movement was implemented, this would be the input vector:"+inputVector);
    }

    public TeamColor GetTeamColor() {
        return _MyTeam;
    }

    public void ChangeTeamColor(TeamColor color) {
        _MyTeam = color;
        //TODO Update visuals to the new color. 
    }

}
