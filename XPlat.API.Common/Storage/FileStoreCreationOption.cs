namespace XPlat.API.Storage
{
    public enum FileStoreCreationOption
    {
        GenerateUniqueName,

        ReplaceExisting,

        FailIfExists,

        OpenIfExists
    }
}