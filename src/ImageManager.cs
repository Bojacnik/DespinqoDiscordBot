namespace DespinqoDiscordBot;

public abstract class ImageManager
{
    public abstract (string, Stream) Get(string directory, int id);
    public abstract int GetCount(string directory);
}

public class ImageManagerImpl : ImageManager
{
    private readonly Dictionary<string, List<(string, Stream)>?> _fileStreams = new();
    
    public ImageManagerImpl(string path)
    {
        LoadImagesToMemory(path);
    }
    
    public override (string, Stream) Get(string directory, int id)
    {
        var selectedStreams = _fileStreams[directory] ;
        if (selectedStreams == null)
            throw new Exception("No such directory like \"" + directory + "\" was found");
        
        if (id < 0)
            throw new Exception("Cannot have id lesser than zero!");
        if (id >= selectedStreams.Count)
            throw new Exception("Cannot have id greater than count-1!");
        
        return selectedStreams[id];
    }

    public override int GetCount(string directory)
    {
        var selectedStreams = _fileStreams[directory];
        return selectedStreams?.Count ?? 0;
    }
    
    private void LoadImagesToMemory(string path)
    {
        foreach (var directory in Directory.GetDirectories(path))
        {
            var fileList = new List<(string, Stream)>();
            foreach (var file in Directory.GetFiles(directory))
            {
                fileList.Add(
                    (
                        file, 
                        new FileStream(file, FileMode.Open, FileAccess.Read)
                    )
                );
            }
            _fileStreams.Add(directory, fileList);
        }
    }
}
