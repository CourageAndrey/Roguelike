using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IHumanoid : IAlive, IInterlocutor, IManequin
	{
		string Name
		{ get; }

		IDictionary<Skill, int> Skills
		{ get; }

		IDictionary<Skill, double> SkillExperience
		{ get; }
	}
}
