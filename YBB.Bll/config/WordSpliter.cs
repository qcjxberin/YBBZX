using YBB.Bll.ShootSeg;

namespace YBB.Bll.config
{
    public static class WordSpliter
    {

        public static string GetKeyword(string string_0)
        {
            return GetKeyword(string_0, " ");
        }

        public static string GetKeyword(string string_0, string string_1)
        {
            Segment segment = new Segment();
            segment.InitWordDics();
            segment.EnablePrefix = true;
            segment.Separator = string_1;
            return segment.SegmentText(string_0, false).Trim();
        }
    }

}
