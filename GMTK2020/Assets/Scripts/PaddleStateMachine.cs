using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PaddleStateMachine : MonoBehaviour {


    public Team MyTeam;
    public Paddle MyPaddle;
    public int PaddleIndex;

    [HideInInspector]
    public List<State> StatesList;
    [HideInInspector]
    public Paddle Paddle;
    [HideInInspector]
    public GameObject GO;
    [HideInInspector]
    public Vector2 TargetPosition = Vector2.zero;
    [HideInInspector]
    public float WaitTime;

    public void Initialize(Team team, Paddle paddle) {
        MyTeam = team;
        MyPaddle = paddle;
        //this.Paddle = paddle;
        //this.GO = paddle.gameObject;
        StatesList = new List<State>(3);

        StatesList.Add(new SearchState(this));
        StatesList.Add(new TravelState(this));
        StatesList.Add(new WaitState(this));

        Current = StatesList[2];
        Current.Enter(MyTeam, MyPaddle);
    }

    public void SwapPaddle(Paddle newPaddle)
    {
        if (Paddle.GetTeamColor() == TeamColor.Green)
        {
            Paddle.mySM = null;
        }

        this.Paddle = newPaddle;
        this.GO = newPaddle.gameObject;
        this.Current = StatesList[2];
    }

    // Update is called once per frame
    void Update()
    {
        Current?.Execute(MyTeam, MyPaddle);
    }

    private State Current;
    public void ChangeState(System.Type t)
    {
        for(int i=0; i < StatesList.Count; i++)
        {
            if (t.Equals(StatesList[i]))
            {
                Current.Exit(MyTeam, MyPaddle);
                Current = StatesList[i];
                Current.Enter(MyTeam, MyPaddle);
            }
        }
    }

    public abstract class State {

        public PaddleStateMachine SM;

        public virtual void Enter(Team myTeam, Paddle myPaddle) { }
        public virtual void Execute(Team myTeam, Paddle myPaddle) { }
        public virtual void Exit(Team myTeam, Paddle myPaddle) { }
    };


    public class SearchState : State {

        public SearchState(PaddleStateMachine sm)
        {
            this.SM = sm;
        }
        
        public override void Execute(Team myTeam, Paddle myPaddle)
        {
            //Get cloest ball
            Collider2D[] colliders = Physics2D.OverlapCircleAll(myPaddle.gameObject.transform.position, 8f, LayerMask.NameToLayer("Ball"));
            colliders = colliders.Where(c => c.GetComponent<Ball>() != null) as Collider2D[];
            if(colliders.Length > 0)
            {
                Collider2D closest = colliders[0];
                float distance = Vector2.Distance(this.SM.GO.transform.position, closest.transform.position);
                for(int i=0; i < colliders.Length; i++)
                {
                    float d = Vector2.Distance(this.SM.GO.transform.position, colliders[i].transform.position);
                    if (d < distance)
                    {
                        distance = d;
                        closest = colliders[i];
                    }
                }

                //Find trajectory of the ball and get a target position based off this
                Rigidbody2D rb = closest.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    RaycastHit2D hit = Physics2D.Raycast(rb.transform.position, rb.velocity.normalized, 15f, LayerMask.NameToLayer("Goal") | LayerMask.NameToLayer("Wall"));
                    if (hit.collider != null)
                    {
                        this.SM.TargetPosition = hit.point;
                    }
                }

                if (this.SM.TargetPosition != Vector2.zero)
                {
                    this.SM.ChangeState(typeof(TravelState));
                }
            }
        }
    }

    public class TravelState : State {

        public Rigidbody2D myRB;

        public TravelState(PaddleStateMachine sm)
        {
            this.SM = sm;
        }

        public override void Enter(Team myTeam, Paddle myPaddle)
        {
            myRB = myPaddle.gameObject.GetComponent<Rigidbody2D>();
            //myRB = this.SM.GO.GetComponent<Rigidbody2D>();
            //myRB.velocity = (this.SM.TargetPosition - (Vector2)this.SM.GO.transform.position);
            var velocity = (this.SM.TargetPosition - (Vector2)this.SM.GO.transform.position);
            myPaddle.ProcessMoveInput(velocity.normalized);
        }

        public override void Execute(Team myTeam, Paddle myPaddle)
        {
            Collider2D[] results = new Collider2D[1];
            if(myRB.OverlapCollider(new ContactFilter2D(), results) > 0 
                || Vector2.Distance((Vector2)myRB.transform.position, this.SM.TargetPosition) < 1.0f)
            {
                this.SM.ChangeState(typeof(WaitState));
            }
        }
    }

    public class WaitState : State {

        public Rigidbody2D myRB;
        public float StartTime;

        public WaitState(PaddleStateMachine sm)
        {
            this.SM = sm;
        }

        public override void Enter(Team myTeam, Paddle myPaddle)
        {
            //            myRB = this.SM.GO.GetComponent<Rigidbody2D>();
            //            myRB.velocity = Vector2.zero;
            myPaddle.ProcessMoveInput(Vector2.zero);
            StartTime = Time.deltaTime;
        }

        public override void Execute(Team myTeam, Paddle myPaddle)
        {
            if (Time.deltaTime - StartTime >= this.SM.WaitTime)
            {
                this.SM.ChangeState(typeof(SearchState));
            }
        }

        public override void Exit(Team myTeam, Paddle myPaddle)
        {
            StartTime = -1;
        }
    }
}
