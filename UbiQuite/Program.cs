using System.Diagnostics;
using System.Runtime.InteropServices;

const Int32 SW_MINIMIZE = 6;
const string UbiGameLauncher = "UbisoftGameLauncher";
const string UbiConnect = "upc";

string[] envArgs = Environment.GetCommandLineArgs();
bool isGameStarted = false;

if (envArgs.Length < 2 || string.IsNullOrEmpty(envArgs[1]))
{
    Console.WriteLine("Please provide game exe path.");

    Console.ReadLine();

    Environment.Exit(0);
}
else
{
    if (envArgs.Length > 2 && !string.IsNullOrEmpty(envArgs[2]))
    {
        MinimizeConsoleWindow();
    }

    Process.Start(envArgs[1]);

    Console.WriteLine(GetGameProcName(envArgs[1]));

    Thread.Sleep(10000);
}

while (true)
{
    Thread.Sleep(3000);

    if (!isGameStarted)
    {
        ValidateUbisoftConnect();

        var gameName = GetGameProcName(envArgs[1]);
        var game = Process.GetProcessesByName(gameName).FirstOrDefault();

        isGameStarted = game != null;
    }
    else
    {
        ValidateGameLauncher();
    }
}

void ValidateGameLauncher()
{
    var gameLauncher = Process.GetProcessesByName(UbiGameLauncher).FirstOrDefault();

    if (gameLauncher == null)
    {
        Console.WriteLine("Exited");

        Thread.Sleep(5000);

        var ubiConnect = Process.GetProcessesByName(UbiConnect).FirstOrDefault();
        ubiConnect?.Kill();

        Environment.Exit(0);
    }
}

void ValidateUbisoftConnect()
{
    var ubiConnect = Process.GetProcessesByName(UbiConnect).FirstOrDefault();

    if (ubiConnect == null)
    {
        Environment.Exit(0);
    }
}

string GetGameProcName(string path)
{
    string[] splits;

    if (path.Contains('/'))
    {
        splits = path.Split('/');
    }
    else
    {
        splits = path.Split('\\');
    }

    string name = splits.Last()[..^4];

    return name;
}

[DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
static extern IntPtr GetConsoleWindow();

[DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
static extern bool ShowWindow([In] IntPtr hWnd, [In] Int32 nCmdShow);

static void MinimizeConsoleWindow()
{
    IntPtr hWndConsole = GetConsoleWindow();
    ShowWindow(hWndConsole, SW_MINIMIZE);
}