using System;
using MonoPatcherLib;
using System.Collections.Generic;
using System.Text;
using Sims3.Gameplay.Core;
using Sims3.Gameplay.ActiveCareer;
using Sims3.Gameplay.Careers;
using Sims3.Gameplay.Interactions;
using Sims3.Gameplay.Objects.Island;
using Sims3.Gameplay.Seasons;
using Sims3.SimIFace;

namespace simbouquet.InteractionInjector.Patches
{
    public class Terrain_Patch
    {
        public static event AddCheatInteractionsDelegate AddCheatInteractionsEvent_Terrain;

        [ReplaceMethod(typeof(Terrain), "AddCheatInteractions")]
        public void AddCheatInteractions(List<InteractionDefinition> cheatInteractions)
        {
            AddCheatInteractionsEvent_Terrain?.Invoke(cheatInteractions);
            cheatInteractions.Add(Terrain.TeleportMeHere.Singleton);
            cheatInteractions.Add(BuildOnThisLot.DebugSingleton);
            cheatInteractions.Add(BuyOnThisLot.DebugSingleton);
            if (ActiveCareer.ActiveCareerInstalled)
            {
                cheatInteractions.Add(SpawnJob.Singleton);
            }
            if (SeasonsManager.Enabled)
            {
                cheatInteractions.Add(Terrain.DEBUG_ChangeSeason.Singleton);
                cheatInteractions.Add(Terrain.DEBUG_ChangeWeather.Singleton);
                cheatInteractions.Add(Terrain.DEBUG_SetTemperature.Singleton);
                cheatInteractions.Add(Terrain.DEBUG_UnsetTemperature.Singleton);
                cheatInteractions.Add(Terrain.DEBUG_SetSnowLevel.Singleton);
                cheatInteractions.Add(Terrain.DEBUG_ChangeWindIntensity.Singleton);
                cheatInteractions.Add(Terrain.DEBUG_TreeCycle.Singleton);
            }
            if (GameUtils.IsInstalled(ProductVersion.EP10))
            {
                cheatInteractions.Add(Houseboat.DEBUG_Teleport.Singleton);
            }
        }
    }
}
