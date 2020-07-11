using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    public Team TeamToControl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //TODO: wsda control first paddle in array
        var paddle1InputVector = new Vector2(Input.GetAxis("HorizontalOne"), Input.GetAxis("VerticalOne"));
        var paddle2InputVector = new Vector2(Input.GetAxis("HorizontalTwo"), Input.GetAxis("VerticalTwo"));
        TeamToControl.ProcessMoveInputForPaddle(paddle1InputVector,0);
        TeamToControl.ProcessMoveInputForPaddle(paddle2InputVector,1);
        //TODO: arrows control second paddle in array

    }
}
