using System;
using System.Collections.Generic;
using System.Text;
using MonoPatcherLib;
using Sims3.Gameplay.ActiveCareer;
using Sims3.Gameplay.Careers;
using Sims3.Gameplay.Core;
using Sims3.Gameplay.Interactions;

namespace simbouquet.InteractionInjector.Patches
{
    public class Lot_Patch
    {
        public static event AddCheatInteractionsDelegate AddCheatInteractionsEvent_Lot;

        [ReplaceMethod(typeof(Lot), "AddCheatInteractions")]
        public void AddCheatInteractions(List<InteractionDefinition> cheatInteractions)
        {
            AddCheatInteractionsEvent_Lot?.Invoke(cheatInteractions);
            if (ActiveCareer.ActiveCareerInstalled)
            {
                cheatInteractions.Add(SpawnJob.Singleton);
            }
        }
    }
}
