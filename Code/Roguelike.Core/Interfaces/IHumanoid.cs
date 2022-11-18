using System.Collections.Generic;

using Roguelike.Core.Objects;

namespace Roguelike.Core.Interfaces
{
	public interface IHumanoid : IAlive, IInterlocutor
	{
		string Name
		{ get; }

		Race Race
		{ get; }

		IManequin Manequin
		{ get; }

		IDictionary<Skill, int> Skills
		{ get; }

		IDictionary<Skill, double> SkillExperience
		{ get; }

		Transport Transport
		{ get; set; }
	}
}
