﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evbpc.Framework.Utilities.Logging;

namespace Evbpc.Framework.Utilities.Prompting
{
    /// <summary>
    /// Represents an <see cref="IPrompt"/> that prompts the user with <code>Console</code> input.
    /// </summary>
    public class ConsolePrompt : IPrompt
    {
        /// <summary>
        /// A <see cref="ILogger"/> that <see cref="ConsolePrompt"/> errors/messages should be posted to.
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// Constructs a new instance of the <see cref="ConsolePrompt"/> with the specified <see cref="ILogger"/>.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> that should be used for the <see cref="Logger"/>.</param>
        public ConsolePrompt(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// This will repeatedly prompt the user with a message and request input, then return said input (if valid).
        /// </summary>
        /// <typeparam name="T">The type of input that should be returned.</typeparam>
        /// <param name="message">The message to initally display to the user.</param>
        /// <param name="options">The <see cref="PromptOptions"/> that specify how the prompt should be treated.</param>
        /// <param name="defaultValue">The default value to be returned if a user enters an empty line (`""`).</param>
        /// <param name="failureMessage">An optional message to display on a failure. If null, then the `message` parameter will be displayed on failure.</param>
        /// <param name="validationMethod">An optional delegate to a method which can perform additional validation if the input is of the target type.</param>
        /// <param name="parseResultMethod">An optional delegate to a method which will parse the final result (assuming validity checks all pass).</param>
        /// <returns>The input collected from the user when it is deemed valid.</returns>
        public T Prompt<T>(string message,
                           PromptOptions options,
                           T defaultValue = default(T),
                           string failureMessage = null,
                           Func<T, bool> validationMethod = null,
                           Func<T, T> parseResultMethod = null)
            where T : IConvertible
        {
            T result = defaultValue;
            Prompt(out result, message, options, defaultValue, failureMessage, validationMethod, parseResultMethod, 0);
            return result;
        }

        private static bool Retrieve<T>(string line, out T resultValue)
            where T : IConvertible
        {
            var type = typeof(T);

            resultValue = default(T);

            try
            {
                resultValue = (T)Convert.ChangeType(line, type);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Prompts the user for input.
        /// </summary>
        /// <typeparam name="T">The type of input that should be returned.</typeparam>
        /// <param name="result">Filled with the result of collecting the input from the user and applying it to the specified type.</param>
        /// <param name="message">The message to initally display to the user.</param>
        /// <param name="options">The <see cref="PromptOptions"/> that specify how the prompt should be treated.</param>
        /// <param name="defaultValue">The default value to be returned if a user enters an empty line (`""`).</param>
        /// <param name="failureMessage">An optional message to display on a failure. If null, then the `message` parameter will be displayed on failure.</param>
        /// <param name="validationMethod">An optional delegate to a method which can perform additional validation if the input is of the target type.</param>
        /// <param name="parseResultMethod">An optional delegate to a method which will parse the final result (assuming validity checks all pass).</param>
        /// <param name="maxTries">An optional integer specifying the maximum number of attempts to make. If <code>0</code> or smaller then the number of attempts will not be limited.</param>
        /// <returns>A boolean value indicating success. If <code>true</code> then result is filled with the user specified input, or the default value. If <code>false</code> then result will be <code>default(T)</code>.</returns>
        public bool Prompt<T>(out T result,
                              string message, 
                              PromptOptions options, 
                              T defaultValue = default(T), 
                              string failureMessage = null, 
                              Func<T, bool> validationMethod = null, 
                              Func<T, T> parseResultMethod = null, 
                              int maxTries = 0) 
            where T : IConvertible
        {
            Console.Write(message);

            if (options == PromptOptions.Optional)
            {
                Console.Write($" [{defaultValue}]");
            }

            Console.Write(": ");

            bool pass = false;
            result = default(T);
            int tries = 0;

            while (!pass)
            {
                string line = Console.ReadLine();

                if (options == PromptOptions.Required || (options == PromptOptions.Optional && !string.IsNullOrEmpty(line)))
                {
                    pass = Retrieve(line, out result);

                    if (pass && validationMethod != null)
                    {
                        pass = validationMethod.Invoke(result);
                    }
                }
                else if (options == PromptOptions.Optional && string.IsNullOrEmpty(line))
                {
                    pass = true;
                    result = defaultValue;
                }

                tries++;

                if (pass)
                {
                    break;
                }

                if (tries == maxTries)
                {
                    pass = false;
                    break;
                }

                Console.WriteLine("Invalid value [{0}]", line);

                string resultMessage = failureMessage ?? message;

                if (options == PromptOptions.Optional)
                {
                    resultMessage += $" [{defaultValue}]";
                }

                Console.Write(resultMessage + ": ");
            }

            if (parseResultMethod != null)
            {
                result = parseResultMethod.Invoke(result);
            }

            return pass;
        }
    }
}
