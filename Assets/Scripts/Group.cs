using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    // Tempo decorrido da ultima queda do bloco
    float lastFall = 0;
    void Start()
    {
        // verifica se o bloco começa em um posição valida
        //caso não comece o jogo é finalizado
        if (!isValidGridPos())
        {
            if (GameController.Score > GameController.HighScore)
            {
                GameController.Top1 = GameController.jogador;
                GameController.HighScore = GameController.Score;
            }
            GameController.gameOver = true;
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("SCORE: "+GameController.GetScore());
        // esquerda
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // modifica a posição 
            transform.position += new Vector3(-1, 0, 0);

            // valida se a posição é valida
            if (isValidGridPos())
                // atualiza a grid
                updateGrid();
            else
                // volta para a posição original
                transform.position += new Vector3(1, 0, 0);
        }

        // direita
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // modifica a posição 
            transform.position += new Vector3(1, 0, 0);

            // valida se a posição é valida
            if (isValidGridPos())
                // atualiza a grid
                updateGrid();
            else
                // volta para a posição original
                transform.position += new Vector3(-1, 0, 0);
        }

        // rotacionar o componente
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //rotaciona
            transform.Rotate(0, 0, -90);

            // valida se a posição é valida
            if (isValidGridPos())
                // atualiza a grid
                updateGrid();
            else
                // volta para a rotação original
                transform.Rotate(0, 0, 90);
        }

        // Baixo
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                 Time.time - lastFall >= GameController.Speed)
        {
            // modifica a posição 
            transform.position += new Vector3(0, -1, 0);

            // valida se a posição é valida
            if (isValidGridPos())
            {
                // atualiza a grid
                updateGrid();
            }
            else
            {
                // volta para a posição original
                transform.position += new Vector3(0, 1, 0);
                // Limpa as linhas horizontais e 
                //retorna quantas linhas foram removidas
                int count = Playerfield.deleteFullRows();
                // incrementa a pontuação do player
                GameController.IncScore(count);
                GameController.IncLinesDestroyed(count);
                // cria novo componente
                FindObjectOfType<Spawner>().spawnNext();
                // desativa script
                enabled = false;
                
            }
            //atualiza o tempo
            lastFall = Time.time;
        }
    }

    //verifica se o bloco esta em uma posição válida na grid
    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playerfield.roundVec2(child.position);

            // se não estiver dentro da área do jogo interrompe a função
            if (!Playerfield.insideBorder(v))
                return false;

            // verifica se a peça não faz parte do mesmo grupo
            if (Playerfield.grid[(int)v.x, (int)v.y] != null &&
                Playerfield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    void updateGrid()
    {
        // Remove os filhos antigos da grid
        for (int y = 0; y < Playerfield.h; ++y)
            for (int x = 0; x < Playerfield.w; ++x)
                if (Playerfield.grid[x, y] != null)
                    if (Playerfield.grid[x, y].parent == transform)
                        Playerfield.grid[x, y] = null;

        // adiciona novos filhos a grid
        foreach (Transform child in transform)
        {
            Vector2 v = Playerfield.roundVec2(child.position);
            Playerfield.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
