using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IBodyPart : IRequireGravitation
	{
		IBody Body
		{ get; }

		IReadOnlyList<IBodyPart> Parts
		{ get; }

		bool IsVital
		{ get; }
	}
}
