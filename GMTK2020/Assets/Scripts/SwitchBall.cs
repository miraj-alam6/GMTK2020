using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBall : Ball {
    public int HitsLeft;
    public float CooldownOnDamageInflict = 0.1f;
    private float CooldownOnDamageInflictLeft;
    //This field might not be used based on the design
    public GameColor MyColor;
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
    }

    public void ExplodeAndChangeOnePaddle(Paddle damageInflicter) {
        Explode();
        var otherTeams = GameController.Instance.GetAllTeamsExceptTarget(damageInflicter);
    }

    private void Explode() {
        Die();
       //Particle effects
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
