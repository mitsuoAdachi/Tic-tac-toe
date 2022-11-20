//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GameManager : MonoBehaviour
//{
//    [SerializeField]
//    private GridController gridPrefab;

//    [SerializeField]
//    private Transform gridTran;

//    [SerializeField]
//    private GridController[] generateGrids;

//    private int putCount;

//    [SerializeField]　　　　//SerializeField 属性はデバッグ用です。動作確認が終了したら、SerializeField属性は削除します。
//    private bool isGameUp;

//    // Start is called before the first frame update
//    void Start()
//    {
//        GenerateGrid();

//        JudgeWinner();
//    }

//    /// <summary>
//    /// ボタンを生成
//    /// </summary>
//    private void GenerateGrid()
//    {
//        generateGrids = new GridController[9];

//        for (int i = 0; i < 9; i++)
//        {
//            generateGrids[i] = Instantiate(gridPrefab, gridTran, false);

//            //Gridの初期設定
//            generateGrids[i].SetUpgrid(i, this);
//        }
//    }

//    /// <summary>
//    /// PlayerがGridをクリックした際の処理
//    /// </summary>
//    /// <param name="no"></param>
//    public void OnnClickGrid(int no)
//    {
//        Debug.Log($"クリック実行：Gridの通し番号：{ no }");

//        if (generateGrids[no].CurrentGridOwnerType == GridOwnerType.None)
//        {
//            putCount++;

//            Debug.Log(generateGrids[no] + "番目のGridに○印をつける");

//            // TODO 画面のインフォ表示(配置できないメッセージ)をリセット


//            //クリックした Grid に○をセット
//            SetOwnerTypeOnGrid(generateGrids[no], GridOwnerType.Player);


//            // 配置した数の判定。全 Grid が埋まる回数おいたら、勝負付かず引き分け
//            if (putCount >= 5 && !isGameUp)
//            {
//                ShowResult(GridOwnerType.Draw);
//                return;
//            }
//            // 敵の順番
//            PutOpponentGrid();
//        }
//        else
//        {
//            Debug.Log("そこには配置できません。");

//            // TODO 印が配置できない Grid なので、配置できないメッセージをゲーム画面に表示する
//        }
//    }

//    /// <summary>
//    /// Gridにオーナーシンボル(◯×)をセット
//    /// </summary>
//    /// <param name="targetGrid"></param>
//    /// <param name="setOwnerType"></param>
//    private void SetOwnerTypeOnGrid(GridController targetGrid, GridOwnerType setOwnerType)
//    {
//        targetGrid.UpdateGridData(setOwnerType, setOwnerType == GridOwnerType.Player ? "○" : "×");

//        //勝敗結果を判定
//        JudgeWinner();
//    }

//    private void JudgeWinner()
//    {
//        GridOwnerType winner = GridOwnerType.None;

//        //○又は×が縦,横,斜めに３つ揃っているかを判定する
//        //横
//        for (int i = 1; i < 3; i++)
//        {
//            int gridOwnerTypeNo = i;

//            for (int x = 0; x < 2; x++)
//            {
//                if (generateGrids[x * 3].CurrentGridOwnerType == (GridOwnerType)gridOwnerTypeNo
//                    && generateGrids[x * 3 + 1].CurrentGridOwnerType == (GridOwnerType)gridOwnerTypeNo
//                    && generateGrids[x * 3 + 2].CurrentGridOwnerType == (GridOwnerType)gridOwnerTypeNo)
//                {
//                    winner = (GridOwnerType)gridOwnerTypeNo;
//                    break;
//                }
//            }

//            if (winner != GridOwnerType.None)
//            {
//                ShowResult(winner);
//                return;
//            }
//        }

//        //縦
//        for (int i = 1; i < 3; i++)
//        {
//            int gridOwnerTypeNo = i;

//            for (int x = 0; x < 2; x++)
//            {
//                if (generateGrids[x].CurrentGridOwnerType == (GridOwnerType)gridOwnerTypeNo
//                    && generateGrids[x + 3].CurrentGridOwnerType == (GridOwnerType)gridOwnerTypeNo
//                    && generateGrids[x + 6].CurrentGridOwnerType == (GridOwnerType)gridOwnerTypeNo)
//                {
//                    winner = (GridOwnerType)gridOwnerTypeNo;
//                    break;
//                }
//            }

//            if (winner != GridOwnerType.None)
//            {
//                ShowResult(winner);
//                return;
//            }

//        }

//        //斜め
//        for (int i = 1; i < 3; i++)
//        {

//            int gridOwnerTypeNo = i;

//            for (int x = -1; x < 1; x++)
//            {
//                if (generateGrids[0 - x * 2].CurrentGridOwnerType == (GridOwnerType)gridOwnerTypeNo
//                && generateGrids[4].CurrentGridOwnerType == (GridOwnerType)gridOwnerTypeNo
//                && generateGrids[8 + x * 2].CurrentGridOwnerType == (GridOwnerType)gridOwnerTypeNo)
//                {
//                    winner = (GridOwnerType)gridOwnerTypeNo;
//                    break;
//                }
//            }

//            // 勝者が確定している場合
//            if (winner != GridOwnerType.None)
//            {
//                ShowResult(winner);
//                return;
//            }

//        }
//    }
//    /// <summary>
//    /// 結果発表
//    /// </summary>
//    /// <param name="winner"></param>
//    private void ShowResult(GridOwnerType winner)
//    {
//        Debug.Log("勝者 : " + winner);

//        isGameUp = true;

//        for(int i = 0;i < generateGrids.Length; i++)
//        {
//            generateGrids[i].BtnGrid.interactable = false;
//        }
//        // TODO リスタート用のボタンを押せる状態にする

//        // TODO 勝者の種類に応じて処理を３つに分岐し、勝利・敗北・引き分けの画面表示と勝利数のカウントアップ
//    }

//    /// <summary>
//    /// 敵の順番(×が置けるか確認してから置く)
//    /// </summary>
//    private void PutOpponentGrid()
//    {
//        while (!isGameUp)
//        {
//            // ランダムなGridの番号を選択
//            int randomPieceIndex = Random.Range(0, generateGrids.Length);

//            // そのGridにオーナーシンボルがなければ×を設置
//            if (generateGrids[randomPieceIndex].CurrentGridOwnerType == GridOwnerType.None)
//            {
//                SetOwnerTypeOnGrid(generateGrids[randomPieceIndex], GridOwnerType.Opponent);
//                break;
//            }
//        }
//    }


//}