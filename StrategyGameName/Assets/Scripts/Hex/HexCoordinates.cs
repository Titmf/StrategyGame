using UnityEngine;

[System.Serializable]
public struct HexCoordinates {

	[SerializeField]
	private int x, z;
	public int X => x;
	public int Z => z;
	public HexCoordinates (int x, int z) {
		this.x = x;
		this.z = z;
	}
	public static HexCoordinates FromOffsetCoordinates (int x, int z) => new(x - z / 2, z);
	public static Vector3 ToPosition(HexCoordinates other) {
		var innerRadius = HexMetrics.innerRadius;
		var outerRadius = HexMetrics.outerRadius;

		var horizontalSpacing = innerRadius * 2f;
		var verticalSpacing = outerRadius * 1.5f;

		var xPosition = other.X * horizontalSpacing + other.Z * horizontalSpacing * 0.5f;
		var zPosition = other.Z * verticalSpacing;

		var yPosition = 0; 

		return new Vector3(xPosition, yPosition, zPosition);
	}
}