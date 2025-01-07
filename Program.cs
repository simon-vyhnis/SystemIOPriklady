using System.IO.Compression;

Console.WriteLine("Příklady Sytem IO");

IterateDirectory(@"C:\Users\simon\pg\io\io1");
CopyDirectory(@"C:\Users\simon\pg\io\io1", @"C:\Users\simon\pg\io\io2", true);
ZipDirectory(@"C:\Users\simon\pg\io\io1", @"C:\Users\simon\pg\io\io1.zip");

static void IterateDirectory(string docPath)
{
    try
    {
        List<string> dirs = new List<string>(Directory.EnumerateDirectories(docPath));
        List<string> files = new List<string>(Directory.EnumerateFiles(docPath));

        Console.WriteLine($"{dirs.Count} directories found.");
        foreach (var dir in dirs)
        {
            Console.WriteLine($"{ dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1)}");
        }

        Console.WriteLine();
        Console.WriteLine($"{files.Count} files found.");
        foreach (var file in files)
        {
            Console.WriteLine($"{file.Substring(file.LastIndexOf(Path.DirectorySeparatorChar) + 1)}");
        }
    }
    catch (UnauthorizedAccessException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (PathTooLongException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
{
    // Get information about the source directory
    var dir = new DirectoryInfo(sourceDir);

    // Check if the source directory exists
    if (!dir.Exists)
        throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

    // Cache directories before we start copying
    DirectoryInfo[] dirs = dir.GetDirectories();

    // Create the destination directory
    Directory.CreateDirectory(destinationDir);

    // Get the files in the source directory and copy to the destination directory
    foreach (FileInfo file in dir.GetFiles())
    {
        string targetFilePath = Path.Combine(destinationDir, file.Name);
        file.CopyTo(targetFilePath);
    }

    // If recursive and copying subdirectories, recursively call this method
    if (recursive)
    {
        foreach (DirectoryInfo subDir in dirs)
        {
            string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
            CopyDirectory(subDir.FullName, newDestinationDir, true);
        }
    }
}

static void ZipDirectory(string startPath, string zipPath)
{
    ZipFile.CreateFromDirectory(startPath, zipPath);
}