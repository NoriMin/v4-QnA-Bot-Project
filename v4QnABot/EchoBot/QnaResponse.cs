namespace EchoBot
{
    public class QnaResponse
    {
        public Answer[] answers { get; set; }
    }

    public class Answer
    {
        public string[] questions { get; set; }
        public string answer { get; set; }
        public float score { get; set; }
        public int id { get; set; }
        public string source { get; set;}
        public object[] metadata { get; set; }
    }
}
