using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAndrew : MonoBehaviour
{
    public float Speed = 5f;
    public float MaxSpeed = 10f;
    public Vector2 StartFacing = Vector2.down;

    private Rigidbody2D _Rigidbody;

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        transform.right = StartFacing.normalized;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        MoveBall();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paddle"))     //Add environment
        {
            //set new direction
            // -2*(V dot N)*N + V
            PaddleAndrew paddle = other.GetComponent<PaddleAndrew>();
            //transform.right = Vector2.Reflect(Speed * Time.deltaTime * transform.right, other.transform.up);
            Vector2 ballVel = Speed * Time.deltaTime * transform.right;
            //Vector2 adjusteddNormal = new Vector2(paddle.Speed * 10, transform.up.y);
            transform.right = -2 * Vector2.Dot(ballVel, other.transform.up) * (Vector2)other.transform.up + ballVel;
            Debug.Log(transform.right);
        }
    }

    public void MoveBall()
    {
        transform.Translate(Speed * Time.deltaTime * Vector2.right);
    }
}
