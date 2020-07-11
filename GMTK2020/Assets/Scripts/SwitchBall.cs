using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBall : Ball {
    public int HitsLeft;
    public float CooldownOnDamageInflict = 0.1f;
    private float CooldownOnDamageInflictLeft;
    //This field might not be used based on the design
    public TeamColor MyColor;
    private void Awake() {
        CooldownOnDamageInflictLeft = 0f;
    }


    public void InflictDamage(Paddle damageInflicter) {
        if (CooldownOnDamageInflictLeft <= 0) {
            HitsLeft--;
            CooldownOnDamageInflictLeft = CooldownOnDamageInflict;
            if (HitsLeft == 0) {
                //The two methods exist just so we can test either case with iteration if necessary
                ExplodeAndChangeTwoPaddles(damageInflicter);
                //ExplodeAndChangeOnePaddle(1, damageInflicter);
            }
        }

    }

    public void ExplodeAndChangeTwoPaddles(Paddle damageInflicter) {
        Explode();
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
            int indexInOtherPaddleTeam = paddleTeam.GetIndexInTeam(paddle);
            if (indexInPaddleTeam < 0 || indexInOtherPaddleTeam < 0) {
                Debug.LogError("Swapping won't work because a paddle index in a team was not able to be found");
                return;
            }
            var tempPaddleReference = paddle;
            paddleTeam.Paddles[indexInPaddleTeam] = otherPaddleTeam.Paddles[indexInOtherPaddleTeam];
            otherPaddleTeam.Paddles[indexInOtherPaddleTeam] = tempPaddleReference;

            //Swap colors.
            var paddleColor = paddle.GetTeamColor();
            paddle.ChangeTeamColor(otherPaddleInTheSwap.GetTeamColor());
            otherPaddleInTheSwap.ChangeTeamColor(paddleColor);            

        }
        else {
            otherPaddleInTheSwap = null;
        }

    }

    private void Explode() {
        Die();
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
            CooldownOnDamageInflict -= Time.deltaTime;
        }

    }
}
