using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IHumanoid : IAlive, IInterlocutor
	{
		string Name
		{ get; }

		IManequin Manequin
		{ get; }

		IDictionary<Skill, int> Skills
		{ get; }

		IDictionary<Skill, double> SkillExperience
		{ get; }

		ITransport Transport
		{ get; set; }
	}
}
