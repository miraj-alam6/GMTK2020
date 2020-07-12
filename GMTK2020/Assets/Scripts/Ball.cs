using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float TimeBufferToSpawnAgain = 2f;
    public float TimeUntilSpawnAgainLeft;
    private bool _MomentarilyDead = false;
    public float MaxSpeed = 10f;
    public bool StartDead = false;

    private Rigidbody2D _RB2D;
    public Rigidbody2D RB2D { get { return _RB2D; } }
    protected SpriteRenderer _MySpriteRenderer;

    public bool MomentarilyDead { get { return _MomentarilyDead; } }

    protected virtual void Awake() {
        _MySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _RB2D = GetComponentInChildren<Rigidbody2D>();
        if (StartDead) {
            CompleteDie();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    protected void Die() {
        //Trigger polish stuff here        
        CompleteDie();
    }

    //Called directly at beginning of game, don't want to call normal Die because that will trigger polish effects
    private void CompleteDie() {
        _MomentarilyDead = true;
        TimeUntilSpawnAgainLeft = TimeBufferToSpawnAgain;
        //        _MySpriteRenderer.enabled = false;
        _RB2D.isKinematic = true;
        _RB2D.velocity = Vector3.zero;
        transform.position = new Vector3(100, 100, -100);
    }

    // Update is called once per frame
    protected virtual void Update() {        
        if (TimeUntilSpawnAgainLeft > 0) {
            TimeUntilSpawnAgainLeft -= Time.deltaTime;
            if (TimeUntilSpawnAgainLeft <= 0) {
                StartSpawnOntoLevelAgain();
            }           
        }
        //Debug.Log(name);
        if (_RB2D.velocity.sqrMagnitude > MaxSpeed) {
            _RB2D.velocity = ((_RB2D.velocity.normalized) * MaxSpeed);
        }
    }

    private void StartSpawnOntoLevelAgain() {
        _MomentarilyDead = false;
        if (this is ScoreBall) {
            GameController.Instance.SpawnAScoreBall((ScoreBall)this);
        }
        else if (this is SwitchBall) {
            GameController.Instance.SpawnASwitchBall((SwitchBall)this);
        }
        else {
            GameController.Instance.SpawnABall(this);
        }

        //TODO Visuals need to turn on, need to probably get spawn locations, maybe from a map class?
    }


    public void CompleteSpawnOntoLevelAgain(Vector3 exactPosition, Vector3 force) {
        transform.position = exactPosition;
        _RB2D.isKinematic = false;
        _RB2D.AddForce(force, ForceMode2D.Impulse);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals(Constants.GOAL_TAG)) {
            Debug.Log(this.name + " just collided with a goal.");
        }
    }
}
