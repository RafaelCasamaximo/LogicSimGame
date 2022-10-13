using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CircuitSimulatorRenderer : MonoBehaviour
{
    #region Tilemaps
    [SerializeField] private Tilemap backgroundTilemap;
    [SerializeField] private Tilemap cursorTilemap;

    #endregion

    #region Tilesets

    [SerializeField] private TileBase backgroundTileBase;
    [SerializeField] private TileBase cursorTileBase;

    #endregion

    #region Settings

    public int width { get; set; }
    public int height { get; set; }

    public Vector3Int cursorLastPosition;

    #endregion

    #region Background

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
