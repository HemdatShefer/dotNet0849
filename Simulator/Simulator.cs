using BlApi;
using System.Diagnostics;
using static BO.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace Simulator;

/// <summary>
/// A simulator for demonstrating the progress of orders within the system.
/// </summary>
public static class Simulator
{
    static private readonly IBl bl = Factory.Get();

    /// <summary>
    /// The current order being processed by the simulator.
    /// </summary>
    static public BO.Order Order { get; set; }

    /// <summary>
    /// A flag indicating whether the simulator is running.
    /// </summary>
    static public bool isRun;

    // Actions to be called upon progress change and updates in the simulation.
    static private Action<int, Status?, Status?, DateTime?, DateTime?> _proggressChange;
    static private Action<DateTime> _runClock;
    static private Action<int> _runProgres;

    static Status? currentStatus;
    static Status? nextStatus;
    static DateTime? startTime;
    static DateTime? handleTime;

    /// <summary>
    /// Starts the simulation process.
    /// </summary>
    /// <param name="proggressChange">An action to invoke upon progress change.</param>
    /// <param name="runClock">An action to invoke upon clock update.</param>
    /// <param name="runProgres">An action to invoke upon progress bar update.</param>
    public static void StartSimulator(Action<int, Status?, Status?, DateTime?, DateTime?> proggressChange, Action<DateTime> runClock, Action<int> runProgres)
    {
        _proggressChange = proggressChange;
        _runClock = runClock;
        _runProgres = runProgres;
        isRun = true;
        RunSimulator();
        RunTime();
    }
    /// <summary>
    /// Initiates the clock operation in the simulator.
    /// </summary>
    private static void RunTime()
    {
        new Thread(new ThreadStart(() =>
        {
            while (isRun)
                _runClock(DateTime.Now);
            Thread.Sleep(1000);
        })).Start();
    }

    /// <summary>
    /// Begins the simulation of order updates.
    /// </summary>
    private static void RunSimulator()
    {

        new Thread(async (ThreadStart) =>
        {
            while (isRun)
            {
                Order = bl.Order.OldOrder();

                if (Order.Status == Status.confirmed)
                {
                    nextStatus = Status.shipped;
                    startTime = DateTime.Now;
                    handleTime = startTime.Value.AddSeconds(10);

                    _proggressChange(Order.ID,Order.Status, nextStatus, startTime, handleTime);

                    RunProgressBar();

                    bl.Order.UpdateshippedDate(Order.ID);

                    _proggressChange(Order.ID, nextStatus, Status.deliverd, startTime, handleTime);
                }
                if (Order.Status == Status.shipped)
                {
                    nextStatus = Status.deliverd;
                    startTime = DateTime.Now;
                    handleTime = startTime.Value.AddSeconds(10);

                    _proggressChange(Order.ID, Order.Status, nextStatus, startTime, handleTime);

                    RunProgressBar();

                    bl.Order.UpdateDeliverdDate(Order.ID);

                    _proggressChange(Order.ID, nextStatus, Status.deliverd, startTime, handleTime);
                }
            }
        }).Start();

    }
    /// <summary>
    /// Starts the progress bar operation in the simulator.
    /// </summary>
    private static void RunProgressBar()
    {
        for (int p = 0; p < 101; p++)
        {
            if (!isRun)
                break;
            _runProgres(p);
            Thread.Sleep(100);
        }
    }
}