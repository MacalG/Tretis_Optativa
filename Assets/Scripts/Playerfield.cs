using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerfield : MonoBehaviour
{
    /*  Matriz representando todas as coordenadas do jogo 
        ___|_0_|_1_|_2_|...
         0 | o | x | o |...
         1 | o | x | x |...
         2 | x | x | o |...
        ...|...|...|...|...
    */
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];

    //arredonda o valor x e y de um vetor
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),
                           Mathf.Round(v.y));
    }

    //verifica se o objeto se encontra dentro da área do jogo
    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
                (int)pos.x < w &&
                (int)pos.y >= 0);
    }

    //deletar os elementos de uma linha
    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    //joga os elementos da linha atual para a linha de baixo
    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                // Move one towards bottom
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Update Block position
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    //movimenta todas as linhas acima da linha deleta
    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    //verifica se a linha esta cheia
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    //deleta todas as linhas cheias
    public static int deleteFullRows()
    {
        int count = 0;
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                count++;
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
        }
        return count;
    }
    
}
