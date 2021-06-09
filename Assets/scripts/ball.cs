using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public float moveSpeed = 22f;
    public Vector2 ballDirection = Vector2.left;
    public float topBounds=15f;
    public float bottomBounds = -15f;
    private float playerPaddleHeight, playerPaddleWidth, computerPaddleHeight, computerPaddleWidth, playerPaddleMaxX, playerPaddleMaxY, playerPaddleMinX, playerPaddleMinY, computerPaddleMinY, computerPaddleMinX, computerPaddleMaxX, computerPaddleMaxY, ballWidth, ballHeight;
    private GameObject paddlePlayer, paddleComputer;
    private float bounceAngle;
    private float vx, vy;
    private float maxAngle = 45f;
    private bool colliderWithPlayer, colliderWithComputer, colliderWithWall;
    private game game;
    private bool asignetpoint;
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("Game").GetComponent<game>();
        if (moveSpeed < 0)
            moveSpeed = -1 * moveSpeed;

        paddlePlayer = GameObject.Find("player_paddle");
        paddleComputer = GameObject.Find("computer_paddle");

        playerPaddleHeight = paddlePlayer.transform.GetComponent<SpriteRenderer>().bounds.size.y;
        playerPaddleWidth = paddlePlayer.transform.GetComponent<SpriteRenderer>().bounds.size.x;
        computerPaddleHeight = paddleComputer.transform.GetComponent<SpriteRenderer>().bounds.size.y;
        computerPaddleWidth = paddleComputer.transform.GetComponent<SpriteRenderer>().bounds.size.x;
        ballHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y;
        ballWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x;

        playerPaddleMaxX = paddlePlayer.transform.localPosition.x + playerPaddleWidth / 2;
        playerPaddleMinX = paddlePlayer.transform.localPosition.x - playerPaddleWidth / 2;

        computerPaddleMaxX = paddleComputer.transform.localPosition.x - computerPaddleWidth / 2;
        computerPaddleMinX = paddleComputer.transform.localPosition.x + computerPaddleWidth / 2;

        bounceAngle = GetRandomBounceAngle();
        vx = moveSpeed * Mathf.Cos(bounceAngle);
        vy = moveSpeed * -Mathf.Sin(bounceAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if (game.gameState != game.GameState.Paused)
        {
            Move();
        }       
    }
    bool CheckCollision()
    {
        playerPaddleMaxY = paddlePlayer.transform.localPosition.y + playerPaddleHeight / 2;
        playerPaddleMinY = paddlePlayer.transform.localPosition.y - playerPaddleHeight / 2;

        computerPaddleMaxY = paddleComputer.transform.localPosition.y + computerPaddleHeight / 2;
        computerPaddleMinY = paddleComputer.transform.localPosition.y - computerPaddleHeight / 2;

        if (transform.localPosition.x - ballWidth / 1 < playerPaddleMaxX && transform.localPosition.x + ballWidth / 1 > playerPaddleMinX)
        {
            if (transform.localPosition.y - ballHeight / 1 < playerPaddleMaxY && transform.localPosition.y + ballHeight / 1 > playerPaddleMinY)
            {
                ballDirection = Vector2.right;
                colliderWithPlayer = true;
                transform.localPosition = new Vector3(playerPaddleMaxX + ballWidth / 1, transform.localPosition.y, transform.localPosition.z);
                return true;

            } else
            {
                
                if (!asignetpoint)
                {
                    asignetpoint = true;

                    game.ComputerPoint();
                }
            }
        }
        if (transform.localPosition.x + ballWidth / 1 > computerPaddleMaxX && transform.localPosition.x - ballWidth / 1 < computerPaddleMinX)
        {
            if (transform.localPosition.y - ballHeight / 1 < computerPaddleMaxY && transform.localPosition.y + ballHeight / 1 > computerPaddleMinY)
            {
                ballDirection = Vector2.left;
                colliderWithComputer = true;
                transform.localPosition = new Vector3(computerPaddleMaxX - ballWidth / 1, transform.localPosition.y, transform.localPosition.z);
                return true;
            }
            else
            {
                if (!asignetpoint)
                {
                    asignetpoint = true;
                    game.PlayerrPoint();
                }
            }
        }

        if (transform.localPosition.y > topBounds)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, topBounds, transform.localPosition.z);
            colliderWithWall = true;
            return true;
        }
        if (transform.localPosition.y < bottomBounds)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, bottomBounds, transform.localPosition.z);
            colliderWithWall = true;
            return true;

        }
        return false;
    }
    void Move()
    {
        if (!CheckCollision())
        {
            vx = moveSpeed * Mathf.Cos(bounceAngle);
            if (moveSpeed > 0)
                vy = moveSpeed * -Mathf.Sin(bounceAngle);
            else vy = moveSpeed * Mathf.Sin(bounceAngle);

            transform.localPosition += new Vector3(ballDirection.x * vx * Time.deltaTime, vy * Time.deltaTime, 0);
        }
        else
        {
            if (moveSpeed < 0)
                moveSpeed = -1 * moveSpeed;
            if (colliderWithPlayer)
            {
                colliderWithPlayer = false;
                float relativeIntersectY = paddlePlayer.transform.localPosition.y - transform.localPosition.y;
                float normalizedRelativeIntersectionY = (relativeIntersectY / (playerPaddleHeight / 1));

                bounceAngle = normalizedRelativeIntersectionY * (maxAngle * Mathf.Deg2Rad);
            }
            else if (colliderWithComputer)
            {
                colliderWithComputer = false;
                float relativeIntersectY = paddleComputer.transform.localPosition.y - transform.localPosition.y;
                float normalizedRelativeIntersectionY = (relativeIntersectY / (computerPaddleHeight / 1));

                bounceAngle = normalizedRelativeIntersectionY * (maxAngle * Mathf.Deg2Rad);
            }else if (colliderWithWall)
            {
                colliderWithWall = false;
                bounceAngle = -bounceAngle;
            }
        }
    }
    float GetRandomBounceAngle(float minDegrees = 160f, float maxDegrees = 260f)
    {
        float minRad = minDegrees * Mathf.PI / 180;
        float maxRad = maxDegrees * Mathf.PI / 180;
        return Random.Range(minRad, maxRad);
    }
}
