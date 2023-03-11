using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using System.IO;
using StateSmith.Output.UserConfig;

#nullable enable

namespace StateSmith.Output.Gil.C99;

public class GilToC99 : IGilTranspiler
{
    public readonly StringBuilder hFileSb = new();
    public readonly StringBuilder cFileSb = new();
    public readonly RenderConfigCVars renderConfigC;

    private readonly ICodeFileWriter codeFileWriter;
    private readonly OutputInfo outputInfo;
    private readonly IGilToC99Customizer cCustomizer;

    public GilToC99(RenderConfigCVars renderConfigC, OutputInfo outputInfo, IGilToC99Customizer cCustomizer, ICodeFileWriter codeFileWriter)
    {
        this.renderConfigC = renderConfigC;
        this.outputInfo = outputInfo;
        this.cCustomizer = cCustomizer;
        this.codeFileWriter = codeFileWriter;
    }

    public void TranspileAndOutputCode(string programText)
    {
        //File.WriteAllText($"{outputInfo.outputDirectory}{cNameMangler.SmName}.gil.cs", programText);

        GilHelper.Compile(programText, out CompilationUnitSyntax root, out SemanticModel model);

        C99GenVisitor visitor = new(model, hFileSb, cFileSb, renderConfigC, cCustomizer);

        visitor.Visit(root);

        PostProcessor.PostProcess(hFileSb);
        PostProcessor.PostProcess(cFileSb);

        codeFileWriter.WriteFile($"{outputInfo.outputDirectory}{cCustomizer.MakeHFileName()}", code: hFileSb.ToString());
        codeFileWriter.WriteFile($"{outputInfo.outputDirectory}{cCustomizer.MakeCFileName()}", code: cFileSb.ToString());
    }
}