using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour{
    public Vector3 HeadingToShootBallOut = Vector3.right;
    public Transform _ExactPlaceToSpawnBall;
    public float ForceToShootBallOutWith = 10;
    public bool AlreadyAboutToSpawnSomething { get { return BallAboutToSpawn != null; } }
    public Ball BallAboutToSpawn;
    public float TelegraphDelay = 1.0f;
    private Animator _Animator;
    private void Awake() {
        _Animator = GetComponent<Animator>();
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


    //Spawner color of 3 signifies purple
    public void StartSpawnABall(Ball ball, int spawnerColor) {
        if (spawnerColor == 0) {
            _Animator.SetBool("Green",true);
        }
        else if (spawnerColor == 1) {
            _Animator.SetBool("Red", true);
            _Animator.SetBool("Green", false);
            _Animator.SetBool("Blue", false);
            _Animator.SetBool("Purple", false);
        }
        else if (spawnerColor == 2) {
            _Animator.SetBool("Blue", true);
            _Animator.SetBool("Green", false);
            _Animator.SetBool("Red", false);
            _Animator.SetBool("Purple", false);

        }
        else if (spawnerColor == 3) {
            _Animator.SetBool("Purple", true);
            _Animator.SetBool("Green", false);
            _Animator.SetBool("Red", false);
            _Animator.SetBool("Blue", false);
        }
        BallAboutToSpawn = ball;
        Invoke("CompleteSpawnABall", TelegraphDelay);
    }

    
    public void CompleteSpawnABall() {
        _Animator.SetBool("Green", false);
        _Animator.SetBool("Red", false);
        _Animator.SetBool("Blue", false);
        _Animator.SetBool("Purple", false);
        BallAboutToSpawn.CompleteSpawnOntoLevelAgain(_ExactPlaceToSpawnBall.position, HeadingToShootBallOut * ForceToShootBallOutWith);
        BallAboutToSpawn = null;
    }
    //Following no polish and deprecated
    public void SpawnABall(Ball ball) {
        ball.CompleteSpawnOntoLevelAgain(_ExactPlaceToSpawnBall.position, HeadingToShootBallOut * ForceToShootBallOutWith);
    }
}
