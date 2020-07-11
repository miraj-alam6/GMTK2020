using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour{
    public TeamColor MyColor;
    private SpriteRenderer _MySpriteRenderer;

    private void Awake() {
        _MySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _MySpriteRenderer.color = StaticFunctions.GetUnityColor(MyColor);
    }
}
