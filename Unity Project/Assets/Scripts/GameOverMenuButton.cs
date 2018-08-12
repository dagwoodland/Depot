using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuButton : OnClick {

    public override void Clicked() {
        Camera.main.GetComponent<Animator>().SetTrigger("PanToMenu");
    }

}
