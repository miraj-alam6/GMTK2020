using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paddle : MonoBehaviour{
    [SerializeField]
    private TeamColor _MyTeam;
    private SpriteRenderer _MySpriteRenderer;
    public float Speed;
    public float ForceMagnitude = 10f;
    public bool UseFixedAccelaration;
    public float FixedAcceleration = 10f;
    private Rigidbody2D _RB2D;
    private int IndexInTeam;
    public SpriteRenderer[] DirectionIcons;
    public bool SimpleMovement;
    public PaddleStateMachine mySM;
    private void Awake() {
        _MySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _MySpriteRenderer.color = StaticFunctions.GetUnityColor(_MyTeam);
        _RB2D = GetComponent<Rigidbody2D>();
    }
    public void ProcessMoveInput(Vector2 inputVector) {
        if (SimpleMovement) {
            _RB2D.velocity = Speed * inputVector;
        }
        else {
            float currentSpeed = _RB2D.velocity.sqrMagnitude;
            float predictedSpeedNextFrame = (_RB2D.velocity + (inputVector * ForceMagnitude * Time.fixedDeltaTime)/_RB2D.mass).sqrMagnitude;
            float forceToUse = (UseFixedAccelaration) ? FixedAcceleration * _RB2D.mass : ForceMagnitude;
            if (predictedSpeedNextFrame < (Speed*Speed)) {
                _RB2D.AddForce(forceToUse * inputVector, ForceMode2D.Force);
            }
        }
    }

    public void SetIndexInTeam(int index) {
        for (int i=0; i < DirectionIcons.Length; i++) {
            if (i == index) {
                DirectionIcons[i].enabled = true;
            }
            else {
                DirectionIcons[i].enabled = false;
            }

        }
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
