using System;

namespace Roguelike.Core
{
	public class Vector : IEquatable<Vector>
	{
		#region Properties

		public int X
		{ get; }

		public int Y
		{ get; }

		public int Z
		{ get; }

		#endregion

		public Vector(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public bool Equals(Vector other)
		{
			return	X == other.X &&
					Y == other.Y &&
					Z == other.Z;
		}

		public override string ToString()
		{
			return $"({X}, {Y}, {Z})";
		}

		#region Mathematics

		public double GetDistanceSquare(Vector other)
		{
			int	dx = X - other.X,
				dy = Y - other.Y,
				dz = Z - other.Z;
			return dx * dx + dy * dy + dz * dz;
		}

		public double GetDistance(Vector other)
		{
			return Math.Sqrt(GetDistanceSquare(other));
		}

		public bool IsNeighboor(Vector other)
		{
			return	Math.Abs(X - other.X) <= 1 &&
					Math.Abs(Y - other.Y) <= 1 &&
					Math.Abs(Z - other.Z) <= 1;
		}

		public Direction GetDirection(Vector other)
		{
#warning 2D-only directions are taken into account.
			if (X > other.X)
			{
				if (Y > other.Y)
				{
					return Direction.DownLeft;
				}
				else if (Y < other.Y)
				{
					return Direction.UpLeft;
				}
				else // Y == other.Y
				{
					return Direction.Left;
				}
			}
			else if (X < other.X)
			{
				if (Y > other.Y)
				{
					return Direction.DownRight;
				}
				else if (Y < other.Y)
				{
					return Direction.UpRight;
				}
				else // Y == other.Y
				{
					return Direction.Right;
				}
			}
			else // X == other.X
			{
				if (Y > other.Y)
				{
					return Direction.Down;
				}
				else if (Y < other.Y)
				{
					return Direction.Up;
				}
				else // Y == other.Y
				{
					return Direction.None;
				}
			}
		}

		public Vector GetNeighboor(Direction direction)
		{
			int x = X;
			int y = Y;
			int z = Z;

			if ((direction & Direction.Left) > 0)
			{
				x--;
			}
			if ((direction & Direction.Right) > 0)
			{
				x++;
			}
			if ((direction & Direction.Up) > 0)
			{
				y++;
			}
			if ((direction & Direction.Down) > 0)
			{
				y--;
			}

			return new Vector(x, y, z);
		}

		#endregion
	}
}
