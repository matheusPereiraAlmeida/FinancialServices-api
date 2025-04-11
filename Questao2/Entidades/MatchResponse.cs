namespace Questao2.Entidades
{
    public class MatchResponse
    {
        public int Page { get; set; }
        public int Per_Page { get; set; }
        public int Total { get; set; }
        public int Total_Pages { get; set; }
        public List<MatchData> Data { get; set; }
    }
}
