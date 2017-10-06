using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TestTile : TileBase
{
    public GameObject TilePrefab;
    public Tile.ColliderType ColliderType;
    //public Sprite TileSprite;
    //public Color TileColor;
    //public TileFlags Flags;


    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)

    {
        base.GetTileData(position, tilemap, ref tileData);
        tileData.gameObject = TilePrefab;
        tileData.colliderType = ColliderType;
    }
}
