using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VentTileTransparency : MonoBehaviour
{

    // player
    public GameObject[] p;
    
    public Vector3Int[] tileStart;
    public Vector3Int[] tileEnd;
    public Vector3Int[] tileCounts;

    // cache sprite renderer color
    private Tilemap myTileMap;
    private Color myTileColor;
    public int numb;

    private void Awake()
    {
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");

        tileCounts = new Vector3Int[tileStart.Length];
        myTileMap = GetComponent("Tilemap") as Tilemap;
        myTileColor = myTileMap.color;
    }

    void Update()
    {

        
        if (p[0].GetComponent<P_avoidEnemyVent>().firstTap || p[1].GetComponent<P_avoidEnemyVent>().firstTap)
        {
            myTileColor.a = 0.6f;


            for (int i = 0; i < tileStart.Length; i++)
            {
                tileCounts[i].x = Mathf.Abs(tileStart[i].x - tileEnd[i].x) + 1;

                for (int x = 0; x < tileCounts[i].x; x++)
                {
                    myTileMap.SetTileFlags(new Vector3Int(tileStart[i].x + x, tileStart[i].y, 0), TileFlags.None);
                    myTileMap.SetColor(new Vector3Int(tileStart[i].x + x, tileStart[i].y, 0), myTileColor);
                }
            }
        }
        else if (!p[0].GetComponent<P_avoidEnemyVent>().firstTap && !p[1].GetComponent<P_avoidEnemyVent>().firstTap)
        {
            myTileColor.a = 0.0f;

            for (int i = 0; i < tileStart.Length; i++)
            {
                tileCounts[i].x = Mathf.Abs(tileStart[i].x - tileEnd[i].x) + 1;

                for (int x = 0; x < tileCounts[i].x; x++)
                {
                    myTileMap.SetTileFlags(new Vector3Int(tileStart[i].x + x, tileStart[i].y, 0), TileFlags.None);
                    myTileMap.SetColor(new Vector3Int(tileStart[i].x + x, tileStart[i].y, 0), myTileColor);
                }
            }

        }
        


    }
}
