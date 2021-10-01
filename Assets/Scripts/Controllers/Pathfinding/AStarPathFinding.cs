using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinding : MonoBehaviour, IPathFinding
{
    [SerializeField, Tooltip("How far the pathfinding will check for the target")] private float FollowRadius;
    [SerializeField, Tooltip("Distance between each step in the algorithm checks for a path")] private float StepSize;
    [SerializeField, Tooltip("How close to the next move before popping the next move in the path")] private float NextMoveCheck;
    [SerializeField, Tooltip("Collision radius for each step check")] private float StepCollisionSize;
    [SerializeField, Tooltip("TileMap Offset for checking collisions")] private Vector2 TilemapOffset;

    public bool _debug;

    private Stack<Vector2> Path;
    private Vector2 nextMove;
    private Vector2 target;

    private LayerMask ObstacleLayer;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        ObstacleLayer = GameLayers.Singleton.GroundLayer;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (_debug && Application.isPlaying)
        {
            foreach (Vector2 _tile in Path)
            {
                Gizmos.DrawSphere(_tile, .1f);
            }
        }
    }

    public void Initialize()
    {
        Path = new Stack<Vector2>();
        nextMove = transform.position;
    }

    public Vector2 GetDirection()
    {
        Vector2 direction;
        Vector2 pos = transform.position;

        if (/*(Mathf.Abs(Vector2.Distance(pos, target))) < FollowRadius - 1 || */Path.Count == 0)
        {
            direction = Vector2.zero;
        }
        else
        {
            if (Mathf.Abs(Vector2.Distance(nextMove, pos)) < NextMoveCheck)
            {
                nextMove = Path.Pop();
            }
            direction = (nextMove - pos).normalized;
        }

        return direction;
    }

    public int StepsLeftInPath()
    {
        return Path.Count;
    }

    public void UpdatePath(Vector2 _target)
    {
        Vector2 roundedTarget = Vector2Int.RoundToInt(_target) /*+ TilemapOffset*/;
        if (roundedTarget != target)
        {
            target = roundedTarget;
            float Distance = Vector2.Distance(_target, transform.position);
            if (Distance < FollowRadius)
                CalculatePath();
        }
    }

    private void CalculatePath()
    {
        Path.Clear();
        Vector2 currentPos = Vector2Int.RoundToInt(transform.position) /*+ TilemapOffset*/;

        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();
        Dictionary<Vector2, float> costSoFar = new Dictionary<Vector2, float>
        {
            { currentPos, 0 }
        };

        PriorityQueue<Vector2> checkTileQueue = new PriorityQueue<Vector2>();
        checkTileQueue.Add(new PriorityElement<Vector2>(currentPos, GetHueristic(currentPos)));

        int count = 0;
        while (!checkTileQueue.IsEmpty() && count++ < 150)
        {
            PriorityElement<Vector2> _CurrentElement = checkTileQueue.Dequeue();
            if (_CurrentElement.Item.Equals(target))
            {
                break;
            }

            List<Vector2> possibleMoves = GetAdjacents(_CurrentElement.Item);
            foreach (Vector2 _move in possibleMoves)
            {
                float cost = costSoFar[_CurrentElement.Item] + StepSize;
                if (!costSoFar.ContainsKey(_move))
                {
                    costSoFar.Add(_move, cost);
                    AddPossibleMove(cost, _move, checkTileQueue, cameFrom, _CurrentElement.Item);
                }
                else if (cost < costSoFar[_move])
                {
                    costSoFar[_move] = cost;
                    AddPossibleMove(cost, _move, checkTileQueue, cameFrom, _CurrentElement.Item);
                }
            }
        }

        if (cameFrom.ContainsKey(target))
        {
            GeneratePath(cameFrom, currentPos);
        }
    }

    private void GeneratePath(Dictionary<Vector2, Vector2> cameFrom, Vector2 currentPos)
    {
        Vector2 _nextMove = target;
        while (!_nextMove.Equals(currentPos))
        {

            Path.Push(_nextMove);
            _nextMove = cameFrom[_nextMove];
        }

        nextMove = transform.position;
    }

    private void AddPossibleMove(float cost, Vector2 _move, PriorityQueue<Vector2> checkTileQueue,
        Dictionary<Vector2, Vector2> cameFrom, Vector2 _currentTile)
    {
        bool addToPriorityQueue;
        
        if (cameFrom.ContainsKey(_move))
        {
            if (cameFrom[_move] == _currentTile)
            {
                //This already exists, don't add move to stack
                addToPriorityQueue = false;
            }
            else
            {
                addToPriorityQueue = true;
                cameFrom[_move] = _currentTile;
            }
        }
        else
        {
            addToPriorityQueue = true;
            cameFrom.Add(_move, _currentTile);
        }

        if (addToPriorityQueue)
        {
            float prority = cost + GetHueristic(_move);
            checkTileQueue.Add(new PriorityElement<Vector2>(_move, prority));
        }
    }

    private List<Vector2> GetAdjacents(Vector2 currentTile)
    {
        List<Vector2> adjacents = new List<Vector2>();
        for (float i = -StepSize; i <= StepSize; i += StepSize)
        {
            for (float j = -StepSize; j <= StepSize; j += StepSize)
            {
                Vector2 _tile = currentTile + new Vector2(i, j) /*+ TilemapOffset*/;

                if (!(i == 0 && j == 0))
                {
                    if (!Physics2D.OverlapCircle(_tile, StepCollisionSize, ObstacleLayer))
                    {
                        adjacents.Add(_tile);
                    }
                }
            }
        }

        return adjacents;
    }

    private float GetHueristic(Vector2 currentPos)
    {
        return Vector2.Distance(currentPos, target);
    }
}

public class PriorityElement<T>
{
    public T Item { get; private set; }
    public float Priority { get; private set; }
    public PriorityElement(T _item, float _priority)
    {
        Item = _item;
        Priority = _priority;
    }

    public void SetPriority(int priority)
    {
        Priority = priority;
    }
}

public class PriorityQueue<T>
{
    private List<PriorityElement<T>> Queue;

    public PriorityQueue()
    {
        Queue = new List<PriorityElement<T>>();
    }

    public bool IsEmpty() { return Queue.Count == 0; }

    public void Add(PriorityElement<T> element)
    {
        int index;
        for (index = 0; index < Queue.Count; index++)
        {
            if (element.Priority < Queue[index].Priority)
            {
                break;
            }
        }

        Queue.Insert(index, element);
    }

    public PriorityElement<T> Dequeue()
    {
        PriorityElement<T> head;

        if (Queue.Count == 0)
        {
            head = null;
        }
        else
        {
            head = Queue[0];
            Queue.RemoveAt(0);
        }

        return head;
    }

    public PriorityElement<T> Peek()
    {
        return Queue[0];
    }
}
