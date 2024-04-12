using UnityEngine;

namespace Hex
{
    public class HexGridChunk : MonoBehaviour {

        HexCell[] cells;

        private void Awake () {
            cells = new HexCell[HexMetrics.chunkSizeX * HexMetrics.chunkSizeZ];
        }
        public void AddCell (int index, HexCell cell) {
            cells[index] = cell;
            cell.transform.SetParent(transform, false);
        }
    }
}