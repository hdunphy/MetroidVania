using System;
using UnityEngine;
using System.Collections;
public abstract class ActionNode : Node
{
    public override string GetClass()
    {
        return "action";
    }
}