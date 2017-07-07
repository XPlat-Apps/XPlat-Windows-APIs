namespace XPlat.Storage
{
    /// <summary>
    /// Specifies what to do if a file or folder with the specified name already exists in the current folder when you copy, move, or rename a file or folder.
    /// </summary>
    public enum NameCollisionOption
    {
        GenerateUniqueName,

        ReplaceExisting,

        FailIfExists
    }
}