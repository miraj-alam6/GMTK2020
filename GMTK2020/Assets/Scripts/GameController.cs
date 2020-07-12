using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour{
    public Team[] Teams;
    
    public static GameController Instance;
    //We will probably have more than one score ball on the field, so I'm commenting this out since no references
    //in the project 
    //public ScoreBall TheScoreBall;
    public SwitchBall TheSwitchBall;

    public bool DEBUG_TESTING_STUFF;
    public Paddle DEBUG_PADDLE_THAT_BROKE_BALL;

    public BallSpawner[] BallSpawners;
    public bool GameDone { get; private set;}

    private void Update() {

        if (DEBUG_TESTING_STUFF) {
            //Switch paddles hot key
            if (Input.GetKeyDown(KeyCode.C)) {
                TheSwitchBall.ChangeTwoPaddles(DEBUG_PADDLE_THAT_BROKE_BALL);
            }
            //Reset hot key
            if (Input.GetKeyDown(KeyCode.R)) {
                Time.timeScale = 0f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }
        }

        //How to correctly reset in the game.
        if (GameDone) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }
            if (Input.GetKeyDown(KeyCode.Q)) {
                //TODO: implement quitting to main menu
            }

        }
    }
    private void Awake() {
        Instance = this;
        Time.timeScale = 1.0f;
        for (int i=0; i < Teams.Length;i++) {
            var paddles = Teams[i].Paddles;
            for (int j=0; j < paddles.Length;j++) {
                paddles[j].SetIndexInTeam(j);
            }
        }
    }

    public void StartAGame() {

    }


    public Team GetSpecificTeam(Paddle paddleOfTeamToExclude) {
        return GetSpecificTeam(paddleOfTeamToExclude.GetTeamColor());
    }

    public Team GetSpecificTeam(TeamColor targetTeam) {
        var teamArray = new Team[Teams.Length - 1];
        for (int i = 0; i < Teams.Length; i++) {
            if (Teams[i].MyTeamType == targetTeam) {
                return Teams[i];
            }
        }
        Debug.LogError("Get specific team returned null, this should not happen.");
        return null;
    }

    public Team[] GetAllTeams() {
        return Teams;
    }

    public void SpawnAScoreBall(ScoreBall ball) {        
        int selector = Random.Range(0, 3);
        TeamColor randomColor = (TeamColor)selector;
        ball.ChangeColor(randomColor);
        SpawnABall(ball, (int)randomColor);
    }

    public void SpawnASwitchBall(SwitchBall ball) {
        SpawnABall(ball, 3);
    }

    public void SpawnABall(Ball ball, int color) {
        var validSpawners = new List<BallSpawner>();
        for (int i=0; i < BallSpawners.Length; i++) {
            if (!BallSpawners[i].AlreadyAboutToSpawnSomething) {
                validSpawners.Add(BallSpawners[i]);
            }
        }
        if (validSpawners.Count > 0) {
            var randomBallSpawner = validSpawners[Random.Range(0, validSpawners.Count)];
            randomBallSpawner.StartSpawnABall(ball,color);
        }
    }

    public Team[] GetAllTeamsExceptTarget(Paddle paddleOfTeamToExclude) {
        return GetAllTeamsExceptTarget(paddleOfTeamToExclude.GetTeamColor());
    }

    public Team[] GetAllTeamsExceptTarget(TeamColor teamToExclude) {
        var teamArray = new Team[Teams.Length - 1];
        int j = 0;
        for (int i = 0; i < Teams.Length; i++) {
            if (Teams[i].MyTeamType != teamToExclude && j < teamArray.Length) {
                teamArray[j] = Teams[i];
                j++;
            }
        }
        return teamArray;
    }   
    
    public void EndGame(Team winner) {
        GameDone = true;
        if (winner.MyTeamType == TeamColor.Green) {
            AudioController.Instance.PlaySound(SFXType.YouWinSound);
            GameUI.Instance.ShowWinPanel("You win");
        }
        else{
            AudioController.Instance.PlaySound(SFXType.YouLoseSound);
            GameUI.Instance.ShowWinPanel("You lose");
        }
        Time.timeScale = 0f;
    }

}
