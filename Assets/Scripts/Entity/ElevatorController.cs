using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private Transform MovePosition;

    private int currentIndex;
    private int moveDirection;
    private List<Transform> movePositions;
    // Start is called before the first frame update
    void Start()
    {
        moveDirection = 1;
        currentIndex = 0;

        movePositions = new List<Transform>();

        foreach(Transform child in MovePosition)
        {
            movePositions.Add(child);
        }

        transform.position = movePositions[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (movePositions.Count > 1)
        {
            float distance = Vector3.Distance(transform.position, movePositions[currentIndex].position);
            if (Mathf.Abs(distance) <= 0.1f)
            {
                UpdateIndex();
            }
            else
            {
                var nextPosition = Vector3.MoveTowards(transform.position, movePositions[currentIndex].position, MoveSpeed * Time.deltaTime);
                transform.position = nextPosition;
            }
        }
    }

    private void UpdateIndex()
    {
        var nextIndex = currentIndex + moveDirection;
        if(nextIndex >= movePositions.Count || nextIndex < 0)
        {
            moveDirection *= -1;
        }

        currentIndex += moveDirection;
    }
}
