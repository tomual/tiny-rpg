using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float distanceToMove;
    [SerializeField] private float moveSpeed;

    private bool moveToPoint = false;
    private bool listening;
    private Vector3 endPosition;
    private Vector3 startRayPosition;

    GameObject blackScreen;

    private enum Direction { Left, Right, Up, Down };
    private Direction facing;

    // Start is called before the first frame update
    void Start()
    {
        endPosition = transform.position;
        facing = Direction.Down;
        blackScreen = GameObject.FindGameObjectWithTag("BlackScreen");
        SetListening(false);
    }

    void FixedUpdate()
    {
        if (moveToPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
            //Debug.Log(Vector3.Distance(transform.position, endPosition));
            if (Vector3.Distance(transform.position, endPosition) == 0)
            {
                moveToPoint = false;
            }
        }
    }

    void Update()
    {
        if (!moveToPoint && !IsListening())
        {
            if (Input.GetKey(KeyCode.A))
            {
                startRayPosition = new Vector3(transform.position.x - distanceToMove, transform.position.y, transform.position.z);
                if (CanWalkAhead(startRayPosition))
                {
                    endPosition = new Vector3(endPosition.x - distanceToMove, endPosition.y, endPosition.z);
                    moveToPoint = true;
                    facing = Direction.Left;
                    return;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                startRayPosition = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
                if (CanWalkAhead(startRayPosition))
                {
                    endPosition = new Vector3(endPosition.x + distanceToMove, endPosition.y, endPosition.z);
                    moveToPoint = true;
                    facing = Direction.Right;
                    return;
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                startRayPosition = new Vector3(transform.position.x, transform.position.y + distanceToMove, transform.position.z);
                if (CanWalkAhead(startRayPosition))
                {
                    endPosition = new Vector3(endPosition.x, endPosition.y + distanceToMove, endPosition.z);
                    moveToPoint = true;
                    facing = Direction.Up;
                    return;
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                startRayPosition = new Vector3(transform.position.x, transform.position.y - distanceToMove, transform.position.z);
                if (CanWalkAhead(startRayPosition))
                {
                    endPosition = new Vector3(endPosition.x, endPosition.y - distanceToMove, endPosition.z);
                    moveToPoint = true;
                    facing = Direction.Down;
                    return;
                }
            }
            if (Input.GetKey(KeyCode.Return))
            {
                if (CanTalkAhead())
                {
                    Talk();
                    return;
                }
            }
        }
    }

    bool CanWalkAhead(Vector3 startRayPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(startRayPosition, Vector2.left, 0.1f);
        if (hit.collider != null)
        {
            if (!hit.collider.isTrigger)
            {
                return false;
            }
        }
        return true;
    }

    bool CanTalkAhead()
    {
        Collider2D tile = GetTileAhead();
        if (tile != null)
        {
            if (tile.tag == "Interactable")
            {
                return true;
            }
        }
        return false;
    }

    public Collider2D GetTileAhead()
    {
        switch (facing)
        {
            case Direction.Left:
                startRayPosition = new Vector3(transform.position.x - distanceToMove, transform.position.y, transform.position.z);
                break;
            case Direction.Right:
                startRayPosition = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
                break;
            case Direction.Up:
                startRayPosition = new Vector3(transform.position.x, transform.position.y + distanceToMove, transform.position.z);
                break;
            case Direction.Down:
                startRayPosition = new Vector3(transform.position.x, transform.position.y - distanceToMove, transform.position.z);
                break;
        }
        RaycastHit2D hit = Physics2D.Raycast(startRayPosition, Vector2.left, 0.1f);
        return hit.collider;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.tag == "Teleporter")
        {
            Debug.Log("Teleporter");
            blackScreen.GetComponentInChildren<CanvasGroup>().alpha = 1;
        }
    }

    private void Talk()
    {
        Collider2D actor = GetTileAhead();
        actor.GetComponent<Interactable>().StartConversation();
        SetListening(true);
    }

    public void SetListening(bool listening)
    {
        this.listening = listening;
    }

    public bool IsListening()
    {
        return listening;
    }
}
