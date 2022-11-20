using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gridゲームオブジェクト制御用クラス
/// </summary>
public class GridController : MonoBehaviour
{
    [SerializeField]
    private Button btnGrid;
    public Button BtnGrid { get => btnGrid; }

    [SerializeField]
    private Text txtGridOwnerIcon;

    //　デバッグ用。後程、SerializeField 属性は削除
    [SerializeField]
    private GridOwnerType currentGridOwnerType;

    /// <summary>
    /// currentGridOwnerTypeのプロパティ
    /// </summary>
    public GridOwnerType CurrentGridOwnerType
    {
        get => currentGridOwnerType;
        set => currentGridOwnerType = value;
    }

    private int gridNo;

    /// <summary>
    /// Gridの初期設定
    /// </summary>
    /// <param name="no"></param>
    /// <param name="gameManager"></param>
    public void SetUpgrid(int no,MainGame mainGame)
    {
        // Gridの通し番号
        gridNo = no;

        btnGrid.onClick.AddListener(() => mainGame.OnClickGrid(gridNo));

        // Gird の情報更新
        UpdateGridData(GridOwnerType.None, string.Empty);

        Debug.Log($"Gridの設定完了: Gridの通し番号: { no }");
    }

    /// <summary>
    /// Gridの情報更新(オーナーシンボル(○×)のセット、および初期化に利用する)
    /// </summary>
    /// <param name="newGridOwnerType"></param>
    /// <param name="ownerSymbol"></param>
    public void UpdateGridData(GridOwnerType newGridOwnerType,string ownerSymbol)
    {
        currentGridOwnerType = newGridOwnerType;
        txtGridOwnerIcon.text = ownerSymbol;
    }

}
