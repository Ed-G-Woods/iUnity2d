using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GInventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public So_GIData GIData;

    public string ItemName;
    public short ItemSizeX;
    public short ItemSizeY;

    public Color DefaultColor=Color.white;
    public Color HighlightColor=Color.yellow;
    public Color PressColor=Color.blue;

    [SerializeField] private GIManager _GIManager;

    private RectTransform _ItemRect;
    private UnityEngine.UI.Image _ItemImage;

    private bool isSelect;
    private List<Vector2Int> CellsOccupied = new List<Vector2Int>();

    private void Awake()
    {
        _ItemRect = gameObject.GetComponent<RectTransform>();
        _ItemRect.sizeDelta = new Vector2(ItemSizeX * GIData.CellSpace, ItemSizeY * GIData.CellSpace);

        _ItemImage = gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    void Start () {

	}
	void Update () {
        if (isSelect)
        {
            MoveItem();
            //             if (Input.GetMouseButtonDown(0))
            //             {
            //                 FinishMove();
            //             }

            _GIManager.ResetColorInFreecells();

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
        if (_GIManager.CurrentCell == null)
        {
            return;
        }

        Vector2Int CurrentCellPos_LB = _GIManager.CurrentCell.GetComponent<CellState>().CellPos;
        Vector2Int CellPos_RT = new Vector2Int(CurrentCellPos_LB.x + ItemSizeX - 1, CurrentCellPos_LB.y + ItemSizeY - 1);

        _GIManager.SetCellsColor(CurrentCellPos_LB, CellPos_RT, newcolor);
    }
    //检查图片下的Cells是否可用
    public bool CheakCanFinish()
    {
        bool canFinish = false;
        if (_GIManager.CurrentCell==null)
        {
            return false;
        }

        Vector2Int CurrentCellPos_LB = _GIManager.CurrentCell.GetComponent<CellState>().CellPos;
        Vector2Int CellPos_RT = new Vector2Int(CurrentCellPos_LB.x + ItemSizeX - 1, CurrentCellPos_LB.y + ItemSizeY - 1);

        canFinish = _GIManager.CheckCellsFree(CurrentCellPos_LB, CellPos_RT);

        return canFinish;
    }
    //
    public void FinishMove()
    {
        if (CheakCanFinish())       
        {
            isSelect = false;
            _ItemImage.raycastTarget = true;

            Vector3 NewItemPosition = _GIManager.CurrentCell.transform.position;
            float realsize = _GIManager.getRealCellSize();
            NewItemPosition.x += (realsize * ItemSizeX / 2) - (realsize / 2);
            NewItemPosition.y += (realsize * ItemSizeY / 2) - (realsize / 2);

            transform.position = NewItemPosition;

            OccupyCell(true);
        }
    }
    //
    public void StartMove()
    {
        if (CellsOccupied.Count !=0)
        {
            OccupyCell(false);
        }

        isSelect = !isSelect;
        _ItemImage.raycastTarget = false;
    }
    void OccupyCell(bool occupy)
    {
        if (occupy)
        {
            Vector2Int CurrentCellPos_LB = _GIManager.CurrentCell.GetComponent<CellState>().CellPos;
            Vector2Int CellPos_RT = new Vector2Int(CurrentCellPos_LB.x + ItemSizeX - 1, CurrentCellPos_LB.y + ItemSizeY - 1);
            
            for (int y = CurrentCellPos_LB.y; y <= CellPos_RT.y; y++)
            {
                for (int x = CurrentCellPos_LB.x; x <= CellPos_RT.x; x++)
                {
                    Vector2Int cellPos = new Vector2Int(x, y);
                    CellsOccupied.Add(cellPos);
                    _GIManager.LockCell(cellPos);
                }
            }
        }
        else
        {
            foreach(Vector2Int cellsVector in CellsOccupied)
            {
                _GIManager.FreeCell(cellsVector);
            }
            CellsOccupied.Clear();
        }
    }


    void MoveItem()
    {
        Vector3 NewItemPosition = new Vector3(0, 0, 0);
        NewItemPosition.x = Input.mousePosition.x;
        NewItemPosition.y = Input.mousePosition.y;
        //         NewItemPosition.x += _ItemRect.sizeDelta.x / 2 - (So_GIData.CellSpace / 2);
        //         NewItemPosition.y += _ItemRect.sizeDelta.y / 2 - (So_GIData.CellSpace / 2);
        float realsize = _GIManager.getRealCellSize();
        NewItemPosition.x += (realsize * ItemSizeX / 2) - (realsize / 2);
        NewItemPosition.y += (realsize * ItemSizeY / 2) - (realsize / 2);

        transform.position = NewItemPosition;
    }

    

}
