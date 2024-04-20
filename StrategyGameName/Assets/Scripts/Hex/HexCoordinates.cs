using ECS.Data;

using UnityEngine;

[System.Serializable]
public struct HexCoordinates {

	[SerializeField]
	private int x, z;

	public int X => x;
	public int Z => z;
	public int S => -X - Z;

	public HexCoordinates (int x, int z) {
		this.x = x;
		this.z = z;
	}

	public static HexCoordinates FromOffsetCoordinates (int x, int z) => new(x - z / 2, z);

	public static HexCoordinates FromPosition (Vector3 position) {
		var x = position.x / (HexMetrics.innerRadius * 2f);
		var y = -x;

		var offset = position.z / (HexMetrics.outerRadius * 3f);
		x -= offset;
		y -= offset;

		var iX = Mathf.RoundToInt(x);
		var iY = Mathf.RoundToInt(y);
		var iZ = Mathf.RoundToInt(-x -y);

		if (iX + iY + iZ != 0) {
			var dX = Mathf.Abs(x - iX);
			var dY = Mathf.Abs(y - iY);
			var dZ = Mathf.Abs(-x -y - iZ);

			if (dX > dY && dX > dZ)
				iX = -iY - iZ;
			else if (dZ > dY) iZ = -iX - iY;
		}

		return new HexCoordinates(iX, iZ);
	}
	
	public static Vector3 ToPosition(HexCoordinates other) {
		float innerRadius = HexMetrics.innerRadius;
		float outerRadius = HexMetrics.outerRadius;

		float horizontalSpacing = innerRadius * 2f;
		float verticalSpacing = outerRadius * 1.5f;

		float xPosition = other.X * horizontalSpacing + other.Z * horizontalSpacing * 0.5f;
		float zPosition = other.Z * verticalSpacing;

		float yPosition = 0; 

		return new Vector3(xPosition, yPosition, zPosition);
	}
}