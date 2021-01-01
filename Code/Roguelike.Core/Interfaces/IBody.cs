using System.Collections.Generic;

using Roguelike.Core.ActiveObjects;

namespace Roguelike.Core.Interfaces
{
	public interface IBody
	{
		IAlive Owner
		{ get; }

		ICollection<BodyPart> Parts
		{ get; }

		double Weight
		{ get; }
	}
}
