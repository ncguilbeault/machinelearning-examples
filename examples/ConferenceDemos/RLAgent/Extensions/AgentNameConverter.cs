using Bonsai;
using Bonsai.Expressions;
using System.Linq;
using System.ComponentModel;

class AgentNameConverter : StringConverter
{
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
        return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
        if (context != null)
        {
            var workflowBuilder = (WorkflowBuilder)context.GetService(typeof(WorkflowBuilder));
            if (workflowBuilder != null)
            {
                var agentNames = (from builder in workflowBuilder.Workflow.Descendants()
                                    where builder.GetType() != typeof(DisableBuilder)
                                    let createAgent = ExpressionBuilder.GetWorkflowElement(builder) as CreateRLAgent
                                    where createAgent != null && !string.IsNullOrEmpty(createAgent.Name)
                                    select createAgent.Name)
                                    .Distinct()
                                    .ToList();
                if (agentNames.Count > 0)
                {
                    return new StandardValuesCollection(agentNames);
                }
            }
        }

        return new StandardValuesCollection(new string[] { });
    }
}