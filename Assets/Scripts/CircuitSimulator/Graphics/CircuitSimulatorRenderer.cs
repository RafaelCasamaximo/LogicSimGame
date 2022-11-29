using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CircuitSimulatorRenderer : MonoBehaviour
{
    #region Tilemaps
    /// <summary>
    /// Parametros para os tilemaps do fundo e o tile do cursor e suas variações que serão usados para popular
    /// </summary>
    [SerializeField] private Tilemap backgroundTilemap;
    [SerializeField] private Tilemap cursorTilemap;

    #endregion

    #region Tilesets
    /// <summary>
    /// Armazena os tilebase (tiles individuais) que serão utilizados
    /// </summary>
    [SerializeField] private TileBase backgroundTileBase;
    [SerializeField] private TileBase cursorTileBase;

    #endregion

    #region Settings
    /// <summary>
    /// Define a altura e largura do grid do circuitSimulator. Esses parametros são passados através de setters no CircuitManager
    /// </summary>
    public int width { get; set; }
    public int height { get; set; }

    
    /// <summary>
    /// Auxiliar responsável pela atualização da posição do cursor na tela
    /// </summary>
    public Vector3Int cursorLastPosition;

    #endregion

    #region Background

    /// <summary>
    /// Preenche o Background com o backgrounTileBase e os parametros informados
    /// </summary>
    public void FillBackground()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                backgroundTilemap.SetTile(new Vector3Int(i, j), backgroundTileBase);
            }
        }
    }

    /// <summary>
    /// Método para alterar a posição do cursor na tela
    /// </summary>
    /// <param name="newCursorPosition">Novo Vector3Int da posição do cursor</param>
    public void ChangeCursorPosition(Vector3Int newCursorPosition)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                cursorTilemap.SetTile(new Vector3Int(i, j), backgroundTileBase);
            }
        }
        newCursorPosition.x = Mathf.Clamp(newCursorPosition.x, 0, width);
        newCursorPosition.y = Mathf.Clamp(newCursorPosition.y, 0, height);
        cursorTilemap.SetTile(newCursorPosition, cursorTileBase);
    }

    #endregion
}
