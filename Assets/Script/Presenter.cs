using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class Presenter : MonoBehaviour
{
    [SerializeField]
    private MainGame mainGame;

    [SerializeField]
    private Result_View[] resultViews;

    [SerializeField]
    private Result_Model[] resultModels;


    void Start()
    {
        // TODO ゲームの初期設定を行う。後程修正し、Grid_View の情報を配列で受け取る
        mainGame.InitialSettings();


        // TODO リスタートボタンの設定


        // TODO InfoView の設定


        // ゲーム終了状態の監視
        mainGame.IsGameUp.Subscribe(x => {

            // TODO リスタート用のボタンを押せる状態にする

            PrepareResult(mainGame.winner);
        }).AddTo(gameObject);

        // ResultView と ResultModel の設定
        SetUpResult();


        /// <summary>
        /// ResultView と ResultModel の設定するローカル関数
        /// </summary>
        void SetUpResult()
        {

            // Model => View
            // Result_Model の RectiveDictionary を購読して、Result_View を更新
            for (int i = 0; i < resultViews.Length; i++)
            {

                // Subscribe の値が Length に固定されてしまい、登録は成功するが、更新時に失敗するので、1回値を受ける
                int a = i;

                // Result_Model の RectiveProperty を購読し、加算結果に合わせて画面を更新
                resultModels[a].WinCount.Subscribe(x => resultViews[a].UpdateDisplayWincount(x)).AddTo(gameObject);

                // GridOwnerType の設定と勝利数の初期化
                resultModels[a].SetUpResultModel((GridOwnerType)a + 1);

                Debug.Log("勝利数 購読開始 : " + resultModels[a].CurrentGridOwnerType.ToString());


                // Result_Model の RectiveProperty を購読し、勝敗結果に合わせて画面を更新
                resultModels[a].ResultMessage.Subscribe(x => resultViews[a].UpdateDisplayResultGame(x)).AddTo(gameObject);

                // 勝敗表示の初期化
                resultModels[a].InitResultMessage();

                Debug.Log("勝敗結果 購読開始 : " + resultModels[a].CurrentGridOwnerType.ToString());
            }
        }

        // 勝利数の初期化、ゲーム情報の初期化
        mainGame.ResetGameParameters();

        // TODO Grid の購読を行う

    }

    /// <summary>
    /// リザルトの準備
    /// </summary>
    /// <param name="winner"></param>
    public void PrepareResult(GridOwnerType winner)
    {

        // 結果表示
        for (int i = 0; i < resultModels.Length; i++)
        {
            resultModels[i].ShowResult(winner);
        }
    }
}