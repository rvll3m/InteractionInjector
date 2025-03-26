using System;
using System.Collections.Generic;
using System.Text;
using MonoPatcherLib;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.Autonomy;
using Sims3.Gameplay.CAS;
using Sims3.Gameplay.CelebritySystem;
using Sims3.Gameplay.Controllers;
using Sims3.Gameplay.Core;
using Sims3.Gameplay.Interactions;
using Sims3.Gameplay.Interfaces;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;

namespace simbouquet.InteractionInjector.Patches
{
    public delegate void OnPickFromPanelDelegate_MSD(UIMouseEventArgs eventArgs, GameObjectHit gameObjectHit, List<InteractionObjectPair> list, IGameObject targetObject, MiniSimDescription targetSim);

    public class MiniSimDescription_Patch
    {
        public static event OnPickFromPanelDelegate_MSD OnPickFromPanelEvent_MSD;
        public static event OnPickFromPanelDelegate_MSD OnPickFromPanelEvent_MSD_Phone;

        [ReplaceMethod(typeof(MiniSimDescription), "OnPickFromPanel")]
        public void OnPickFromPanel(UIMouseEventArgs eventArgs, GameObjectHit gameObjectHit)
        {
            var desc = (MiniSimDescription)(this as object);
            Sims3.Gameplay.UI.PieMenu.ClearInteractions();
            Sims3.Gameplay.UI.PieMenu.HidePieMenuSimHead = true;
            Sims3.UI.Hud.PieMenu.sIncrementalButtonIndexing = true;
            Sim activeActor = Sim.ActiveActor;
            if (activeActor != null)
            {
                if (activeActor.InteractionQueue.CanPlayerQueue())
                {
                    List<InteractionObjectPair> list = new List<InteractionObjectPair>();
                    OnPickFromPanelEvent_MSD?.Invoke(eventArgs, gameObjectHit, list, activeActor, desc);
                    if (GameUtils.IsInstalled(ProductVersion.EP8))
                    {
                        InteractionDefinition interactionDefinition = new Mailbox.WriteLoveLetter.Definition(desc.SimDescriptionId);
                        list.Add(new InteractionObjectPair(interactionDefinition, activeActor));
                    }
                    IPhone phone = activeActor.Inventory.Find<IPhone>();
                    if (phone != null)
                    {
                        OnPickFromPanelEvent_MSD_Phone?.Invoke(eventArgs, gameObjectHit, list, phone, desc);
                        list.Add(new InteractionObjectPair(phone.GetCallInviteOverForeignVisitorsFromRelationPanelDefinition(desc), phone));
                        list.Add(new InteractionObjectPair(phone.GetCallChatFromRelationPanelDefinition(desc), phone));
                        list.Add(new InteractionObjectPair(phone.GetCallInviteToAttendGraduationFromRelationPanelDefinition(desc), phone));
                    }
                    if (GameUtils.IsInstalled(ProductVersion.EP9))
                    {
                        list.Add(new InteractionObjectPair(phone.GetSendChatTextFromRelationPanelDefinition(desc), phone));
                        list.Add(new InteractionObjectPair(phone.GetSendInsultTextFromRelationPanelDefinition(desc), phone));
                        list.Add(new InteractionObjectPair(phone.GetSendPictureTextFromRelationPanelDefinition(desc), phone));
                        list.Add(new InteractionObjectPair(phone.GetSendSecretAdmirerTextFromRelationPanelDefinition(desc), phone));
                        list.Add(new InteractionObjectPair(phone.GetSendBreakUpTextFromRelationPanelDefinition(desc), phone));
                        list.Add(new InteractionObjectPair(phone.GetSendWooHootyTextFromRelationPanelDefinition(desc), phone));
                    }
                    if (CelebrityManager.CanSocialize(activeActor.SimDescription, desc))
                    {
                        Sims3.Gameplay.UI.PieMenu.TestAndBringUpPieMenu(null, eventArgs, gameObjectHit, list, InteractionMenuTypes.Normal);
                        return;
                    }
                    GreyedOutTooltipCallback greyedOutTooltipCallback = () => Localization.LocalizeString("Gameplay/CelebritySystem/CelebrityManager:NotAbleToSocialize", new object[0]);
                    Sims3.Gameplay.UI.PieMenu.TestAndBringUpPieMenu(null, eventArgs, gameObjectHit, list, InteractionMenuTypes.Normal, "Gameplay/Abstracts/GameObject:NoInteractions", false, greyedOutTooltipCallback);
                    return;
                }
                else
                {
                    Vector2 vector;
                    if (eventArgs.DestinationWindow != null)
                    {
                        vector = eventArgs.DestinationWindow.WindowToScreen(eventArgs.MousePosition);
                    }
                    else
                    {
                        vector = eventArgs.MousePosition;
                    }
                    Sims3.Gameplay.UI.PieMenu.ShowGreyedOutTooltip(Localization.LocalizeString("Gameplay/Abstracts/GameObject:TooManyInteractions", new object[0]), vector);
                }
            }
        }
    }
}
