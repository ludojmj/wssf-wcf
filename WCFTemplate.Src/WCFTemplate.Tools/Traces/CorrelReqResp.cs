using System;
using System.Diagnostics;

namespace WCFTemplate.Tools.Traces
{
    public class CorrelReqResp
    {
        public Uri Url { get; set; }
        public string Name { get; set; }
        public string AppClient { get; set; }
        public string WsClient { get; set; }
        public string Id { get; set; }
        public Stopwatch Chrono { get; set; }
        public string Request { get; set; }
    }
}
