using Hex;

using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	public int chunkCountX, chunkCountZ;
	public HexGridChunk chunkPrefab;
	public HexCell cellPrefab;
	public Color defaultColor = Color.white;
	
	private int cellCountX, cellCountZ;
	private HexGridChunk[] chunks;
	private HexCell[] _cells;
	
	private void Awake () {
		cellCountX = chunkCountX * HexMetrics.chunkSizeX;
		cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;

		CreateChunks();
		CreateCells();
	}

	private void CreateChunks () {
		chunks = new HexGridChunk[chunkCountX * chunkCountZ];

		for (int z = 0, i = 0; z < chunkCountZ; z++) {
			for (int x = 0; x < chunkCountX; x++) {
				HexGridChunk chunk = chunks[i++] = Instantiate(chunkPrefab);
				chunk.transform.SetParent(transform);
			}
		}
	}
	private void CreateCells()
	{
		_cells = new HexCell[cellCountZ  * cellCountX];

		for (int z = 0, i = 0; z < cellCountZ; z++)
		{
			for (var x = 0; x < cellCountX; x++)
			{
				CreateCell(x, z, i++);
			}
		}
	}

	public void ColorCell (Vector3 position, Color color) {
		//
	}

	private void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = _cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
		cell.color = defaultColor;
		
		AddCellToChunk(x, z, cell);
	}

	private void AddCellToChunk (int x, int z, HexCell cell) {
		int chunkX = x / HexMetrics.chunkSizeX;
		int chunkZ = z / HexMetrics.chunkSizeZ;
		HexGridChunk chunk = chunks[chunkX + chunkZ * chunkCountX];
		
		int localX = x - chunkX * HexMetrics.chunkSizeX;
		int localZ = z - chunkZ * HexMetrics.chunkSizeZ;
		chunk.AddCell(localX + localZ * HexMetrics.chunkSizeX, cell);
	}
}