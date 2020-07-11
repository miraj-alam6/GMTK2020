using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{
    Team[] Teams;
    
    public static GameController Instance;
    public ScoreBall TheScoreBall;
    public SwitchBall TheSwitchBall;

    private void Update() {

    }
    private void Awake() {
        Instance = this;
    }

    public void StartAGame() {

    }


    public Team GetSpecificTeam(Paddle paddleOfTeamToExclude) {
        return GetSpecificTeam(paddleOfTeamToExclude.MyTeam);
    }

    public Team GetSpecificTeam(GameColor targetTeam) {
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
        return GetAllTeamsExceptTarget(paddleOfTeamToExclude.MyTeam);
    }

    public Team[] GetAllTeamsExceptTarget(GameColor teamToExclude) {
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
