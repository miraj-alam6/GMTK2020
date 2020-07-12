using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float PaddleWaitTime = 1.5f;

    public Team BlueTeam;
    public Team RedTeam;

    public List<PaddleStateMachine> BlueSMs;
    public List<PaddleStateMachine> RedSMs;

    public static AIController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for(int i=0; i < BlueTeam.Paddles.Length; i++)
        {
            Paddle paddle = BlueTeam.Paddles[i];
            Debug.Log(paddle);
            BlueSMs[i].SwapPaddle(paddle);
            BlueSMs[i].WaitTime = PaddleWaitTime;
            paddle.mySM = BlueSMs[i];
        }

        for (int i = 0; i < RedTeam.Paddles.Length; i++)
        {
            Paddle paddle = RedTeam.Paddles[i];
            Debug.Log(paddle);
            RedSMs[i].SwapPaddle(paddle);
            RedSMs[i].WaitTime = PaddleWaitTime;
            paddle.mySM = RedSMs[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
