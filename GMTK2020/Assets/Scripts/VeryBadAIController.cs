using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeryBadAIController : MonoBehaviour{
    public float MaxTimeBetweenRandomizers = 0.5f;
    public float MinTimeBetweenRandomizers = 2f;
    public float TimeLeftToReRandomize;
    public Vector2[] InputToFeed;
    public Team MyTeam;
    // Start is called before the first frame update
    void Awake()
    {       
        InputToFeed = new Vector2[MyTeam.Paddles.Length];

    }

    // Update is called once per frame
    void Update()
    {
        if (TimeLeftToReRandomize > 0f) {
            TimeLeftToReRandomize -= Time.deltaTime;
            for (int i=0; i < InputToFeed.Length; i++) {
                MyTeam.ProcessMoveInputForPaddle(InputToFeed[i], i);
            }
        }
        else {
            TimeLeftToReRandomize = Random.Range(MinTimeBetweenRandomizers, MaxTimeBetweenRandomizers);
            for (int i = 0; i < InputToFeed.Length; i++) {
                InputToFeed[i] = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                MyTeam.ProcessMoveInputForPaddle(InputToFeed[i], i);
            }
        }
    }
}
