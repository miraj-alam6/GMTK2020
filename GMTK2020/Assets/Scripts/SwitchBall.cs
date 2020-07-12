using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBall : Ball {
    public int MaxHealth = 5;
    public int Health;
    public bool JustGotHit;
    public float CooldownOnDamageInflict = 0.1f;
    private float CooldownOnDamageInflictLeft;
    public float SwitchBallDeathAnimationTime =1f;
    public float SwitchBallWhenToSwitchTime = 1f;
    public float SwitchBallWhenToSwitchTimeLeft = 0f;
    private Paddle WhoBrokeTheBall;
    //This field might not be used based on the design
    public TeamColor MyColor;
    private void Awake() {
        base.Awake();
        Health = MaxHealth;
        CooldownOnDamageInflictLeft = 0f;
        SwitchBallWhenToSwitchTimeLeft = 0f;
    }


    public void InflictDamage(Paddle damageInflicter) {
        if (Health > 0 && CooldownOnDamageInflictLeft <= 0) {
            Health--;
            CooldownOnDamageInflictLeft = CooldownOnDamageInflict;
            if (Health <= 0) {
                //uncomment following out
                Explode();
                SwitchBallWhenToSwitchTimeLeft = SwitchBallWhenToSwitchTime;
                WhoBrokeTheBall = damageInflicter;
                //comment following out TODO: don't call explode here, call it later in update
                //The two methods exist just so we can test either case with iteration if necessary
                //ExplodeAndChangeTwoPaddles(damageInflicter);
                //ExplodeAndChangeOnePaddle(1, damageInflicter);
            }
            else {
                AnimJustHitCooldownLeft = AnimJustHitCooldown;
            }
        }

    }

    public void ChangeTwoPaddles(Paddle damageInflicter) {
        var otherTeams = GameController.Instance.GetAllTeamsExceptTarget(damageInflicter);
        var damageInflictorTeam = GameController.Instance.GetSpecificTeam(damageInflicter);
        List<Paddle> allPaddlesOnTeamOfBallBreaker = new List<Paddle>();
        List<Paddle> paddlesOnOtherTeams = new List<Paddle>();
        for (int i=0; i < damageInflictorTeam.Paddles.Length;i++) {
            allPaddlesOnTeamOfBallBreaker.Add(damageInflictorTeam.Paddles[i]);
        }

        for (int i=0; i < otherTeams.Length; i++) {
            var otherTeam = otherTeams[i];
            for (int j=0; j < otherTeam.Paddles.Length; j++) {
                paddlesOnOtherTeams.Add(otherTeam.Paddles[j]);
            }
        }

        //Do all the swapping, and with each swap, remove whatever was on the other side of the swap from the list of
        //things you are allowed to swap with
        for (int i=0; i < allPaddlesOnTeamOfBallBreaker.Count; i++) {
            var paddleToSwap = allPaddlesOnTeamOfBallBreaker[i];
            Paddle otherPaddleSwappedInThisIteration = null;
            if (paddlesOnOtherTeams.Count > 0) {
                SwapPaddle(paddleToSwap, paddlesOnOtherTeams, out otherPaddleSwappedInThisIteration);
            }
            if (otherPaddleSwappedInThisIteration != null) {
                paddlesOnOtherTeams.Remove(otherPaddleSwappedInThisIteration);
            }
        }


    }

    public void ExplodeAndChangeOnePaddle(Paddle damageInflicter) {
        Explode();
        _MySpriteRenderer.enabled = false;
        var otherTeams = GameController.Instance.GetAllTeamsExceptTarget(damageInflicter);
    }

    public void SwapPaddle(Paddle paddle, List<Paddle> validPaddlesToSwapWith, out Paddle otherPaddleInTheSwap) {
        if (validPaddlesToSwapWith.Count > 0) {
            otherPaddleInTheSwap = validPaddlesToSwapWith[Random.Range(0, validPaddlesToSwapWith.Count)];

            //Absolutely essential that we swap team reference before swapping colors because of GetTeam works
            //Swap team's references to the paddles
            var paddleTeam = GameController.Instance.GetSpecificTeam(paddle);
            var otherPaddleTeam = GameController.Instance.GetSpecificTeam(otherPaddleInTheSwap);
            int indexInPaddleTeam = paddleTeam.GetIndexInTeam(paddle);
            int indexInOtherPaddleTeam = otherPaddleTeam.GetIndexInTeam(otherPaddleInTheSwap);
            if (indexInPaddleTeam < 0 || indexInOtherPaddleTeam < 0) {
                Debug.LogError("Swapping won't work because a paddle index in a team was not able to be found");
                return;
            }
            var tempPaddleReference = paddle;
            paddleTeam.Paddles[indexInPaddleTeam] = otherPaddleTeam.Paddles[indexInOtherPaddleTeam];
            otherPaddleTeam.Paddles[indexInOtherPaddleTeam] = tempPaddleReference;

            //swap statemachines
            if(paddleTeam.MyTeamType == TeamColor.Blue || paddleTeam.MyTeamType == TeamColor.Red)
            {
                //swapping prob here
                //paddle.mySM
            }

            //Swap colors.
            var paddleColor = paddle.GetTeamColor();
            paddle.ChangeTeamColor(otherPaddleInTheSwap.GetTeamColor());
            otherPaddleInTheSwap.ChangeTeamColor(paddleColor);

            paddle.SetIndexInTeam(indexInOtherPaddleTeam);
            otherPaddleInTheSwap.SetIndexInTeam(indexInPaddleTeam);
        }
        else {
            otherPaddleInTheSwap = null;
        }

    }

    private void Explode() {
        AnimExploded = true;
        Die();
        Health = MaxHealth;
       //TODO Particle effects
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    protected override void Update() {
        base.Update();
        if (CooldownOnDamageInflictLeft > 0) {
            CooldownOnDamageInflictLeft -= Time.deltaTime;
        }
        if (SwitchBallWhenToSwitchTimeLeft > 0) {
            SwitchBallWhenToSwitchTimeLeft -= Time.deltaTime;
            if (SwitchBallWhenToSwitchTimeLeft <= 0) {
                ChangeTwoPaddles(WhoBrokeTheBall);
            }
        }

    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);
        if (collision.collider.tag.Equals(Constants.PADDLE_TAG)) {
            var whoHitMe = collision.collider.GetComponent<Paddle>();
            InflictDamage(whoHitMe);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        base.Update();
        if (collision.tag.Equals(Constants.GOAL_TAG)) {
            Goal goalComponent = collision.GetComponent<Goal>();
            if (goalComponent != null) {
                var teamOfGoal = GameController.Instance.GetSpecificTeam(goalComponent.MyColor);
                teamOfGoal.RemoveAPoint();
                Die();
            }
        }
    }
}
