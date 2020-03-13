namespace CoronaVirusLive.Messages
{
    class StatusMessage
    {
        public string Message { get; set; }

        public StatusMessage(string message)
        {
            Message = message;
        }
    }
}
