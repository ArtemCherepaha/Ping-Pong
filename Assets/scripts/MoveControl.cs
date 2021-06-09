using UnityEngine;

public class MoveControl : MonoBehaviour
{
    private Rigidbody2D rb;

    private float moveSpeed;

    private bool moveLeft, moveRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 8f;
        
        moveLeft = false;
        moveRight = false;
    }

    public void MoveLeft()
    {
        moveLeft = true;        
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    

    public void StopMoving()
    {
        moveLeft = false;
        moveRight = false;
        rb.velocity = Vector2.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        if (moveLeft)
        {
            rb.velocity = new Vector2(0f, -moveSpeed);
        }

        if (moveRight)
        {
            rb.velocity = new Vector2(0f,moveSpeed);
        }
    }
}
