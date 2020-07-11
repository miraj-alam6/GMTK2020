using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunctions{
    public static Color GetUnityColor(TeamColor teamColor) {
        switch (teamColor) {
            case TeamColor.Blue:
                return Color.blue;
            case TeamColor.Red:
                return Color.red;
            case TeamColor.Green:
                return Color.green;
            default:
                return Color.grey;
        }
    }
}
