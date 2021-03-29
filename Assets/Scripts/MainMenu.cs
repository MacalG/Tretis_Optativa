using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text txtHigh;
    public Text txtPlayer;

    public InputField Playername;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txtHigh.text = "PONTUAÇÃO \r\n" + GameController.HighScore;
        txtPlayer.text = "TOP 1 \r\n" + GameController.Top1;        
    }

    public void Fase()
    {
        GameController.Score = 0;
        GameController.jogador = Playername.text;
        SceneManager.LoadScene("SampleScene");        
    }
}
