using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySoundEventSystem : MonoBehaviour
{
    public void CallSoundFromGameSoundManager(string soundName)
    {
        GameSoundManager.Singleton.PlaySound(soundName);
    }
}
