using UnityEngine;
using UniRx;

public class Result_Model : MonoBehaviour
{
    private GridOwnerType currentGridOwnerType;

    /// <summary>
    /// プロパティ
    /// </summary>
    public GridOwnerType CurrentGridOwnerType { get => currentGridOwnerType; set => currentGridOwnerType = value; }

    public ReactiveProperty<int> WinCount = new ReactiveProperty<int>();

    public ReactiveProperty<string> ResultMessage = new ReactiveProperty<string>();


    /// <summary>
    /// Result_Model の初期設定
    /// </summary>
    public void SetUpResultModel(GridOwnerType gridOwnerType)
    {
        WinCount.Value = 0;
        CurrentGridOwnerType = gridOwnerType;
    }

    /// <summary>
    /// 勝敗表示の初期化
    /// </summary>
    public void InitResultMessage()
    {
        ResultMessage.Value = string.Empty;
    }

    /// <summary>
    /// 結果発表
    /// </summary>
    /// <param name="winner"></param>
    public void ShowResult(GridOwnerType winner)
    {

        if (winner == currentGridOwnerType)
        {
            ResultMessage.Value = "Win!";
            WinCount.Value++;
        }
        else if (winner == GridOwnerType.Draw)
        {
            ResultMessage.Value = "Draw";
        }
        else if (winner == GridOwnerType.None)
        {
            ResultMessage.Value = string.Empty;
        }
        else
        {
            ResultMessage.Value = "Lose...";
        }
    }
}