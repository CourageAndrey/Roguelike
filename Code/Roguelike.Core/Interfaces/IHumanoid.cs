using System.Collections.Generic;

using Roguelike.Core.Aspects;

namespace Roguelike.Core.Interfaces
{
	public interface IHumanoid : IAlive, IInterlocutor, ISkilled, IRider
	{
		string Name
		{ get; }

		Race Race
		{ get; }

		IManequin Manequin
		{ get; }
	}

	public interface ISkilled
	{
		IDictionary<Skill, int> Skills
		{ get; }

		IDictionary<Skill, double> SkillExperience
		{ get; }
	}

	public interface IRider
	{
		Transport Transport
		{ get; set; }
	}
}
