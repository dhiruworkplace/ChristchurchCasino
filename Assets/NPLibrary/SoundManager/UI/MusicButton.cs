using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ir;

public class MusicButton : Toggle
{
    protected override bool IsOn
    {
        get
        {
            return SoundController.SfxEnable;
        }

        set
        {
            SoundController.SfxEnable = value;
        }
    }
}
