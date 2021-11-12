using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    [SerializeField] private Camera Camera;

    public void SetCameraPosition(Vector3 pos)
    {
        Camera.transform.position = pos;
    }
}
