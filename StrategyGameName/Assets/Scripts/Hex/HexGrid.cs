﻿using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	public int width = 6;
	public int height = 6;

	public Color defaultColor = Color.white;

	public HexCell cellPrefab;

	private HexCell[] _cells;
	
	private void Awake () {

		_cells = new HexCell[height * width];

		for (int z = 0, i = 0; z < height; z++) {
			for (var x = 0; x < width; x++) {
				CreateCell(x, z, i++);
			}
		}
	}

	public void ColorCell (Vector3 position, Color color) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
		HexCell cell = _cells[index];
		cell.color = color;
	}

	private void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = _cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
		cell.color = defaultColor;
	}
}