﻿using System.Diagnostics;
using Newtonsoft.Json;
using static System.Math;

namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;

    private static int calculatorCount;

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");
        // Use a switch statement to do the math.
        switch (op)
        {
            case "+":
                result = num1 + num2;
                writer.WriteValue("Add");
                break;
            case "-":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                break;
            case "*":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "/":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                break;
            case "^":
                result = Pow(num1, num2);
                writer.WriteValue("Pow");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        calculatorCount++;

        return result;
    }

    public double DoAdvancedOperation(double operand, string op)
    {
        double result = double.NaN;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(operand);
        writer.WritePropertyName("Operation");
        // Use a switch statement to do the math.
        switch (op)
        {
            case "sqrt":
                result = Sqrt(operand); 
                writer.WriteValue("Sqrt");
                break;
            case "10^":
                result = Pow(10, operand);
                writer.WriteValue("10^");
                break;
            case "sin":
                result = Sin(operand);
                writer.WriteValue("Sinus");
                break;
            case "cos":
                result = Cos(operand);
                writer.WriteValue("CosSinus");
                break;
            case "tan":
                result = Tan(operand);
                writer.WriteValue("Tangent");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        calculatorCount++;

        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public int GetCalculatorCount()
    {
        return calculatorCount;
    }
}
