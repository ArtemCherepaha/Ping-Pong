using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    private GameObject ball;
    private int computerScore;
    private int playerScore;
    private GameObject hudCanvas;
    private Hud hud;
    private GameObject paddleComputer;
    private int winningScore=2;

    public enum GameState
    {
        Playing,
        GameOver,
        Paused,
        Launched
    }
    public GameState gameState = GameState.Launched; 
    // Start is called before the first frame update
    void Start()
    {
        
        paddleComputer = GameObject.Find("computer_paddle");
        hudCanvas = GameObject.Find("HUD_Canvas");
        hud = hudCanvas.GetComponent<Hud>();
        hud.playAgain.text = "PRESS TO PLAY";
    }

    // Update is called once per frame
    void Update()
    {
        CheckScore();
        CheckInput();
    }
    public void CheckInput()
    {
        if (gameState == GameState.Paused || gameState == GameState.Playing)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                PauseResumeGame();
            }
        }
        if (gameState == GameState.Launched || gameState == GameState.GameOver)
        {
            if (Input.GetKeyUp(KeyCode.Space)){
                StartGame();
            }
        }
       
    }
    void CheckScore()
    {
        if (playerScore >= winningScore || computerScore >= winningScore)
        {
            if (playerScore >= winningScore && computerScore < playerScore - 1)
            {
                PlayerWins();
            }else if (computerScore >= winningScore && playerScore < computerScore - 1)
            {
                ComputerWins();
            }
        }
    }
    void SpawnBall()
    {
        ball = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/ball", typeof(GameObject)));
        ball.transform.localPosition = new Vector3(13, 0, -2);
    }
    private void PlayerWins()
    {
        hud.winPlayer.enabled = true;
        GameOver();
    }
    private void ComputerWins()
    {
        hud.winComputer.enabled = true;
        GameOver();
    }
    public void ComputerPoint()
    {
        computerScore++;
        hud.computerScore.text = computerScore.ToString();
        NextRound();
    }
    public void PlayerrPoint()
    {
        playerScore++;
        hud.playerScore.text = playerScore.ToString();
        NextRound();
    }
    public void StartGame()
    {
        playerScore = 0;
        computerScore = 0;
        hud.playerScore.text = "0";
        hud.computerScore.text = "0";
        hud.winComputer.enabled = false;
        hud.winPlayer.enabled = false;
        hud.playAgain.enabled = false;
        gameState = GameState.Playing;
        paddleComputer.transform.localPosition = new Vector3(paddleComputer.transform.localPosition.x, 0, paddleComputer.transform.localPosition.z);
        SpawnBall();
    }
    private void NextRound()
    {
        if (gameState == GameState.Playing)
        {
            paddleComputer.transform.localPosition = new Vector3(paddleComputer.transform.localPosition.x, 0, paddleComputer.transform.localPosition.z);
            GameObject.Destroy(ball.gameObject);
            SpawnBall();
        }
    }
    private void GameOver()
    {
        GameObject.Destroy(ball.gameObject);
        hud.playAgain.text = "PRESS TO PLAY AGAIN";
        hud.playAgain.enabled = true;
        gameState = GameState.GameOver;
    }
    private void PauseResumeGame()
    {
        if (gameState == GameState.Paused)
        {
            gameState = GameState.Playing;
            hud.playAgain.enabled = false;
        }
        else
        {
            gameState = GameState.Paused;
            hud.playAgain.text = "GAME IS PAUSED PRESS TO CONTINUE";
            hud.playAgain.enabled = true;
        }
    }
}
