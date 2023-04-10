namespace PhotoAuto
{
    public static class Helper
    {
        public static string[] files_help(string folder)
        {
            var dir = folder;
            var files = Directory.GetFiles(dir);
            return files;
        }
    }
}