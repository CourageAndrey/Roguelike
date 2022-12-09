using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IBodyPart : IVariableMassy
	{
		IBody Body
		{ get; }

		IReadOnlyCollection<IBodyPart> Parts
		{ get; }

		bool IsVital
		{ get; }
	}
}
