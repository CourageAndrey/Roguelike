using System.Collections.Generic;

using Roguelike.Core.Aspects;

namespace Roguelike.Core.Interfaces
{
	public interface IHumanoid : IAlive
	{
		string Name
		{ get; }

		Race Race
		{ get; }

		Mannequin Mannequin
		{ get; }

		Skilled Skilled
		{ get; }

		Rider Rider
		{ get; }

		Profession Profession
		{ get; }

		Interlocutor Interlocutor
		{ get; }

		Appearance Appearance
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
