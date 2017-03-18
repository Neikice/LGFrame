using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[ExecuteInEditMode]
public class ResizeGridLayoutGroupCell : MonoBehaviour {
    public int RowCount;

	// Use this for initialization
	void Start () {
        this.SetGridHeight();
	}

#if UNITY_EDITOR
    void Update () {
        this.SetGridHeight();
    }

#endif
    public void SetGridHeight()
    {
        this.SetGridHeight(this.RowCount);
    }

    public void SetGridHeight(int num)     //每行Cell的个数
    {
        var grid = this.GetComponent<GridLayoutGroup>();
        var rectTransform = this.GetComponent<RectTransform>();

        float childCount = this.transform.childCount;  //获得Layout Group子物体个数
        var spacing  = grid.spacing;
        float cellwith = rectTransform.rect.width / this.RowCount - grid.spacing.x;
        float ratio = grid.cellSize.y / grid.cellSize.x;  //长宽比
        float cellheight = cellwith * ratio;
        float delta = cellheight/grid.cellSize.y;           //变化系数

        grid.cellSize = new Vector2(cellwith, cellheight);
        grid.spacing = this.multiDelta(grid.spacing, delta);
    }

    Vector2 multiDelta(Vector2 value,float delta)
    {
        float x = value.x * delta;
        float y = value.y * delta;
        return new Vector2(x, y);
    }


}
