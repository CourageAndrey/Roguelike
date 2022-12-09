using System.Collections.Generic;

using Roguelike.Core.ActiveObjects;

namespace Roguelike.Core.Interfaces
{
	public interface IBody : IVariableMassy
	{
		ICollection<BodyPart> Parts
		{ get; }
	}
}
