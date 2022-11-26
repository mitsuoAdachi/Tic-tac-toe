using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MainGame : MonoBehaviour
{

    [SerializeField]
    private Transform gridSetTran;

    [SerializeField]
    private Grid_Model gridModelPrefab;

    public List<Grid_Model> gridModelList = new List<Grid_Model>();

    [SerializeField]
    private Info_Model infoModel;

    private int putCount;
    private int gridCount = 9;

    public GridOwnerType winner;

    public ReactiveProperty<bool> IsGameUp = new ReactiveProperty<bool>();

    /// <summary>
    /// ゲームの初期設定
    /// </summary>
    public Grid_View[] InitialSettings()
    {

        List<Grid_View> gridViewList = new List<Grid_View>();

        // Grid の生成
        for (int i = 0; i < gridCount; i++)
        {
            Grid_Model gridModel = Instantiate(gridModelPrefab, gridSetTran, false);　　
            gridModel.SetUpGridModel(i);　　　　　　　　　　　　　　　　　　　　　　
            gridModelList.Add(gridModel);　　　　　　　　　　　　　　　　　　　　　
            gridViewList.Add(gridModel.GetComponent<Grid_View>());　　　　　　　　　　　
        }

        return gridViewList.ToArray();

    }

    /// <summary>
    /// Player が Grid をクリックした際の処理
    /// </summary>
    public void OnClickGrid(int no)
    {

        Debug.Log($"クリック実行 : Grid の通し番号 : { no }");

        if (IsGameUp.Value)
        {
            return;
        }

        // オーナーシンボル(プレイヤーは○印)が置けるか確認
        // 引数で届いている no 変数を配列の要素番号として利用し、クリックした Grid のオーナーシンボルの情報が None であるか判定する
        if (gridModelList[no].CurrentGridOwnerType.Value == GridOwnerType.None)
        {

            // 配置した数をカウント
            putCount++;

            // 画面のインフォ表示(配置できないメッセージ)をリセット
            infoModel.UpdateInfoMessage(string.Empty);

            gridModelList[no].CurrentGridOwnerType.Value = GridOwnerType.Player;

            // 配置した数の判定。全 Grid が埋まる回数おいたら、勝負付かず引き分け
            if (putCount >= 5 && !IsGameUp.Value)
            {
                GameUp(GridOwnerType.Draw);
                return;
            }

            // 敵の順番
            PutOpponentGrid();

        }
        else
        {

            Debug.Log("そこには配置出来ません。");

            //印が配置できないGridなので配置できないメッセージをゲーム画面に表示する
            infoModel.UpdateInfoMessage("そこには配置出来ません。");

        }
    }

    /// <summary>
    /// 勝者がいるか判定
    /// </summary>
    private void JudgeWinner()
    {

        // 勝者はなしの状態で初期値を作成し、勝者がいる場合には、この変数を上書きする
        GridOwnerType winner = GridOwnerType.None;

        // 横列確認
        for (int i = 1; i < 3; i++)
        {

            int gridOwnerTypeNo = i;

            // 0,1,2 || 3,4,5 || 6,7,8
            for (int x = 0; x < 3; x++)
            {
                if (gridModelList[x * 3].CurrentGridOwnerType.Value == (GridOwnerType)gridOwnerTypeNo
                && gridModelList[x * 3 + 1].CurrentGridOwnerType.Value == (GridOwnerType)gridOwnerTypeNo
                && gridModelList[x * 3 + 2].CurrentGridOwnerType.Value == (GridOwnerType)gridOwnerTypeNo)
                {

                    winner = (GridOwnerType)gridOwnerTypeNo;
                    break;
                }
            }

            // 勝者が確定している場合
            if (winner != GridOwnerType.None)
            {
                // 結果発表
                GameUp(winner);
                return;
            }
        }

        // 縦列確認
        for (int i = 0; i < 2; i++)
        {

            int gridOwnerTypeNo = i + 1;

            // 0,3,6 || 1,4,7 || 2,5,8
            for (int x = 0; x < 3; x++)
            {
                if (gridModelList[x].CurrentGridOwnerType.Value == (GridOwnerType)gridOwnerTypeNo
                && gridModelList[x + 3].CurrentGridOwnerType.Value == (GridOwnerType)gridOwnerTypeNo
                && gridModelList[x + 6].CurrentGridOwnerType.Value == (GridOwnerType)gridOwnerTypeNo)
                {

                    winner = (GridOwnerType)gridOwnerTypeNo;
                    break;
                }
            }

            // 勝者が確定している場合
            if (winner != GridOwnerType.None)
            {
                // 結果発表
                GameUp(winner);
                return;
            }
        }

        // 斜め列確認
        for (int i = 0; i < 2; i++)
        {

            int gridOwnerTypeNo = i + 1;

            // 2, 4, 6 ||  0, 4, 8
            for (int x = -1; x < 1; x++)
            {
                if (gridModelList[0 - x * 2].CurrentGridOwnerType.Value == (GridOwnerType)gridOwnerTypeNo
                && gridModelList[4].CurrentGridOwnerType.Value == (GridOwnerType)gridOwnerTypeNo
                && gridModelList[8 + x * 2].CurrentGridOwnerType.Value == (GridOwnerType)gridOwnerTypeNo)
                {

                    winner = (GridOwnerType)gridOwnerTypeNo;
                    break;
                }
            }

            // 勝者が確定している場合
            if (winner != GridOwnerType.None)
            {
                // 結果発表
                GameUp(winner);
                return;
            }
        }
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    /// <param name="winner"></param>
    private void GameUp(GridOwnerType winner)
    {
        Debug.Log("勝者 : " + winner);

        this.winner = winner;
        IsGameUp.Value = true;
    }

    /// <summary>
    /// 敵の順番(×が置けるか確認してから置く)
    /// </summary>
    private void PutOpponentGrid()
    {
        Debug.Log("test1");

        // ReactiveProperty の値を監視
        while (!IsGameUp.Value)
        {
            Debug.Log("test2");

            int randomPieceIndex = Random.Range(0, gridModelList.Count);

            if (gridModelList[randomPieceIndex].CurrentGridOwnerType.Value == GridOwnerType.None)
            {

                // Grid_Model の ReactiveProperty の値を更新
                gridModelList[randomPieceIndex].CurrentGridOwnerType.Value = GridOwnerType.Opponent;

            }
            JudgeWinner();
            break;
        }
    }

    /// <summary>
    /// ゲームに利用する情報の初期化
    /// </summary>
    public void ResetGameParameters()
    {

        winner = GridOwnerType.None;
        IsGameUp.Value = false;

        putCount = 0;
    }

    /// <summary>
    /// 次のゲームを再開する準備
    /// </summary>
    public void Restart()
    {

        for (int i = 0; i < gridModelList.Count; i++)
        {
            gridModelList[i].CurrentGridOwnerType.Value = GridOwnerType.None;
        }

        // ゲームに利用する情報の初期化
        ResetGameParameters();
    }
}
