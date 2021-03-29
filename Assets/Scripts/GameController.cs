using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //do objeto
    public Text txtScore;
    public Text txtLevel;
    public Text txtHighScore;
    public Text txtGameOver;
    public Text txtJogador;

    //da classe
    public static int Score = 0; //controla a pontuação
    public static int Level = 1; //controla o nível do jogo
    public static float Speed = 1f; //controla a velocidade do jogo

    public static string Top1;
    public static bool gameOver = false;
        
    public static int HighScore;
    public static string jogador;

    //próximas aventuras
    public static string email;
    public static string senha;

    //Atualiza o nivel do jogo após LinesUpdate
    public static int LinesUpdate = 10;
    //linhas destruidas para atualizar o nivel do jogo
    public static int LinesDestroyedUpdate;     
    public static int LinesDestroyed; //linhas destruidas no total
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public static void IncLinesDestroyed(int lines)
    {
        LinesDestroyed += lines;
        LinesDestroyedUpdate += lines;
        if(LinesDestroyedUpdate >= LinesUpdate) //LevelUp
        {
            GameController.LevelUP();
        }
    }
    public static void LevelUP()
    {
        Level++;
        if(Speed > 0.3f)
        {
            Speed = Speed - 0.1F;
        }
    } 
    // Update is called once per frame
    void Update()
    {       

        if (GameController.gameOver)
        {
            txtGameOver.text = "GAME OVER!!!";
            txtGameOver.gameObject.SetActive(true);
            GameController.gameOver = false;
            SceneManager.LoadScene("Menu");            
        }
        else
        {
            txtGameOver.gameObject.SetActive(false);
        }
        txtJogador.text = "JOGADOR \r\n" + GameController.jogador;
        txtScore.text = "PONTUAÇÃO \r\n" + GameController.Score;
        txtLevel.text = "LEVEL \r\n" + GameController.Level;
        txtHighScore.text = "MAIOR PONTUAÇÃO\r\n" + GameController.HighScore;
    }


    // Incrementa o Score do jogo
    /// <summary>
    /// Incrementa o Score do jogo
    /// </summary>
    public static void IncScore(int LinhasDestruidas)
    {
        Score += 10*LinhasDestruidas;
    }

    public static int GetScore()
    {
        return Score;
    }
}
