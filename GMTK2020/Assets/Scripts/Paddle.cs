using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paddle : MonoBehaviour{
    [SerializeField]
    private TeamColor _MyTeam;
    public float MovementSpeed;
    private SpriteRenderer _MySpriteRenderer;

    private void Awake() {
        _MySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _MySpriteRenderer.color = StaticFunctions.GetUnityColor(_MyTeam);
    }
    public void ProcessMoveInput(Vector2 inputVector) {
        Debug.Log("If movement was implemented, this would be the input vector:"+inputVector);
    }

    public TeamColor GetTeamColor() {
        return _MyTeam;
    }

    public void ChangeTeamColor(TeamColor teamColor) {
        _MyTeam = teamColor;

        Color unityColor = StaticFunctions.GetUnityColor(teamColor);
        _MySpriteRenderer.color = unityColor;

        //TODO need more juice for update. 

    }

}
