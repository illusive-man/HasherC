namespace HasherC
{
    public interface IRunCommand
    {
        string GetHashes(string hasherFileName, string arguments);
    }
}