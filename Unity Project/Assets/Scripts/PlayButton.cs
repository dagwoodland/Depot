using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : OnClick {
    
    public override void Clicked() {
        Camera.main.GetComponent<Animator>().SetTrigger("PanToGame");
        GameManager.gameRunning = true;
    }

}
