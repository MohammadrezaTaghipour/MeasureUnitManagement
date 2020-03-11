using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Domain.Services.ExpressionEvaluator
{
    public class FormulaExpressionEvaluator : IFormulaExpressionEvaluator
    {
        public double Evaluate(string expression)
        {
            var tokenizer = new Tokenizer(new StringReader(expression));
            var node = this.ParseExpression(tokenizer);
            return node.Eval();
        }

        // Parse an entire expression and check EOF was reached
        private Node ParseExpression(Tokenizer tokenizer)
        {
            // For the moment, all we understand is add and subtract
            var expr = ParseAddSubtract(tokenizer);

            // Check everything was consumed
            if (tokenizer.Token != Token.EOF)
                throw new Exception("Unexpected characters at end of expression");

            return expr;
        }

        // Parse an sequence of add/subtract operators
        private Node ParseAddSubtract(Tokenizer tokenizer)
        {
            // Parse the left hand side
            var lhs = ParseMultiplyDivide(tokenizer);

            while (true)
            {
                // Work out the operator
                Func<double, double, double> op = null;
                if (tokenizer.Token == Token.Add)
                {
                    op = (a, b) => a + b;
                }
                else if (tokenizer.Token == Token.Subtract)
                {
                    op = (a, b) => a - b;
                }

                // Binary operator found?
                if (op == null)
                    return lhs;             // no

                // Skip the operator
                tokenizer.NextToken();

                // Parse the right hand side of the expression
                var rhs = ParseMultiplyDivide(tokenizer);

                // Create a binary node and use it as the left-hand side from now on
                lhs = new NodeBinary(lhs, rhs, op);
            }
        }

        // Parse an sequence of add/subtract operators
        private Node ParseMultiplyDivide(Tokenizer tokenizer)
        {
            // Parse the left hand side
            var lhs = ParseUnary(tokenizer);

            while (true)
            {
                // Work out the operator
                Func<double, double, double> op = null;
                if (tokenizer.Token == Token.Multiply)
                {
                    op = (a, b) => a * b;
                }
                else if (tokenizer.Token == Token.Divide)
                {
                    op = (a, b) => a / b;
                }

                // Binary operator found?
                if (op == null)
                    return lhs;             // no

                // Skip the operator
                tokenizer.NextToken();

                // Parse the right hand side of the expression
                var rhs = ParseUnary(tokenizer);

                // Create a binary node and use it as the left-hand side from now on
                lhs = new NodeBinary(lhs, rhs, op);
            }
        }

        // Parse a unary operator (eg: negative/positive)
        private Node ParseUnary(Tokenizer tokenizer)
        {
            while (true)
            {
                // Positive operator is a no-op so just skip it
                if (tokenizer.Token == Token.Add)
                {
                    // Skip
                    tokenizer.NextToken();
                    continue;
                }

                // Negative operator
                if (tokenizer.Token == Token.Subtract)
                {
                    // Skip
                    tokenizer.NextToken();

                    // Parse RHS 
                    // Note this recurses to self to support negative of a negative
                    var rhs = ParseUnary(tokenizer);

                    // Create unary node
                    return new NodeUnary(rhs, (a) => -a);
                }

                // No positive/negative operator so parse a leaf node
                return ParseLeaf(tokenizer);
            }
        }

        // Parse a leaf node
        // (For the moment this is just a number)
        private Node ParseLeaf(Tokenizer tokenizer)
        {
            // Is it a number?
            if (tokenizer.Token == Token.Number)
            {
                var node = new NodeNumber(tokenizer.Number);
                tokenizer.NextToken();
                return node;
            }

            // Parenthesis?
            if (tokenizer.Token == Token.OpenParens)
            {
                // Skip '('
                tokenizer.NextToken();

                // Parse a top-level expression
                var node = ParseAddSubtract(tokenizer);

                // Check and skip ')'
                if (tokenizer.Token != Token.CloseParens)
                    throw new Exception("Missing close parenthesis");
                tokenizer.NextToken();

                // Return
                return node;
            }

            // Variable
            if (tokenizer.Token == Token.Identifier)
            {
                // Capture the name and skip it
                var name = tokenizer.Identifier;
                tokenizer.NextToken();

                // Parens indicate a function call, otherwise just a variable
                if (tokenizer.Token != Token.OpenParens)
                {
                    // Variable
                    //return new NodeVariable(name);
                }
                else
                {
                    // Function call

                    // Skip parens
                    tokenizer.NextToken();

                    // Parse arguments
                    var arguments = new List<Node>();
                    while (true)
                    {
                        // Parse argument and add to list
                        arguments.Add(ParseAddSubtract(tokenizer));

                        // Is there another argument?
                        if (tokenizer.Token == Token.Comma)
                        {
                            tokenizer.NextToken();
                            continue;
                        }

                        // Get out
                        break;
                    }

                    // Check and skip ')'
                    if (tokenizer.Token != Token.CloseParens)
                        throw new Exception("Missing close parenthesis");
                    tokenizer.NextToken();

                    // Create the function call node
                    //return new NodeFunctionCall(name, arguments.ToArray());
                }
            }

            // Don't Understand
            throw new Exception($"Unexpect token: {tokenizer.Token}");
        }
    }
}
