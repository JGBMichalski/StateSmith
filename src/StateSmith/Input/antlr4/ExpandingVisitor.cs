using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using StateSmith.Input.Expansions;
using StateSmith.Output.Gil;

namespace StateSmith.Input.Antlr4;

public class ExpandingVisitor : StateSmithLabelGrammarBaseVisitor<string>
{
    public Expander expander;

    public ExpandingVisitor(Expander expander)
    {
        this.expander = expander;
    }

    public override string VisitTerminal(ITerminalNode node)
    {
        if (node.Symbol != null)
        {
            return node.Symbol.Text;
        }
        return "";
    }

    public override string VisitExpandable_identifier([NotNull] StateSmithLabelGrammarParser.Expandable_identifierContext context)
    {
        string result = context.ohs()?.GetText() ?? "";
        string identifier = context.IDENTIFIER().GetText();
        identifier = expander.TryExpandVariableExpansion(identifier);
        result += identifier;

        return result;
    }

    public override string VisitExpandable_function_call([NotNull] StateSmithLabelGrammarParser.Expandable_function_callContext context)
    {
        var result = context.ohs()?.Accept(this) ?? "";

        var functionName = context.IDENTIFIER().GetText();

        if (functionName == GilCreationHelper.GilExpansionMarkerFuncName)
        {
            string code = context.braced_function_args().function_args().GetText();
            result += WrappingExpander.HandleGilFunctionCode(code);
        }
        else if (expander.HasFunctionName(functionName))
        {
            result = ExpandFunctionCall(context, result, functionName);
        }
        else
        {
            result += functionName;
            result += context.braced_function_args().Accept(this);
        }

        return result;
    }

    private string ExpandFunctionCall(StateSmithLabelGrammarParser.Expandable_function_callContext context, string result, string functionName)
    {
        //We can't just visit the `function_args` rule because it includes commas and additional white space.
        //We need to manually visit each `function_arg_code` rule
        var functionArgContexts = context.braced_function_args().function_args()?.function_arg();

        var stringArgs = new string[functionArgContexts?.Length ?? 0];

        for (int i = 0; i < stringArgs.Length; i++)
        {
            var argContext = functionArgContexts[i];
            stringArgs[i] = argContext.function_arg_code().Accept(this);
        }

        var expandedCode = expander.TryExpandFunctionExpansion(functionName, stringArgs);
        result += expandedCode;
        return result;
    }

    protected override string AggregateResult(string aggregate, string nextResult)
    {
        return aggregate + nextResult;
    }

    public static string ParseAndExpandCode(Expander expander, string code)
    {
        var parser = new LabelParser();
        var visitor = new ExpandingVisitor(expander);
        var result = parser.ParseAndVisitAnyCode(visitor, code);
        if (parser.HasError())
        {
            //todolow improve error handling messages
            throw parser.GetErrors()[0].exception;
        }
        return result;
    }
}
