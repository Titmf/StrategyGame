using UnityEngine;

namespace Hex
{
    public class Borders : MonoBehaviour
    {
        [SerializeField] private HexGrid _hexGrid;
        
        private const int ZOffset = 1;
        private HexCell[] _borderCells;
        private void Awake() => _hexGrid.OnCreateBorders += CreateBorderCells;
        private void OnDestroy() => _hexGrid.OnCreateBorders -= CreateBorderCells;
        private void CreateBorderCells(HexCell hexCell, int cellCountX, int cellCountZ)
        {
            var borderCellCount = 2 * (cellCountX + cellCountZ);
            _borderCells = new HexCell[borderCellCount];
		
            for (var i = 0; i < borderCellCount ; i++)
            {
                //down
                for (var x = - 1; x < (cellCountX); x++) CreateBorderCell(hexCell, x, -cellCountZ + ZOffset, i++);
			    //up
                for (var x = 0; x < (cellCountX); x++) CreateBorderCell(hexCell, x, ZOffset, i++);
                //right
                for (var z = + 1; z < cellCountZ; z++) CreateBorderCell(hexCell, cellCountX, -z + ZOffset, i++);
			    //left
                for (var z = 0; z < cellCountZ; z++) CreateBorderCell(hexCell, -1, -z + ZOffset, i++);
            }
        }
        private void CreateBorderCell(HexCell cellPrefab ,int x, int z, int i)
        {
            Vector3 position;
            position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
            position.y = -90f;
            position.z = z * (HexMetrics.outerRadius * 1.5f);

            HexCell cell = _borderCells[i] = Instantiate(cellPrefab, transform);
            cell.transform.localPosition = position;
            cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        }
    }
}