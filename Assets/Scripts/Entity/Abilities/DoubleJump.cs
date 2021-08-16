using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleJump", menuName = "Scriptable/Abilities/DoubleJump")]
public class DoubleJump : Jump
{
    //Inherits from jump but after use, can no longer use
    public override void Activate(GameObject parent)
    {
        base.Activate(parent);

        HasUse = false;
    }
}
