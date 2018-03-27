using System;
using System.ServiceModel.Configuration;

namespace WCFTemplate.Tools.Traces
{
    // Handling HTTP requests and responses to trace
    public class TraceHandlerElement : BehaviorExtensionElement
    {
        private readonly Type _behaviorType = typeof(TraceHandler);

        protected override object CreateBehavior()
        {
            return new TraceHandler();
        }

        public override Type BehaviorType
        {
            get { return _behaviorType; }
        }
    }
}
