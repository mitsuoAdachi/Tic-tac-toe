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

    [SerializeField]
    private Info_View infoView;

    [SerializeField]
    private Info_Model infoMode;

    [SerializeField]
    private Button btnRestart;

    [SerializeField]
    private Grid_View[] gridViews;

    void Start()
    {
        // ☆①　戻り値になっているメソッドから配列を受け取るように修正しま
        gridViews = mainGame.InitialSettings();


        // リスタートボタンの設定
        btnRestart.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(1.0f))
            .Subscribe(_ => mainGame.Restart()).AddTo(gameObject);


        // InfoView の設定Info_Model内のReactivePropertyであるInfoMessageの購読を行い、値が更新された際の処理の設定を行う
        infoMode.InfoMessage.Subscribe(x => infoView.UpdateDispayInfo(x)).AddTo(gameObject);


        // ゲーム終了状態の監視
        mainGame.IsGameUp.Subscribe(x => {

            // リスタート用のボタンを押せる状態にする
            btnRestart.interactable = x;// TODO リスタート用のボタンを押せる状態にする

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

        //Grid の購読を行う
        for (int i = 0; i < mainGame.gridModelList.Count; i++)
        {
            int index = i;

            // 各 Grid ボタンの購読を行い、クリック(タップ)入力を受付
            gridViews[index].GetGridButton().OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(1.0f))
                .Select(_ => mainGame.gridModelList[index].GridNo)　　// Subscribe の引数用にストリームの情報を gridNo に置き換える 
                .Subscribe(x => mainGame.OnClickGrid(x))              // x は gridNo
                .AddTo(this);

            // 各 Grid_Model のオーナー情報を購読し、更新された際には画面表示を更新する
            mainGame.gridModelList[index].CurrentGridOwnerType
                .Subscribe(x => gridViews[index].UpdateGridOwnerSymbol(x == GridOwnerType.Player ? "〇" : x == GridOwnerType.Opponent ? "×" : string.Empty))
                .AddTo(this);
        }


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