using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float PauseTime;
    [SerializeField] private Transform MovePosition;

    private int currentIndex;
    private int moveDirection;
    private float currentPauseTime;
    private bool atPosition;
    private Vector3 moveDistance;
    private List<Transform> movePositions;
    private List<GameObject> connectedEntities;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = 1;
        currentIndex = 0;
        currentPauseTime = 0;
        atPosition = false;

        movePositions = new List<Transform>();
        connectedEntities = new List<GameObject>();

        foreach(Transform child in MovePosition)
        {
            movePositions.Add(child);
        }

        transform.position = movePositions[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        moveDistance = Vector3.zero;

        if (movePositions.Count > 1)
        {
            if(atPosition && currentPauseTime > 0)
            {
                currentPauseTime -= Time.deltaTime;
            }
            else
            {
                atPosition = false;

                float distance = Vector3.Distance(transform.position, movePositions[currentIndex].position);
                if (Mathf.Abs(distance) <= 0.1f)
                {
                    atPosition = true;
                    currentPauseTime = PauseTime;

                    UpdateIndex();
                }
                else
                {
                    var nextPosition = Vector3.MoveTowards(transform.position, movePositions[currentIndex].position, MoveSpeed * Time.deltaTime);
                    moveDistance = new Vector2((nextPosition - transform.position).x, 0);
                    transform.position = nextPosition;
                }
            }
        }
    }

    private void LateUpdate()
    {
        foreach(var entity in connectedEntities)
        {
            entity.transform.position += moveDistance;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IEntityMovement entity))
        {
            connectedEntities.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        connectedEntities.Remove(collision.gameObject);
    }
}
