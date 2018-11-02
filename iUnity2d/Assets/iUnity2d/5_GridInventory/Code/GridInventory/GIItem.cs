using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public So_GIData GIData;

    public string ItemName;
    public short ItemSizeX;
    public short ItemSizeY;

    public Color DefaultColor=Color.white;
    public Color HighlightColor=Color.yellow;
    public Color PressColor=Color.blue;

    private bool isSelect;
    private List<Vector2Int> CellsOccupied = new List<Vector2Int>();


    //private GridManager _GridManager;
    private InventoryManager _InventoryManager;

    private RectTransform _ItemRect;
    private UnityEngine.UI.Image _ItemImage;

    private GIManager _GIManager;

    private void Awake()
    {
        _ItemRect = gameObject.GetComponent<RectTransform>();

        _ItemImage = gameObject.GetComponent<UnityEngine.UI.Image>();

        //_GIManager = GameObject.Find("GridInventoryManager").GetComponent<GIManager>();
        GameObject g = GameObject.Find("GridInventoryManager");
        if (g == null)
        {
            g = GameObject.FindGameObjectWithTag("GIManager");
        }
        _GIManager = g.GetComponent<GIManager>();


        _InventoryManager = _GIManager._InventroryManager;
    }

    void Start () {
        _ItemRect.sizeDelta = new Vector2(ItemSizeX * GIData.CellSpace, ItemSizeY * GIData.CellSpace);
    }
	void Update () {
        if (isSelect)
        {
            MoveItem();
            _GIManager._GridManager.ResetColorInFreecells();

            if (CheakCanFinish())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    FinishMove();
                }
                paintCellsColor(Color.blue);
            }
            else
            {
                paintCellsColor(Color.red);
            }

        }
	}

    public void setItemImage(Sprite s)
    {
        _ItemImage.sprite = s;
    }

    void HandleUIEvenet()
    {

    }

    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        _ItemImage.color = HighlightColor;
    }

    public void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        _ItemImage.color = DefaultColor;
    }

    public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        StartMove();
    }
    //改变图片下Cells的颜色
    public void paintCellsColor(Color newcolor)
    {
        if (_GIManager._GridManager.CurrentCell == null)
        {
            return;
        }

        Vector2Int CurrentCellPos_LB = _GIManager._GridManager.CurrentCell.GetComponent<CellState>().CellPos;
        Vector2Int CellPos_RT = new Vector2Int(CurrentCellPos_LB.x + ItemSizeX - 1, CurrentCellPos_LB.y + ItemSizeY - 1);

        _GIManager._GridManager.SetCellsColor(CurrentCellPos_LB, CellPos_RT, newcolor);
    }
    //检查图片下的Cells是否可用
    public bool CheakCanFinish()
    {
        bool canFinish = false;
        if (_GIManager._GridManager.CurrentCell==null)
        {
            return false;
        }

        Vector2Int CurrentCellPos_LB = _GIManager._GridManager.CurrentCell.GetComponent<CellState>().CellPos;
        Vector2Int CellPos_RT = new Vector2Int(CurrentCellPos_LB.x + ItemSizeX - 1, CurrentCellPos_LB.y + ItemSizeY - 1);

        canFinish = _GIManager._GridManager.CheckCellsFree(CurrentCellPos_LB, CellPos_RT);

        return canFinish;
    }
    //完成移动
    public void FinishMove()
    {
        if (/*CheakCanFinish()*/true)       
        {
            _InventoryManager.isHaveSelect = false;
            isSelect = false;
            _ItemImage.raycastTarget = true;

            Vector3 NewItemPosition = _GIManager._GridManager.CurrentCell.transform.position;
            float realsize = _GIManager._GridManager.getRealCellSize();
            NewItemPosition.x += (realsize * ItemSizeX / 2) - (realsize / 2);
            NewItemPosition.y += (realsize * ItemSizeY / 2) - (realsize / 2);

            transform.position = NewItemPosition;

            OccupyCell(true);

            transform.SetAsFirstSibling();//渲染顺序（显示前后)
        }
    }
    //开始移动Inventory，由OnPointerClick触发
    public void StartMove()
    {
        if (_InventoryManager.isHaveSelect) return; //是否已经有在移动的物体了

        if (CellsOccupied.Count !=0)
        {
            OccupyCell(false);
        }

        _InventoryManager.isHaveSelect = true;
        isSelect = true;
        _ItemImage.raycastTarget = false;

        transform.SetAsLastSibling();//渲染顺序（显示前后）
    }
    void OccupyCell(bool occupy)
    {
        if (occupy)
        {
            Vector2Int CurrentCellPos_LB = _GIManager._GridManager.CurrentCell.GetComponent<CellState>().CellPos;
            Vector2Int CellPos_RT = new Vector2Int(CurrentCellPos_LB.x + ItemSizeX - 1, CurrentCellPos_LB.y + ItemSizeY - 1);
            
            for (int y = CurrentCellPos_LB.y; y <= CellPos_RT.y; y++)
            {
                for (int x = CurrentCellPos_LB.x; x <= CellPos_RT.x; x++)
                {
                    Vector2Int cellPos = new Vector2Int(x, y);
                    CellsOccupied.Add(cellPos);
                    _GIManager._GridManager.LockCell(cellPos);
                }
            }
        }
        else
        {
            foreach(Vector2Int cellsVector in CellsOccupied)
            {
                _GIManager._GridManager.FreeCell(cellsVector);
            }
            CellsOccupied.Clear();
        }
    }

    //移动中……
    void MoveItem()
    {
        Vector3 NewItemPosition = new Vector3(0, 0, 0);
        NewItemPosition.x = Input.mousePosition.x;
        NewItemPosition.y = Input.mousePosition.y;
        //         NewItemPosition.x += _ItemRect.sizeDelta.x / 2 - (So_GIData.CellSpace / 2);
        //         NewItemPosition.y += _ItemRect.sizeDelta.y / 2 - (So_GIData.CellSpace / 2);
        float realsize = _GIManager._GridManager.getRealCellSize();
        NewItemPosition.x += (realsize * ItemSizeX / 2) - (realsize / 2);
        NewItemPosition.y += (realsize * ItemSizeY / 2) - (realsize / 2);

        transform.position = NewItemPosition;
    }

    

}
