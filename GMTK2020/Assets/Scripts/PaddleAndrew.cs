using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAndrew : MonoBehaviour
{
    public float Speed = 0f;
    public float MaxSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        MovePaddle();
    }

    void MovePaddle()
    {
        Speed = Input.GetAxis("Horizontal") * Time.deltaTime * MaxSpeed;      //source to be changed
        transform.Translate(Speed * Vector2.right);    //move relative to the paddle's current position
    }
}
