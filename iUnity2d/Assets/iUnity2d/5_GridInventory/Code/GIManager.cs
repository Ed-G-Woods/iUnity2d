using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIManager : MonoBehaviour {
    
    public short GridNumX = 10;
    public short GridNumY = 15;
    public CellState CurrentCell = null;

    [SerializeField] private So_GIData GIData;
    [SerializeField] private GameObject GridCellPerfab;

    private Dictionary<Vector2Int, GameObject> Cells = new Dictionary<Vector2Int, GameObject>();

    private void Awake()
    {
        for (int y=0; y< GridNumY; y++)
        {
            for (int x=0; x< GridNumX; x++)
            {
                Vector2Int CellPos_Cell =new Vector2Int(x,y);
                SpawnCell(CellPos_Cell);
            }
        }
    }
    
    void Start ()
    {
		
	}
	void Update ()
    {
		
	}

    void SpawnCell(Vector2Int cellPosition)
    {
        GameObject GridCell = Instantiate<GameObject>(GridCellPerfab);

        GridCell.transform.SetParent(transform);

        Vector3 localPos = new Vector3(cellPosition.x * GIData.CellSpace, cellPosition.y * GIData.CellSpace, 0);
        GridCell.transform.localPosition = localPos;

        GridCell.name = cellPosition.ToString();

        CellState CCellstate = GridCell.GetComponent<CellState>();
        CCellstate.CellPos = cellPosition;
        CCellstate.SetColor(GIData.CellDefaultColor);
        CCellstate.gridInventoryManager = this;

        RectTransform CellRectTransform = GridCell.GetComponent<RectTransform>();
        CellRectTransform.sizeDelta = new Vector2(GIData.CellSize, GIData.CellSize);

        CellRectTransform.localScale = Vector3.one;

        Cells.Add(cellPosition, GridCell);
    }


    ///~~~~~~~~~~~~
    /// 检查，锁定，解锁,变色 Cell or Cells
    ///~~~~~~~~~~~~

    //检查
    public bool CheckCellsFree(Vector2Int LB_Cell,Vector2Int RT_Cell)
    {
        bool free = true;
        for (int y = LB_Cell.y; y <= RT_Cell.y; y++)
        {
            for (int x = LB_Cell.x; x <= RT_Cell.x; x++)
            {
                Vector2Int cellPos = new Vector2Int(x, y);
                if (!Cells.ContainsKey(cellPos)) { return false; }
                free = Cells[cellPos].GetComponent<CellState>().isFree;
                if (!free){ return false; }
            }
        }

        return true;
    }
    public bool CheckCellFree(Vector2Int CellPos)
    {
        return Cells[CellPos].GetComponent<CellState>().isFree;
    }
    //解锁
    public void FreeCells(Vector2Int LB_Cell, Vector2Int RT_Cell)
    {
        for (int y = LB_Cell.y; y <= RT_Cell.y; y++)
        {
            for (int x = LB_Cell.x; x <= RT_Cell.x; x++)
            {
                Vector2Int cellPos = new Vector2Int(x, y);
                Cells[cellPos].GetComponent<CellState>().isFree = true;
            }
        }
    }
    public void FreeCell(Vector2Int CellPos)
    {
        Cells[CellPos].GetComponent<CellState>().isFree = true;
    }
    //锁定
    public void LockCells(Vector2Int LB_Cell, Vector2Int RT_Cell)
    {
        for (int y = LB_Cell.y; y <= RT_Cell.y; y++)
        {
            for (int x = LB_Cell.x; x <= RT_Cell.x; x++)
            {
                Vector2Int cellPos = new Vector2Int(x, y);
                Cells[cellPos].GetComponent<CellState>().isFree = false;
            }
        }
    }
    public void LockCell(Vector2Int CellPos)
    {
        Cells[CellPos].GetComponent<CellState>().isFree = false;
    }
    //变色
    public void SetCellsColor(Vector2Int LB_Cell, Vector2Int RT_Cell,Color newcolor)
    {
        for (int y = LB_Cell.y; y <= RT_Cell.y; y++)
        {
            for (int x = LB_Cell.x; x <= RT_Cell.x; x++)
            {
                Vector2Int cellPos = new Vector2Int(x, y);
                GameObject cell = null;
                if (x>GridNumX-1||y>GridNumY-1)
                {
                    continue;
                }
                cell = Cells[cellPos];
                if (!cell.GetComponent<CellState>().isFree)
                {
                    continue;
                }
                cell.GetComponent<CellState>().SetColor(newcolor);
                
            }
        }
    }
    public void SetCellColor(Vector2Int CellPos,Color newcolor)
    {
        Cells[CellPos].GetComponent<CellState>().SetColor(newcolor);
    }
    //重置free的cell的 颜色
    public void ResetColorInFreecells()
    {
        foreach (GameObject cell in Cells.Values)
        {
            CellState cstate = cell.GetComponent<CellState>();
            if (cstate.isFree)
            {
                cstate.SetColor(GIData.CellDefaultColor);
            }
        }
    }



    //获取真实的Cell大小，像素
    public float getRealCellSize()
    {
        Vector2Int a = new Vector2Int(0, 0);
        Vector2Int b = new Vector2Int(1, 0);
        float realSize = Cells[b].transform.position.x - Cells[a].transform.position.x;
        return realSize;
    }
}
