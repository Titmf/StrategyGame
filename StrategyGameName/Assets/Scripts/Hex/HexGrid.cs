using System;

using Hex;

using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	public int chunkCountX, chunkCountZ;
	public HexGridChunk chunkPrefab;
	public HexCell cellPrefab;
	public Color defaultColor = Color.white;
	
	private int _cellCountX, _cellCountZ;
	private HexGridChunk[] _chunks;
	private HexCell[] _cells;
	private HexCell[] _borderCells;
	
	public event Action<HexCell, int, int> OnCreateBorders; 
	private void Start () {
		_cellCountX = chunkCountX * HexMetrics.chunkSizeX;
		_cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;

		CreateChunks();
		CreateCells();
		OnCreateBorders?.Invoke(cellPrefab, _cellCountX, _cellCountZ);
	}
	private void CreateChunks () {
		_chunks = new HexGridChunk[chunkCountX * chunkCountZ];

		for (int z = 0, i = 0; z < chunkCountZ; z++) {
			for (int x = 0; x < chunkCountX; x++) {
				HexGridChunk chunk = _chunks[i++] = Instantiate(chunkPrefab);
				chunk.transform.SetParent(transform);
			}
		}
	}
	private void CreateCells()
	{
		_cells = new HexCell[_cellCountZ  * _cellCountX];

		for (int z = 0, i = 0; z < _cellCountZ; z++)
		{
			for (var x = 0; x < _cellCountX; x++) CreateCell(x, z, i++);
		}
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
		var chunkX = x / HexMetrics.chunkSizeX;
		var chunkZ = z / HexMetrics.chunkSizeZ;
		HexGridChunk chunk = _chunks[chunkX + chunkZ * chunkCountX];
		
		var localX = x - chunkX * HexMetrics.chunkSizeX;
		var localZ = z - chunkZ * HexMetrics.chunkSizeZ;
		chunk.AddCell(localX + localZ * HexMetrics.chunkSizeX, cell);
	}
}