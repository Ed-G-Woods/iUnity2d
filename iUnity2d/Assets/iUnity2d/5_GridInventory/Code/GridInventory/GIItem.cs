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

    private bool isMoving;
    private List<Vector2Int> CellsOccupied = new List<Vector2Int>();
    
    private RectTransform _ItemRect;
    private UnityEngine.UI.Image _ItemImage;

    private InventoryManager _InventoryManager=null;//用来判断item的inventory（是不是从一个_Inventory移到了另一个_Inventory？）

    private GIManager _GIManager;

    private void Awake()
    {
        _ItemRect = gameObject.GetComponent<RectTransform>();

        _ItemImage = gameObject.GetComponent<UnityEngine.UI.Image>();
        
        _GIManager = GIManager.GgetGIManager;
        if (_GIManager ==null)
        {
            GameObject g = GameObject.Find("GridInventoryManager");
            if (g == null)
            {
                g = GameObject.FindGameObjectWithTag("GIManager");
            }
            _GIManager = g.GetComponent<GIManager>();
        }
    }

    void Start () {
        _ItemRect.sizeDelta = new Vector2(ItemSizeX * GIData.CellSpace, ItemSizeY * GIData.CellSpace);
    }
	void Update () {
        if (isMoving)
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
            _GIManager._GISelected.isHaveSelected = false;  //全局isHaveSelected
            isMoving = false;   //本地isMoving
            _ItemImage.raycastTarget = true;    //设置遮挡
            
            transform.SetParent(_GIManager._InventroryManager.transform);
            BetweenInventory();

            //set最后结束的位置
            Vector3 NewItemPosition = _GIManager._GridManager.CurrentCell.transform.position;
            float realsize = _GIManager._GridManager.getRealCellSize();
            NewItemPosition.x += (realsize * ItemSizeX / 2) - (realsize / 2);
            NewItemPosition.y += (realsize * ItemSizeY / 2) - (realsize / 2);
            transform.position = NewItemPosition;
           //-set最后结束的位置

            OccupyCell(true);

            transform.SetAsFirstSibling();//渲染顺序（显示前后)
        }
    }
    //开始移动Inventory，由OnPointerClick触发
    public void StartMove()
    {
        if (_GIManager._GISelected.isHaveSelected) return; //全局isHaveSelected 是否已经有在移动的物体了

        if (CellsOccupied.Count !=0)
        {
            OccupyCell(false);
        }
        
        transform.SetParent(_GIManager._GISelected.transform);

        _GIManager._GISelected.isHaveSelected = true;   //全局isHaveSelected
        isMoving = true;    //本地isMoving
        _ItemImage.raycastTarget = false;   ////设置遮挡

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

    void BetweenInventory()
    {
        bool isSameInv = _InventoryManager == GIManager.GgetGIManager._InventroryManager;
        if (!isSameInv)
        {
            
            string namebefore = _InventoryManager == null ? "Null" : _InventoryManager.ToString();
            string nameafter = GIManager.GgetGIManager._InventroryManager == null ? "Null" : GIManager.GgetGIManager._InventroryManager.ToString();
            Debug.Log("Move from " + namebefore+" to " + nameafter+" .");

            _InventoryManager = GIManager.GgetGIManager._InventroryManager;
        }
    }

}
