#nullable enable

namespace StateSmith.Runner;

/// <summary>
/// https://github.com/StateSmith/StateSmith/issues/200
/// </summary>
public class SmDesignDescriberSettings
{
    public bool enabled = false;

    public Sections outputSections = new();

    /// <summary>
    /// Optional. Can be relative (to normal output path) or absolute.
    /// </summary>
    public string? outputDirectory;

    public class Sections
    {
        /// <summary>
        /// Set to true to output before transformations section.
        /// Before transformations is good for when you want to see the original design.
        /// </summary>
        public bool beforeTransformations = true;

        /// <summary>
        /// Set to true to output after transformations section.
        /// After transformations is good for when you want to see what is passed to the code generator.
        /// This is especially useful when you want to understand transformation steps like
        /// TriggerMaps and other custom transformations or optimizations.
        /// </summary>
        public bool afterTransformations = false;
    }
}
