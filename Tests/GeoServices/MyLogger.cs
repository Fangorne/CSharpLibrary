//using System.Reflection;
//using Serilog;
//using Xunit.Abstractions;

//namespace GeoServices;

//public static class MyLogger
//{
//    static MyLogger()
//    {
//        var dllLocation = typeof(MyLogger).GetTypeInfo().Assembly.Location;
//        var path = Path.GetDirectoryName(dllLocation)!;
//        var logsPath = Path.Combine(path, "Logs");

//        Log.Logger = new LoggerConfiguration()
//            .MinimumLevel.Debug()
//            .WriteTo.Console()
//            .WriteTo.File($"{logsPath}/logs", rollingInterval: RollingInterval.Day)
//            .CreateLogger();
//    }

//    public static void ToOutput(ITestOutputHelper output)
//    {
//        var dllLocation = typeof(MyLogger).GetTypeInfo().Assembly.Location;
//        var path = Path.GetDirectoryName(dllLocation)!;
//        var logsPath = Path.Combine(path, "Logs");

//        Log.Logger = new LoggerConfiguration()
//            .MinimumLevel.Debug()
//            .WriteTo.Console()
//            .WriteTo.TestOutput(output)
//            .WriteTo.File($"{logsPath}/logs", rollingInterval: RollingInterval.Day)
//            .CreateLogger();
//    }



//    public static void Information(string msg)
//    {
//        Log.Information(msg);
//    }

//    public static void Warning(string msg)
//    {
//        Log.Warning(msg);
//    }

//    public static void Error(string msg, Exception e)
//    {
//        Log.Error(e, msg);
//    }
//}