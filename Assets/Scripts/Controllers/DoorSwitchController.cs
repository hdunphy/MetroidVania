using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchController : MonoBehaviour
{
    [SerializeField] private DoorController Door;

    public void OnDoorSwitched()
    {
        FindObjectOfType<Damageable>().enabled = false;
        //Animate switch moving to on position
        Door.Open();


        //for testing
        transform.GetComponentInChildren<SpriteRenderer>().color = Color.green;
    }
}
