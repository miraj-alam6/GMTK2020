using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paddle : MonoBehaviour{
    [SerializeField]
    private TeamColor _MyTeam;
    private SpriteRenderer _MySpriteRenderer;
    public float Speed;
    private Rigidbody2D _RB2D;
    private void Awake() {
        _MySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _MySpriteRenderer.color = StaticFunctions.GetUnityColor(_MyTeam);
        _RB2D = GetComponent<Rigidbody2D>();
    }
    public void ProcessMoveInput(Vector2 inputVector) {
        Debug.Log("If movement was implemented, this would be the input vector:"+inputVector);
        _RB2D.velocity = Speed * inputVector;
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
