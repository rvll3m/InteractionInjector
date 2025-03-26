using System;
using System.Collections.Generic;
using System.Text;
using MonoPatcherLib;
using Sims3.Gameplay.Core;
using Sims3.Gameplay.Interactions;

namespace simbouquet.InteractionInjector.Patches
{
    public class Mailbox_Patch
    {
        public static event AddCheatInteractionsDelegate AddCheatInteractionsEvent_Mailbox;

        [ReplaceMethod(typeof(Mailbox), "AddMailboxCheatInteractions")]
        public static void AddMailboxCheatInteractions(List<InteractionDefinition> cheatInteractions)
        {
            AddCheatInteractionsEvent_Mailbox?.Invoke(cheatInteractions);
            cheatInteractions.Add(Cheats.MakeAllHappy.Singleton);
            cheatInteractions.Add(Cheats.MakeFriendsForMe.Singleton);
            cheatInteractions.Add(Cheats.MakeMeKnowEveryone.Singleton);
            cheatInteractions.Add(Cheats.ForceNPC.Singleton);
            cheatInteractions.Add(Cheats.SetCareer.Singleton);
            cheatInteractions.Add(Cheats.ToggleMotiveDecay.Singleton);
            cheatInteractions.Add(Cheats.ForceVisitor.Singleton);
            cheatInteractions.Add(Cheats.AddSupernaturalSimsToWorld.Singleton);
        }
    }
}
