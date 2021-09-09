using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EssentialObjectController : MonoBehaviour
{
    private static EssentialObjectController Singleton { get; set; }

    private void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
