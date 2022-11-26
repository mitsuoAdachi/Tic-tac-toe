using UnityEngine;
using UniRx;

public class Info_Model : MonoBehaviour
{
    public ReactiveProperty<string> InfoMessage = new ReactiveProperty<string>();

    /// <summary>
    /// 値の更新
    /// </summary>
    /// <param name="message"></param>
    public void UpdateInfoMessage(string message)
    {
        InfoMessage.Value = message;
    }
}