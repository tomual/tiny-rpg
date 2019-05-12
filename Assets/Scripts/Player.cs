using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float distanceToMove;
    [SerializeField] private float moveSpeed;

    private bool moveToPoint = false;
    private Vector3 endPosition;

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
            Debug.Log(Vector3.Distance(transform.position, endPosition));
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
            if (Input.GetKey(KeyCode.A)) //Left
            {
                endPosition = new Vector3(endPosition.x - distanceToMove, endPosition.y, endPosition.z);
                moveToPoint = true;
            }
            if (Input.GetKey(KeyCode.D)) //Right
            {
                endPosition = new Vector3(endPosition.x + distanceToMove, endPosition.y, endPosition.z);
                moveToPoint = true;
            }
            if (Input.GetKey(KeyCode.W)) //Up
            {
                endPosition = new Vector3(endPosition.x, endPosition.y + distanceToMove, endPosition.z);
                moveToPoint = true;
            }
            if (Input.GetKey(KeyCode.S)) //Down
            {
                endPosition = new Vector3(endPosition.x, endPosition.y - distanceToMove, endPosition.z);
                moveToPoint = true;
            }
        }
        

    }
}
