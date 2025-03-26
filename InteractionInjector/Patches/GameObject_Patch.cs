using System;
using System.Collections.Generic;
using System.Text;
using MonoPatcherLib;
using Sims3.Gameplay.Abstracts;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.Autonomy;
using Sims3.Gameplay.Core;
using Sims3.Gameplay.Interactions;
using Sims3.Gameplay.Interfaces;
using Sims3.Gameplay.Objects.Tables;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;

namespace simbouquet.InteractionInjector.Patches
{
    public delegate void AddCheatInteractionsDelegate(List<InteractionDefinition> list);

    public class GameObject_Patch
    {
        public static event AddCheatInteractionsDelegate AddCheatInteractionsEvent;

        //[ReplaceMethod(typeof(GameObject), "AddCheatInteractions")]
        //public void AddCheatInteractions(List<InteractionDefinition> cheatInteractions)
        //{
        //    Cheats.AddGameObjectCheatInteractions(cheatInteractions);
        //    try
        //    {
        //        AddCheatInteractionsEvent?.Invoke(cheatInteractions);
        //    }
        //    catch (Exception e)
        //    {
        //        ScriptErrorWindow.DisplayScriptError(null, e);
        //    }
        //}

    }
}
