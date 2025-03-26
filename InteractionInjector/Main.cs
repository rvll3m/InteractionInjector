using System;
using System.Reflection;
using MonoPatcherLib;
using simbouquet.InteractionInjector.Patches;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.CAS;
using Sims3.SimIFace;

//Template Created by Battery

namespace simbouquet.InteractionInjector
{
	[Plugin(false)]
	public class Main
	{
		public Main()
        {
			if (IsNRaasRelationshipPanelInstalled()) MonoPatcher.PatchAll();
			else
			{
				Type simDescriptionOrig = typeof(SimDescription);
				MethodInfo onPickFromPanelOrig = simDescriptionOrig.GetMethod("OnPickFromPanel");
				Type simDescriptionPatch = typeof(SimDescription_Patch);
				MethodInfo onPickFromPanelPatch = simDescriptionPatch.GetMethod("OnPickFromPanel");
				MonoPatcher.ReplaceMethod(onPickFromPanelOrig, onPickFromPanelPatch);

				Type msdOrig = typeof(MiniSimDescription);
				MethodInfo msdOnPickFromPanelOrig = msdOrig.GetMethod("OnPickFromPanel");
				Type msdPatch = typeof(MiniSimDescription_Patch);
				MethodInfo msdOnPickFromPanelPatch = msdPatch.GetMethod("OnPickFromPanel");
				MonoPatcher.ReplaceMethod(msdOnPickFromPanelOrig, msdOnPickFromPanelPatch);
			}
		}
		public static bool IsNRaasRelationshipPanelInstalled()
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (assembly.FullName.Contains("NRaasRelationshipPanel")) return true;
			}
			return false;
		}
	}
}