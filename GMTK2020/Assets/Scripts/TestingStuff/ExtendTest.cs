using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendTest : BaseTest
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Debug.Log("Hello extended");
    }
}
