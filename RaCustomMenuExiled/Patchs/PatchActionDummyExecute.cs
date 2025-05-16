using System;
using System.Collections.Generic;
using CommandSystem;
using CommandSystem.Commands.RemoteAdmin.Dummies;
using Exiled.API.Features;
using HarmonyLib;
using NetworkManagerUtils.Dummies;
using RemoteAdmin.Communication;
using Utils;

namespace RaCustomMenuExiled.Patchs;

[HarmonyPatch(typeof(ActionDummyCommand), nameof(ActionDummyCommand.Execute))]
public static class PatchActionDummyExecute
{
    [HarmonyPatch, HarmonyPrefix]
    public static bool Prefix(RaDummyActions __instance, ArraySegment<string> arguments, ICommandSender sender, out string response, ref bool __result)
    {
        if (!sender.CheckPermission(PlayerPermissions.FacilityManagement, out response))
        {
            return false;
        }
        if (arguments.Count < 3)
        {
            response = "You must specify all arguments! (target, module, action)";
            return false;
        }
        string[] array;
        List<ReferenceHub> list = RAUtils.ProcessPlayerIdOrNamesList(arguments, 0, out array, false);
        if (list == null)
        {
            response = "An unexpected problem has occurred during PlayerId or name array processing.";
            return false;
        }
        string text = arguments.At(1);
        string text2 = arguments.At(2);
        int numPlayer = 0;
        int numDummy = 0;
        response = "";
        foreach (ReferenceHub referenceHub in list)
        {
            if (!(referenceHub == null))
            {
                List<DummyAction> list2 = DummyActionCollector.ServerGetActions(referenceHub);
                bool flag = false;
                foreach (DummyAction dummyAction in list2)
                {
                    string text3 = dummyAction.Name.Replace(' ', '_');
                    if (dummyAction.Action == null)
                    {
                        flag = (text3 == text);
                    }
                    else if (flag && text3 == text2)
                    {
                        dummyAction.Action.Invoke();
                        if (referenceHub.IsDummy)
                        {
                            numDummy++;
                        }else if (!referenceHub.IsDummy)
                        {
                            numPlayer++;
                        }
                    }
                }
            }
        }

        if (numPlayer == 0 && numDummy == 0)
        {
            response = "Action requested on 0 user";
        }else if (numPlayer > 0 && numDummy == 0)
        {
            response = string.Format("Action requested on {0} play{1}", numPlayer, (numPlayer == 1) ? "er!" : "ers!");
        }else if (numPlayer == 0 && numDummy > 0)
        {
            response = string.Format("Action requested on {0} dumm{1}", numDummy, (numDummy == 1) ? "y!" : "ies!");
        }else if (numPlayer > 0 && numDummy > 0)
        {
            response = string.Format("Action requested on {0} dumm{1} and on {2} play{3}", numDummy, (numDummy == 1) ? "y" : "ies", numPlayer, (numPlayer == 1) ? "er!" : "ers!");
        }

        __result = true;
        return false;
    }
}