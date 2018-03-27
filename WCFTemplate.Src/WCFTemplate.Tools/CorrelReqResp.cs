using System;
using System.Diagnostics;

namespace WCFTemplate.Tools
{
    public class CorrelReqResp
    {
        public Guid Id { get; set; }
        public Stopwatch Chrono { get; set; }
        public int ElapsedTime { get; set; }
        public string ElapsedTimeStr { get; set; }
        public string AppClient { get; set; }
        public string WsClient { get; set; }
        public string Operation { get; set; }
        public string WebServiceMethod { get; set; }
        public string Requete { get; set; }
        public string Reponse { get; set; }
        public string Status { get; set; }
    }
}

