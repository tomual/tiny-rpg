using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float distanceToMove;
    [SerializeField] private float moveSpeed;

    private bool moveToPoint = false;
    private Vector3 endPosition;
    private Vector3 startRayPosition;

    // Start is called before the first frame update
    void Start()
    {
        endPosition = transform.position;
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
        if (!moveToPoint)
        {
            if (Input.GetKey(KeyCode.A))
            {
                startRayPosition = new Vector3(transform.position.x - distanceToMove, transform.position.y, transform.position.z);
                RaycastHit2D hit = Physics2D.Raycast(startRayPosition, Vector2.left, 0.1f);
                if (hit.collider == null)
                {
                    endPosition = new Vector3(endPosition.x - distanceToMove, endPosition.y, endPosition.z);
                    moveToPoint = true;
                    return;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                startRayPosition = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
                RaycastHit2D hit = Physics2D.Raycast(startRayPosition, Vector2.left, 0.1f);
                if (hit.collider == null)
                {
                    endPosition = new Vector3(endPosition.x + distanceToMove, endPosition.y, endPosition.z);
                    moveToPoint = true;
                    return;
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                startRayPosition = new Vector3(transform.position.x, transform.position.y + distanceToMove, transform.position.z);
                RaycastHit2D hit = Physics2D.Raycast(startRayPosition, Vector2.left, 0.1f);
                if (hit.collider == null)
                {
                    endPosition = new Vector3(endPosition.x, endPosition.y + distanceToMove, endPosition.z);
                    moveToPoint = true;
                    return;
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                startRayPosition = new Vector3(transform.position.x, transform.position.y - distanceToMove, transform.position.z);
                RaycastHit2D hit = Physics2D.Raycast(startRayPosition, Vector2.left, 0.1f);
                if (hit.collider == null)
                {
                    endPosition = new Vector3(endPosition.x, endPosition.y - distanceToMove, endPosition.z);
                    moveToPoint = true;
                    return;
                }
            }
        }
    }


}
