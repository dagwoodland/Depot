using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : OnClick {

    public override void Clicked() {
        GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>().GameOver(false);
        Camera.main.GetComponent<Animator>().SetTrigger("PanGameToMenu");
    }

}
