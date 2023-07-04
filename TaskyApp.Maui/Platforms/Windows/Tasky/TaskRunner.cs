using System.Diagnostics;
using TaskyApp.Contracts;

namespace TaskyApp.Maui.SingleProject.WinUI.Tasky;

public class TaskRunner : ITaskRunner
{
    public bool IsPingPongServerEnabled { get; }
    public Task RunTask(Func<CancellationToken, Task> backgroundTask, CancellationToken cancellationToken, TimeSpan? interval = null)
    {
        Debug.WriteLine($"{nameof(TaskRunner)}.{nameof(RunTask)} invoked");
        return Task.CompletedTask;
    }

    public void RunWorker(Func<CancellationToken, Task> backgroundTask, CancellationToken cancellationToken, TimeSpan? interval = null)
    {
        Debug.WriteLine($"{nameof(TaskRunner)}.{nameof(RunWorker)} invoked");
    }

    public Func<string, CancellationToken, Task>? DoWorkFunc { get; }
    public Task StartService(Func<string, CancellationToken, Task> serviceFunc, string workloadName, TimeSpan? interval = null)
    {
        Debug.WriteLine($"{nameof(TaskRunner)}.{nameof(StartService)} invoked");
        return Task.CompletedTask;
    }

    public Task<bool> StopService(string workloadName)
    {
        Debug.WriteLine($"{nameof(TaskRunner)}.{nameof(StopService)} invoked");
        return Task.FromResult(true);
    }

    public Task EnsureGrantedPermission<TPermission>() where TPermission : Permissions.BasePermission, new()
    {
        Debug.WriteLine($"{nameof(TaskRunner)}.{nameof(EnsureGrantedPermission)} invoked");
        return Task.CompletedTask;
    }

    public void Log(string message, string tag = "ITaskRunner", Exception? exception = null, string callerMemberName = "",
        string callerFileName = "")
    {
        Debug.WriteLine($"{nameof(TaskRunner)}.{nameof(Log)} invoked");
    }

    public void AquireCpuWakeLock(string logTag)
    {
        Debug.WriteLine($"{nameof(TaskRunner)}.{nameof(AquireCpuWakeLock)} invoked");
    }

    public void ReleaseCpuWakeLock(string logTag)
    {
        Debug.WriteLine($"{nameof(TaskRunner)}.{nameof(ReleaseCpuWakeLock)} invoked");
    }
}