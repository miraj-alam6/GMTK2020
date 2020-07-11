using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float TimeBufferToSpawnAgain = 2f;
    public float TimeUntilSpawnAgainLeft;
    private bool _MomentarilyDead = false;
    public float MaxSpeed = 10f;

    private Rigidbody2D _RB2D;
    protected SpriteRenderer _MySpriteRenderer;

    public bool MomentarilyDead { get { return _MomentarilyDead; } }

    protected virtual void Awake() {
        _MySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _RB2D = GetComponentInChildren<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    protected void Die() {
        _MomentarilyDead = true;
        TimeUntilSpawnAgainLeft = TimeBufferToSpawnAgain;
        //TODO Visuals need to turn off
    }

    // Update is called once per frame
    protected virtual void Update() {        
        if (TimeUntilSpawnAgainLeft > 0) {
            TimeUntilSpawnAgainLeft -= Time.deltaTime;
            if (TimeUntilSpawnAgainLeft <= 0) {
                SpawnOntoLevelAgain();
            }           
        }
        //Debug.Log(name);
        if (_RB2D.velocity.sqrMagnitude > MaxSpeed) {
            _RB2D.velocity = ((_RB2D.velocity.normalized) * MaxSpeed);
        }
    }

    private void SpawnOntoLevelAgain() {
        _MomentarilyDead = false;
        //TODO Visuals need to turn on, need to probably get spawn locations, maybe from a map class?
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals(Constants.GOAL_TAG)) {
            Debug.Log(this.name + " just collided with a goal.");
        }
    }
}
