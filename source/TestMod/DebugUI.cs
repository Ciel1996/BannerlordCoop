using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace CoopTestMod
{
    class DebugUI
    {
        private ClientAgentManager clientAgentManager = ClientAgentManager.Instance();

        public void Update()
        {
            Imgui.BeginMainThreadScope();

            RenderAgentManager();

            Imgui.EndMainThreadScope();
        }

        private void RenderAgentManager()
        {
            Imgui.Begin("Agent manager");

            if (!Imgui.TreeNode("Agents"))
                return;

            Imgui.Columns(3);
            Imgui.Text("ID");
            Imgui.NextColumn();
            Imgui.Text("Local index");
            Imgui.NextColumn();
            Imgui.Text("Agent name");
            Imgui.NextColumn();

            foreach (KeyValuePair<string, int> agentData in clientAgentManager.GetAgentsList())
            {
                Imgui.Text(agentData.Key);
                Imgui.NextColumn();

                Imgui.Text(agentData.Value.ToString());
                Imgui.NextColumn();

                var localAgent = Mission.Current.FindAgentWithIndex(agentData.Value);

                Imgui.Text(localAgent?.Name);
                Imgui.NextColumn();
            }

            Imgui.TreePop();
            
            Imgui.End();
        }
    }
}
