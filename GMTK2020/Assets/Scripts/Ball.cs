using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float TimeBufferToSpawnAgain = 2f;
    public float TimeUntilSpawnAgainLeft;
    private bool _MomentarilyDead = false;
    public bool MomentarilyDead { get { return _MomentarilyDead; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    protected void Die() {
        _MomentarilyDead = true;
        TimeUntilSpawnAgainLeft = TimeBufferToSpawnAgain;
        //TODO Visuals need to turn off
    }

    // Update is called once per frame
    protected virtual void Update() {        
        if (TimeUntilSpawnAgainLeft > 0) {
            TimeUntilSpawnAgainLeft -= Time.deltaTime;
            if (TimeUntilSpawnAgainLeft <= 0) {
                SpawnOntoLevelAgain();
            }
        }
    }

    private void SpawnOntoLevelAgain() {
        _MomentarilyDead = false;
        //TODO Visuals need to turn on, need to probably get spawn locations, maybe from a map class?
    }

}
