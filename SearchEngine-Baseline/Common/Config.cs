namespace Common
{
    public static class Config
    {
        public static string DatabasePath { get; } = "/Users/emil_/data-searchengine/database.db";
        public static string DataSourcePath { get; } = "/Users/emil_/data-searchengine/source";
        public static int NumberOfFoldersToIndex { get; } = 0; // Use 0 or less for indexing all folders
    }
}