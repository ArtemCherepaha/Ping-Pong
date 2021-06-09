using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float topBounds = 8.3f;
    public float bottomBounds = -8.3f;
    public Vector2 startingPosition = new Vector2(-13.0f, 0f);
    game game;
    
    // Start is called before the first frame update
    void Start()
    {   
        game = GameObject.Find("Game").GetComponent<game>();
        transform.localPosition = (Vector3)startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.gameState == game.GameState.Playing)
        {
           CheckUserInput();
            
        }
        
    }
    void CheckUserInput()
    {
       if (Input.GetKey(KeyCode.UpArrow))
        
            if (transform.localPosition.y >= topBounds)
            
                transform.localPosition = new Vector3(transform.localPosition.x, topBounds, transform.localPosition.z);
            
        
        else 
            if (transform.localPosition.y <= bottomBounds)
            
                transform.localPosition = new Vector3(transform.localPosition.x, bottomBounds, transform.localPosition.z);
            
            
    }



}
