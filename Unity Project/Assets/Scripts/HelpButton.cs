using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : OnClick {
    public GameObject helpPanel;

    public override void Clicked() {
        helpPanel.SetActive(true);
    }
}
