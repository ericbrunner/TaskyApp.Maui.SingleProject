using MvvmHelpers.Interfaces;
using System.Windows.Input;

namespace TaskyApp.Contracts;

public interface ITaskyViewModel : IBaseViewModel
{
    IAsyncCommand GetTodosCommand { get; }
    ICommand GetLocationCommand { get; }
    ICommand StartGpsServiceCommand { get; }
    ICommand StopGpsServiceCommand { get; }
    ICommand StartWorkerCommand { get; }
    ICommand StopWorkerCommand { get; }
    ICommand StartTaskCommand { get; }
    ICommand StopTaskCommand { get; }
    ICommand LongPressCommand { get; }
    string LongPressCommandParam { get; }
    ICommand PressedCommand { get; }
    string PressedCommandParameter { get; }
}