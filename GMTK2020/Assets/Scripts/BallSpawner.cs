using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour{
    public Vector3 HeadingToShootBallOut = Vector3.right;
    public Transform _ExactPlaceToSpawnBall;
    public float ForceToShootBallOutWith = 10;

    private void Awake() {
        if (_ExactPlaceToSpawnBall == null) {
            _ExactPlaceToSpawnBall = this.transform;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnABall(Ball ball) {
        ball.CompleteSpawnOntoLevelAgain(_ExactPlaceToSpawnBall.position, HeadingToShootBallOut * ForceToShootBallOutWith);
    }
}
