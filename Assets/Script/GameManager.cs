using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Button gridPrefab;

    [SerializeField]
    private Transform gridTran;

    [SerializeField]
    private Button[] generateGrids;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    /// <summary>
    /// ボタンを生成
    /// </summary>
    private void GenerateGrid()
    {
        generateGrids = new Button[9];

        for(int i = 0;i < 9;i++)
        generateGrids[i] = Instantiate(gridPrefab, gridTran, false);
    }
}
