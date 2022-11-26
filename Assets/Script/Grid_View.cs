using UnityEngine;
using UnityEngine.UI;

public class Grid_View : MonoBehaviour
{
    [SerializeField]
    private Text txtGridOwnerSymbol;

    [SerializeField]
    private Button btnGrid;


    /// <summary>
    /// オーナーシンボル(○×)の表示更新
    /// </summary>
    /// <param name="ownerSymbol"></param>
    public void UpdateGridOwnerSymbol(string ownerSymbol)
    {
        txtGridOwnerSymbol.text = ownerSymbol;
    }

    /// <summary>
    /// ボタンの取得
    /// </summary>
    /// <returns></returns>
    public Button GetGridButton()
    {
        return btnGrid;
    }
}