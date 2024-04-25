using System;

using UnityEngine;

namespace Hex
{
	[System.Serializable]
	public struct HexCoordinates {
		public bool Equals(HexCoordinates other)
		{
			return x == other.x && z == other.z;
		}
		public override bool Equals(object obj)
		{
			return obj is HexCoordinates other && Equals(other);
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(x, z);
		}

		[SerializeField]
		private int x, z;
		public int X => x;
		public int Z => z;
		public HexCoordinates (int x, int z) {
			this.x = x;
			this.z = z;
		}
		public static HexCoordinates FromOffsetCoordinates (int x, int z) => new(x - z / 2, z);
		public HexCoordinates DirectionToHex(int currentDirectionIndex)
		{
			return currentDirectionIndex switch
			{
				0 => new HexCoordinates(1, 0),
				1 => new HexCoordinates(1, -1),
				2 => new HexCoordinates(0, -1),
				3 => new HexCoordinates(-1, 0),
				4 => new HexCoordinates(-1, 1),
				5 => new HexCoordinates(0, 1),
				_ => throw new ArgumentOutOfRangeException(nameof(currentDirectionIndex), currentDirectionIndex, "Direction index out of range wtf")
			};
		}
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
		public static HexCoordinates operator +(HexCoordinates a, HexCoordinates b) {
			return new HexCoordinates(a.x + b.x, a.z + b.z);
		}
		public static HexCoordinates operator -(HexCoordinates a, HexCoordinates b) {
			return new HexCoordinates(a.x - b.x, a.z - b.z);
		}
		public static HexCoordinates operator *(HexCoordinates a, int b) {
			return new HexCoordinates(a.x * b, a.z * b);
		}
		public static bool operator ==(HexCoordinates a, HexCoordinates b) {
			return a.x == b.x && a.z == b.z;
		}
		public static bool operator !=(HexCoordinates a, HexCoordinates b) {
			return a.x != b.x || a.z != b.z;
		}
	}
}