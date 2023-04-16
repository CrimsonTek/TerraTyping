using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;
using Microsoft.VisualBasic;
using TerraTyping.TypeLoaders;

namespace TerraTyping.Helpers;

public static class LogHelper
{
    public static void Log(this ILog logger, Verbosity verbosity, object callerName, object message, params (string, object)[] args)
    {
        LogInner(logger, verbosity, callerName, message, null, args);
    }

    public static void Log(this ILog logger, Verbosity verbosity, object callerName, object message, Exception exception, params (string, object)[] args)
    {
        LogInner(logger, verbosity, callerName, message, exception, args);
    }

    public static void Log(this ILog logger, Verbosity verbosity, object callerName, object message, Exception exception, ParseContext context, params (string, object)[] args)
    {
        LogInner(logger, verbosity, callerName, message, exception, MyUtils.ArrayCombiner(ContextToArray(context), args));
    }

    public static void Log(this ILog logger, Verbosity verbosity, object message, ParseContext context, [CallerMemberName] string callerName = "")
    {
        LogInner(logger, verbosity, callerName, message, null, ContextToArray(context));
    }

    public static void Log(this ILog logger, Verbosity verbosity, object callerName, object message, ParseContext context)
    {
        LogInner(logger, verbosity, callerName, message, null, ContextToArray(context));
    }

    public static void Log(this ILog logger, Verbosity verbosity, object callerName, object message, Exception exception, ParseContext context)
    {
        LogInner(logger, verbosity, callerName, message, exception, ContextToArray(context));
    }

    private static void LogInner(ILog logger, Verbosity verbosity, object callerName, object message, Exception exception, (string, object)[] args)
    {
        if (callerName is string callerString && string.IsNullOrWhiteSpace(callerString))
        {
            throw new ArgumentException($"'{nameof(callerName)}' cannot be null or whitespace.", nameof(callerName));
        }

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"{callerName}: {message}");

        for (int i = 0; i < args.Length; i++)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append($"    {args[i].Item1}: {args[i].Item2}");
        }

        if (exception is not null)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append(exception);
        }

        HandleVerbosity(logger, verbosity, stringBuilder.ToString());
    }

    private static void HandleVerbosity(ILog log, Verbosity verbosity, string str)
    {
        switch (verbosity)
        {
            case Verbosity.Debug:
                log.Debug(str);
                break;
            case Verbosity.Info:
                log.Info(str);
                break;
            case Verbosity.Warn:
                log.Warn(str);
                break;
            case Verbosity.Error:
                log.Error(str);
                break;
            case Verbosity.Fatal:
                log.Fatal(str);
                break;
            default:
                throw new ArgumentException($"Unexpected verbosity: {verbosity}");
        }
    }

    private static (string, object)[] ContextToArray(ParseContext context)
    {
        return new (string, object)[]
        {
            ( "File Name", context.FileName ),
            ( "Line Number", context.LineCount ),
            ( "Line Content", context.Line ),
            context.ModSource is null ? ($"Source", "User") : ($"Mod Source", context.ModSource.ToString())
        };
    }
}

public enum Verbosity
{
    Debug,
    Info,
    Warn,
    Error,
    Fatal,
}
