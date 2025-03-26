using System;
using System.Collections.Generic;
using System.Text;
using MonoPatcherLib;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.CelebritySystem;
using Sims3.Gameplay.Core;
using Sims3.Gameplay.Interactions;
using Sims3.SimIFace;

namespace simbouquet.InteractionInjector.Patches
{
    public delegate void AddCheatInteractionsDelegate(List<InteractionDefinition> list);

    public class Sim_Patch
    {
        public static event AddCheatInteractionsDelegate AddCheatInteractionsEvent_Sim;

        [ReplaceMethod(typeof(Sim), "AddSimCheatInteractions")]
        public static void AddSimCheatInteractions(List<InteractionDefinition> cheatInteractions)
        {
            AddCheatInteractionsEvent_Sim?.Invoke(cheatInteractions);
            cheatInteractions.Add(Cheats.ModifyTraits.Singleton);
            cheatInteractions.Add(Cheats.AddToFamily.Singleton);
            cheatInteractions.Add(Cheats.TriggerAgeTransition.Singleton);
            cheatInteractions.Add(Cheats.SetFavoriteMusicType.Singleton);
        }
    }
}
