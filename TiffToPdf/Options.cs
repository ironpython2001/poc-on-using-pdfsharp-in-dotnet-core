using CommandLine;

public class Options
{
    [Option(longName: "tiff", Required = true, HelpText = "tiff file name is required")]
    public string TiffFileName { get; set; }
}
