namespace Common
{
    public static class Config
    {
        public static string DatabasePath { get; } = "/Users/emil_/OneDrive/Dokumenter/Skole/Software-development/development_of_large_systems/projects/searchengine/SearchEngine-Baseline/database.db";
        public static string DataSourcePath { get; } = "/Users/emil_/OneDrive/Dokumenter/Skole/Software-development/development_of_large_systems/projects/EnronMini";
        public static int NumberOfFoldersToIndex { get; } = 0; // Use 0 or less for indexing all folders
    }
}