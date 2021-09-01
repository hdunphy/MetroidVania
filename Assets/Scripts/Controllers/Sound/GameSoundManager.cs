using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSoundManager : SoundPlayerBase
{
    public static GameSoundManager Singleton { get; set; }

    protected override void OnAwake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    protected override void OnStart()
    {
    }
}
