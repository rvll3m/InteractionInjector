using System;
using System.Collections.Generic;
using System.Text;
using MonoPatcherLib;
using Sims3.Gameplay;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.ActorSystems;
using Sims3.Gameplay.Autonomy;
using Sims3.Gameplay.CAS;
using Sims3.Gameplay.CelebritySystem;
using Sims3.Gameplay.Controllers;
using Sims3.Gameplay.Core;
using Sims3.Gameplay.Interactions;
using Sims3.Gameplay.Interfaces;
using Sims3.Gameplay.Socializing;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;

namespace simbouquet.InteractionInjector.Patches
{
    public delegate void OnPickFromPanelDelegate(UIMouseEventArgs eventArgs, GameObjectHit gameObjectHit, List<InteractionObjectPair> list, IGameObject targetObject, SimDescription targetSim);

    public class SimDescription_Patch
    {
        public static event OnPickFromPanelDelegate OnPickFromPanelEvent;
        public static event OnPickFromPanelDelegate OnPickFromPanelEvent_Phone;

        [ReplaceMethod(typeof(SimDescription), "OnPickFromPanel")]
        public void OnPickFromPanel(UIMouseEventArgs eventArgs, GameObjectHit gameObjectHit)
        {
            var desc = (SimDescription)(this as object);
            Sims3.Gameplay.UI.PieMenu.ClearInteractions();
            Sims3.Gameplay.UI.PieMenu.HidePieMenuSimHead = true;
            Sims3.UI.Hud.PieMenu.sIncrementalButtonIndexing = true;
            Sim activeActor = Sim.ActiveActor;
            if (activeActor != null)
            {
                if (activeActor.InteractionQueue.CanPlayerQueue())
                {
                    List<InteractionObjectPair> list = new List<InteractionObjectPair>();
                    //if (desc.IsHuman)
                    //{
                    //    if (desc.CreatedSim != null )
                    //    {
                    //        if (!desc.IsDroppingOut && !GameStates.IsEarlyDepartureSim(desc.SimDescriptionId))
                    //        {
                    //            list.Add(new InteractionObjectPair(CallOver.Singleton, desc.CreatedSim));
                    //        }
                    //    }
                    //    bool flag = desc.Household != null && desc.Household.IsAlienHousehold;
                    //    if (GameUtils.IsInstalled(ProductVersion.EP8) && !flag)
                    //    {
                    //        InteractionDefinition interactionDefinition = new Mailbox.WriteLoveLetter.Definition(desc.SimDescriptionId);
                    //        list.Add(new InteractionObjectPair(interactionDefinition, activeActor));
                    //    }
                    //    if (GameUtils.IsInstalled(ProductVersion.EP10))
                    //    {
                    //        OccultMermaid.SignalMermaid.Definition definition = new OccultMermaid.SignalMermaid.Definition(desc);
                    //        list.Add(new InteractionObjectPair(definition, activeActor));
                    //    }
                    //    IPhone phone = activeActor.Inventory.Find<IPhone>();
                    //    if (phone != null)
                    //    {
                    //        list.Add(new InteractionObjectPair(phone.GetCallChatFromRelationPanelDefinition(desc), phone));
                    //        if (GameUtils.IsInstalled(ProductVersion.EP4))
                    //        {
                    //            list.Add(new InteractionObjectPair(phone.GetCallPrank(desc), phone));
                    //        }
                    //        if (GameUtils.IsInstalled(ProductVersion.EP9))
                    //        {
                    //            list.Add(new InteractionObjectPair(phone.GetSendChatTextFromRelationPanelDefinition(desc), phone));
                    //            list.Add(new InteractionObjectPair(phone.GetSendInsultTextFromRelationPanelDefinition(desc), phone));
                    //            list.Add(new InteractionObjectPair(phone.GetSendPictureTextFromRelationPanelDefinition(desc), phone));
                    //            list.Add(new InteractionObjectPair(phone.GetSendSecretAdmirerTextFromRelationPanelDefinition(desc), phone));
                    //            list.Add(new InteractionObjectPair(phone.GetSendBreakUpTextFromRelationPanelDefinition(desc), phone));
                    //            list.Add(new InteractionObjectPair(phone.GetSendWooHootyTextFromRelationPanelDefinition(desc), phone));
                    //        }
                    //        if (!desc.IsEnrolledInBoardingSchool() && !desc.IsDroppingOut && !GameStates.IsEarlyDepartureSim(desc.SimDescriptionId))
                    //        {
                    //            list.Add(new InteractionObjectPair(phone.GetCallInviteOverFromRelationPanelDefinition(desc, true), phone));
                    //            list.Add(new InteractionObjectPair(phone.GetCallInviteToLotFromRelationPanelDefintion(desc), phone));
                    //            list.Add(new InteractionObjectPair(phone.GetCallAskOutOnDateFromRelationPanelDefintion(desc), phone));
                    //            if (!flag)
                    //            {
                    //                list.Add(new InteractionObjectPair(phone.GetCallInviteHouseholdOverFromRelationshipPanelDefiniton(desc), phone));
                    //            }
                    //            list.Add(new InteractionObjectPair(phone.GetCallInviteToAttendGraduationFromRelationPanelDefinition(desc), phone));
                    //        }
                    //        else
                    //        {
                    //            list.Add(new InteractionObjectPair(phone.GetRemoveFromBoardingSchool(desc), phone));
                    //        }
                    //    }
                    //}
                    if (desc.CreatedSim != null)
                    {

                        OnPickFromPanelEvent?.Invoke(eventArgs, gameObjectHit, list, desc.CreatedSim, desc);

                        if (desc.IsHuman)
                        {
                            if (!desc.IsDroppingOut && !GameStates.IsEarlyDepartureSim(desc.SimDescriptionId))
                            {
                                list.Add(new InteractionObjectPair(CallOver.Singleton, desc.CreatedSim));
                            }
                            bool flag = desc.Household != null && desc.Household.IsAlienHousehold;
                            if (GameUtils.IsInstalled(ProductVersion.EP8) && !flag)
                            {
                                InteractionDefinition interactionDefinition = new Mailbox.WriteLoveLetter.Definition(desc.SimDescriptionId);
                                list.Add(new InteractionObjectPair(interactionDefinition, activeActor));
                            }
                            if (GameUtils.IsInstalled(ProductVersion.EP10))
                            {
                                OccultMermaid.SignalMermaid.Definition definition = new OccultMermaid.SignalMermaid.Definition(desc);
                                list.Add(new InteractionObjectPair(definition, activeActor));
                            }
                            IPhone phone = activeActor.Inventory.Find<IPhone>();
                            if (phone != null)
                            {
                                OnPickFromPanelEvent_Phone?.Invoke(eventArgs, gameObjectHit, list, phone, desc);

                                list.Add(new InteractionObjectPair(phone.GetCallChatFromRelationPanelDefinition(desc), phone));
                                if (GameUtils.IsInstalled(ProductVersion.EP4))
                                {
                                    list.Add(new InteractionObjectPair(phone.GetCallPrank(desc), phone));
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
                                if (!desc.IsEnrolledInBoardingSchool() && !desc.IsDroppingOut && !GameStates.IsEarlyDepartureSim(desc.SimDescriptionId))
                                {
                                    list.Add(new InteractionObjectPair(phone.GetCallInviteOverFromRelationPanelDefinition(desc, true), phone));
                                    list.Add(new InteractionObjectPair(phone.GetCallInviteToLotFromRelationPanelDefintion(desc), phone));
                                    list.Add(new InteractionObjectPair(phone.GetCallAskOutOnDateFromRelationPanelDefintion(desc), phone));
                                    if (!flag)
                                    {
                                        list.Add(new InteractionObjectPair(phone.GetCallInviteHouseholdOverFromRelationshipPanelDefiniton(desc), phone));
                                    }
                                    list.Add(new InteractionObjectPair(phone.GetCallInviteToAttendGraduationFromRelationPanelDefinition(desc), phone));
                                }
                                else
                                {
                                    list.Add(new InteractionObjectPair(phone.GetRemoveFromBoardingSchool(desc), phone));
                                }
                            }
                        }
                        else if (desc.IsPet)
                        {
                            if (desc.CreatedSim != null)
                            {
                                list.Add(new InteractionObjectPair(Sim.CallPet.Singleton, desc.CreatedSim));
                            }
                            IPhone phone2 = activeActor.Inventory.Find<IPhone>();
                            if (phone2 != null)
                            {
                                list.Add(new InteractionObjectPair(phone2.GetCallBringPetOverFromRelationshipPanelDefinition(desc), phone2));
                            }
                        }
                    }

                    if (CelebrityManager.CanSocialize(activeActor.SimDescription, desc))
                    {
                        Sims3.Gameplay.UI.PieMenu.TestAndBringUpPieMenu(desc.CreatedSim, eventArgs, gameObjectHit, list, InteractionMenuTypes.Normal);
                        return;
                    }
                    GreyedOutTooltipCallback greyedOutTooltipCallback = InteractionInstance.CreateTooltipCallback(Localization.LocalizeString("Gameplay/CelebritySystem/CelebrityManager:NotAbleToSocialize", new object[0]));
                    Sims3.Gameplay.UI.PieMenu.TestAndBringUpPieMenu(desc.CreatedSim, eventArgs, gameObjectHit, list, InteractionMenuTypes.Normal, "Gameplay/Abstracts/GameObject:NoInteractions", false, greyedOutTooltipCallback);
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
