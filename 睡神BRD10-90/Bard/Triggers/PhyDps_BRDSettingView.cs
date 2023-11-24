using CombatRoutine.View;
using Common.GUI;
using Common.Language;
using ImGuiNET;

namespace Morpheus.Bard;

public class PhyDpsBRDSettingView : ISettingUI
{
    public string Name => "诗人";

    public void Draw()
    {
        /*ImGui.Checkbox(Language.Instance.ToggleWildFireFirst,
            ref MCHSettings.Instance.WildfireFirst);

        ImGuiHelper.LeftInputInt(Language.Instance.InputStrongGcdCheckTime,
            ref MCHSettings.Instance.StrongGCDCheckTime, 1000, 10000, 1000);*/

        if (ImGui.Button("保存设置")) BRDSettings.Instance.Save();
    }
}