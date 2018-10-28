using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridInventoryData")]
public class So_GIData : ScriptableObject
{

	public float CellSize = 32;
    public float CellSpace = 32;
    public Color CellDefaultColor = Color.gray;
    
}
