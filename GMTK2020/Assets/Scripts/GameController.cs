using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour{
    public Team[] Teams;
    
    public static GameController Instance;
    public ScoreBall TheScoreBall;
    public SwitchBall TheSwitchBall;

    public bool DEBUG_TESTING_STUFF;
    public Paddle DEBUG_PADDLE_THAT_BROKE_BALL;
    private void Update() {

        if (DEBUG_TESTING_STUFF) {
            //Switch paddles hot key
            if (Input.GetKeyDown(KeyCode.C)) {
                TheSwitchBall.ExplodeAndChangeTwoPaddles(DEBUG_PADDLE_THAT_BROKE_BALL);
            }
            //Reset hot key
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }
        }
    }
    private void Awake() {
        Instance = this;
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
}
