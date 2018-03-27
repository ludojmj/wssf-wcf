using System;
using System.ServiceModel.Configuration;

namespace WCFTemplate.Tools.Errors
{
    // Handling exceptions
    public class ErrorHandlerElement : BehaviorExtensionElement
    {
        private readonly Type _behaviorType = typeof(ErrorHandler);

        protected override object CreateBehavior()
        {
            return new ErrorHandler();
        }

        public override Type BehaviorType
        {
            get { return _behaviorType; }
        }
    }
}
