using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Tetrominoes")]
public class TetrominoesSO : ScriptableObject
{
    public List<TetrominoData> list;

    public TetrominoData GetRandomTetromino()
    {
        int random = Random.Range(0,list.Count);
        return list[random];
    }
}
