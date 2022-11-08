using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GridController gridPrefab;

    [SerializeField]
    private Transform gridTran;

    [SerializeField]
    private GridController[] generateGrids;

    private int putCount;

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
        generateGrids = new GridController[9];

        for (int i = 0; i < 9; i++)
        {
            generateGrids[i] = Instantiate(gridPrefab, gridTran, false);

            //Gridの初期設定
            generateGrids[i].SetUpgrid(i, this);
        }
    }

    /// <summary>
    /// PlayerがGridをクリックした際の処理
    /// </summary>
    /// <param name="no"></param>
    public void OnnClickGrid(int no)
    {
        Debug.Log($"クリック実行：Gridの通し番号：{ no }");

        if(generateGrids[no].CurrentGridOwnerType == GridOwnerType.None)
        {
            putCount++;

            Debug.Log(generateGrids[no] + "番目のGridに○印をつける");

            // TODO 画面のインフォ表示(配置できないメッセージ)をリセット


            //クリックした Grid に○をセット
            SetOwnerTypeOnGeid(generateGrids[no], GridOwnerType.Player);


            // TODO 配置した数の判定。全 Grid が埋まる回数おいたら、勝負付かず引き分け


            // TODO 敵の順番
        }
        else
        {
            Debug.Log("そこには配置できません。");

            // TODO 印が配置できない Grid なので、配置できないメッセージをゲーム画面に表示する
        }
    }

    /// <summary>
    /// Gridにオーナーシンボル(◯×)をセット
    /// </summary>
    /// <param name="targetGrid"></param>
    /// <param name="setOwnerType"></param>
    private void SetOwnerTypeOnGeid(GridController targetGrid, GridOwnerType setOwnerType)
    {
        targetGrid.UpdateGridData(setOwnerType, setOwnerType == GridOwnerType.Player ? "○" : "×");

        // TODO 勝敗結果を判定
    }
}
