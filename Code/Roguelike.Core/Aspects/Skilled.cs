using System.Collections.Generic;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public class Skilled : IAspect
	{
		#region Properties

		public IDictionary<Skill, int> Skills
		{ get; } = new Dictionary<Skill, int>();

		public IDictionary<Skill, double> SkillExperience
		{ get; } = new Dictionary<Skill, double>();

		#endregion
	}
}
