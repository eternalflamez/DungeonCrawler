//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Spell_ElectricBall : Spell
	{
		public Spell_ElectricBall ()
			: base(1, 5, 30, "Wijs naar het scherm.", 15)
		{
			this.visuals = Resources.Load<GameObject>("energyBall");
			this.particle = new SpellParticle(visuals, this.spellDistance, 15);
		}
	}
}

