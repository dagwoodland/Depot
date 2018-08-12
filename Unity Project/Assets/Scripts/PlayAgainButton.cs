using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgainButton : OnClick {

    public override void Clicked() {
        Camera.main.GetComponent<Animator>().SetTrigger("PanGameOverToGame");
        GameManager.gameRunning = true;
        Debug.Log("Play Again!!!");
    }

}
