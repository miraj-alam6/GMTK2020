using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public static AIController Instance;

    public float PaddleWaitTime = 1.5f;

    public Team BlueTeam;
    public Team RedTeam;

    public List<PaddleStateMachine> BlueSMs;
    public List<PaddleStateMachine> RedSMs;

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < RedSMs.Count && i < RedTeam.Paddles.Length; i++) {
            //RedSMs[i].MyTeam = RedTeam;
            //RedSMs[i].MyPaddle = RedTeam.Paddles[i];
            RedSMs[i].Initialize(RedTeam, RedTeam.Paddles[i]);
            RedSMs[i].GO = RedTeam.Paddles[i].gameObject;
        }
        for (int i = 0; i < BlueSMs.Count && i < BlueTeam.Paddles.Length; i++) {
            //BlueSMs[i].MyTeam = BlueTeam;
            //BlueSMs[i].MyPaddle = BlueTeam.Paddles[i];
            BlueSMs[i].Initialize(BlueTeam, BlueTeam.Paddles[i]);
            BlueSMs[i].GO = BlueTeam.Paddles[i].gameObject;
        }

        //for(int i=0; i < BlueTeam.Paddles.Length; i++)
        //{
        //    Paddle paddle = BlueTeam.Paddles[i];
        //    BlueSMs[i].SwapPaddle(paddle);
        //    BlueSMs[i].WaitTime = PaddleWaitTime;
        //    paddle.mySM = BlueSMs[i];
        //}

        //for (int i = 0; i < RedTeam.Paddles.Length; i++)
        //{
        //    Paddle paddle = RedTeam.Paddles[i];
        //    RedSMs[i].SwapPaddle(paddle);
        //    RedSMs[i].WaitTime = PaddleWaitTime;
        //    paddle.mySM = RedSMs[i];
        //}
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i < RedSMs.Count && i < RedTeam.Paddles.Length; i++) {
            RedSMs[i].MyPaddle = RedTeam.Paddles[i];
            RedSMs[i].GO = RedTeam.Paddles[i].gameObject;
        }
        for (int i = 0; i < BlueSMs.Count && i < BlueTeam.Paddles.Length; i++) {
            BlueSMs[i].MyPaddle = BlueTeam.Paddles[i];
            BlueSMs[i].GO = BlueTeam.Paddles[i].gameObject;
        }
    }
}
