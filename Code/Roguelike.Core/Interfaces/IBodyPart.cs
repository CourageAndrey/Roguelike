using System.Collections.Generic;

using Roguelike.Core.Aspects;

namespace Roguelike.Core.Interfaces
{
	public interface IBodyPart : IVariableMassy
	{
		Body Body
		{ get; }

		IReadOnlyCollection<IBodyPart> Parts
		{ get; }

		bool IsVital
		{ get; }
	}
}
