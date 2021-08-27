using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blackboard
{
    private Dictionary<string, object> Parameters;

    public Blackboard()
    {
        Parameters = new Dictionary<string, object>();
    }

    public Blackboard(Dictionary<string, object> parameters)
    {
        Parameters = parameters;
    }

    public object GetParameter(string parameter)
    {
        return Parameters[parameter];
    }

    public void AddParameter(string parameter, object _object)
    {
        Parameters.Add(parameter, _object);
    }

    public void SetParameter(string parameter, object _object)
    {
        Parameters[parameter] = _object;
    }
}
