using UnityEngine;
using UnityEngine.UI;

public class Info_View : MonoBehaviour
{
    [SerializeField]
    private Text txtInfo;

    /// <summary>
    /// インフォ表示の更新
    /// </summary>
    /// <param name="message"></param>
    public void UpdateDispayInfo(string message)
    {
        txtInfo.text = message;
    }
}