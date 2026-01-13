using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace Bannerlord.FluidCombatNext.Helper
{
    public static class MessageHelper
    {
        //Because the way ButrLib registers the provider - just easier to use a static
        public static ILogger? Log { get; set; }

        public static void HandleWarning(string text)
        {
            InformationManager.DisplayMessage(new InformationMessage(text, Colors.Yellow));
            Log.LogWarning(text);
        }

        public static void HandleError(string text, string exception)
        {
            InformationManager.DisplayMessage(new InformationMessage(text, Colors.Red));
            Log.LogError($"{text}: {exception}");
        }

        public static void HandleSuccess(string text)
        {
            InformationManager.DisplayMessage(new InformationMessage(text, Colors.Green));
            Log.LogInformation(text);
        }

        public static void HandleTrace(string text)
        {
            Log.LogTrace(text);
        }
    }
}
