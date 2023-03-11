using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System.Text;
using StateSmith.Output.UserConfig;
using StateSmith.Common;
using Microsoft.CodeAnalysis.Formatting;
using Antlr4.Runtime;

#nullable enable

namespace StateSmith.Output.Gil.CSharp;

public class CSharpGilVisitor : CSharpSyntaxWalker
{
    public readonly StringBuilder sb;
    private readonly RenderConfigCSharpVars renderConfigCSharp;
    private readonly RenderConfigVars renderConfig;

    private SemanticModel SemanticModel => _semanticModel.ThrowIfNull();
    private SemanticModel? _semanticModel;

    public CSharpGilVisitor(StringBuilder sb, RenderConfigCSharpVars renderConfigCSharp, RenderConfigVars renderConfig) : base(SyntaxWalkerDepth.StructuredTrivia)
    {
        this.sb = sb;
        this.renderConfig = renderConfig;
        this.renderConfigCSharp = renderConfigCSharp;
    }

    public void Process()
    {
        // get input gil code and then clear string buffer to hold new result
        string gilCode = sb.ToString();
        sb.Clear();

        sb.AppendLineIfNotBlank(renderConfig.FileTop);
        if (renderConfigCSharp.UseNullable)
            sb.AppendLine($"#nullable enable");

        sb.AppendLineIfNotBlank(renderConfigCSharp.Usings);

        GilHelper.Compile(gilCode, out CompilationUnitSyntax root, out _semanticModel);

        this.Visit(root);

        FormatOutput();
    }

    private void FormatOutput()
    {
        var outputCode = sb.ToString();
        sb.Clear();

        // note: we don't use the regular `NormalizeWhitespace()` as it tightens all code up, and actually messes up some indentation.
        outputCode = Formatter.Format(CSharpSyntaxTree.ParseText(outputCode).GetRoot(), new AdhocWorkspace()).ToFullString();
        sb.Append(outputCode);
    }

    public override void VisitNullableType(NullableTypeSyntax node)
    {
        if (renderConfigCSharp.UseNullable)
        {
            base.VisitNullableType(node);
        }
        else
        {
            Visit(node.ElementType); // this avoids outputting the `?` for a nullable type
            sb.Append(' ');
        }
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        if (GilHelper.IsGilNoEmit(node))
            return;

        var addressableFunc = GilHelper.GetAddresssableFunctionInfo(node, SemanticModel);

        if (addressableFunc != null)
        {
            OutputMethodAsStaticLambda(node, addressableFunc);
        }
        else
        {
            base.VisitMethodDeclaration(node);
        }
    }

    /// <summary>
    /// Why do this? See https://github.com/StateSmith/StateSmith/wiki/Multiple-Language-Support#function-pointers
    /// </summary>
    /// <param name="node"></param>
    /// <param name="addressableFunc"></param>
    private void OutputMethodAsStaticLambda(MethodDeclarationSyntax node, GilHelper.AddressableFunctionInfo addressableFunc)
    {
        VisitLeadingTrivia(node.GetFirstToken());
        foreach (var m in node.Modifiers)
            VisitToken(m);

        sb.Append("readonly ");
        sb.Append(addressableFunc.DelegateSymbol.Name);
        sb.Append(' ');
        sb.Append(node.Identifier);
        sb.Append($" = ");

        Visit(addressableFunc.ParameterListSyntax);

        sb.Append($" => ");
        node.Body.ThrowIfNull().VisitChildNodesAndTokens(this, toSkip: node.Body.CloseBraceToken);
        sb.AppendTokenAndTrivia(node.Body.CloseBraceToken, overrideTokenText: "};");
    }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        if (GilHelper.HandleSpecialGilEmitClasses(node, this)) return;

        var iterableChildSyntaxList = new WalkableChildSyntaxList(this, node.ChildNodesAndTokens());

        iterableChildSyntaxList.VisitUpTo(SyntaxKind.ClassKeyword);

        if (renderConfigCSharp.UsePartialClass)
            sb.Append("partial ");

        iterableChildSyntaxList.VisitUpTo(node.OpenBraceToken, including: true);
        sb.AppendLineIfNotBlank(renderConfigCSharp.ClassCode);  // append class code after open brace token

        iterableChildSyntaxList.VisitRest();
    }

    // to ignore GIL attributes
    public override void VisitAttributeList(AttributeListSyntax node)
    {
        VisitLeadingTrivia(node.GetFirstToken());
    }

    public override void VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        bool done = false;

        done |= GilHelper.HandleGilSpecialInvocations(node, sb);
        done |= GilHelper.HandleGilUnusedVarSpecialInvocation(node, argument =>
        {
            sb.Append(node.GetLeadingTrivia().ToFullString());
            sb.Append($"_ = {argument.ToFullString()}"); // trailing semi-colon is already part of parent ExpressionStatement
        });

        if (!done)
        {
            base.VisitInvocationExpression(node);
        }
    }

    // kinda like: https://sourceroslyn.io/#Microsoft.CodeAnalysis.CSharp/Syntax/InternalSyntax/SyntaxToken.cs,516c0eb61810c3ef,references
    public override void VisitToken(SyntaxToken token)
    {
        this.VisitLeadingTrivia(token);
        sb.Append(token.Text);
        this.VisitTrailingTrivia(token);
    }

    public override void VisitTrivia(SyntaxTrivia trivia)
    {
        sb.Append(trivia.ToString());

        // useful for nullable directives or maybe structured comments
        //if (trivia.HasStructure)
        //{
        //    this.Visit(trivia.GetStructure());
        //}
    }
}