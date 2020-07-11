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
        var teamArray = new Team[Teams.Length - 1];
        for (int i = 0; i < Teams.Length; i++) {
            if (Teams[i].MyTeamType == paddleOfTeamToExclude.MyTeam) {
                return Teams[i];
            }
        }
        Debug.LogError("Get specific team returned null, this should not happen.");
        return null;
    }

    public Team[] GetAllTeams() {
        return Teams;
    }

    public Team[] GetAllTeamsExceptTargetPaddle(Paddle paddleOfTeamToExclude) {
        var teamArray = new Team[Teams.Length - 1];
        int j = 0;
        for (int i=0; i< Teams.Length; i++) {
            if (Teams[i].MyTeamType != paddleOfTeamToExclude.MyTeam && j < teamArray.Length) {
                teamArray[j] = Teams[i];
                j++;
            }
        }
        return teamArray;
    }

}
