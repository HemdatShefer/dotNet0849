using BlApi;
using System.Diagnostics;
using static BO.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace Simulator;

/// <summary>
/// Manages the simulation of order processing for the system.
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
        isRun = true;// Sets the simulation as active.
        RunSimulator();// Starts the order update simulation.
        RunTime(); // Starts the simulation clock.
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
            Thread.Sleep(1000);// Updates the clock every second.
        })).Start();
    }

    /// <summary>
    /// Begins the simulation of order updates, iterating through order status changes and updating UI components.
    /// </summary>
    private static void RunSimulator()
    {

        new Thread(async (ThreadStart) =>
        {
            while (isRun)
            {
                Order = bl.Order.OldOrder();// Fetches an old order to simulate status updates.

                // Logic to simulate order progression from 'confirmed' to 'shipped', then to 'delivered'.
                if (Order.Status == Status.confirmed)
                {
                    nextStatus = Status.shipped;
                    startTime = DateTime.Now;
                    handleTime = startTime.Value.AddSeconds(10);// Simulates a handling time of 10 seconds.


                    _proggressChange(Order.ID,Order.Status, nextStatus, startTime, handleTime);

                    RunProgressBar();// Starts the progress bar simulation.

                    bl.Order.UpdateshippedDate(Order.ID);// Updates the shipped date in the business logic layer.

                    _proggressChange(Order.ID, nextStatus, Status.deliverd, startTime, handleTime);
                }
                if (Order.Status == Status.shipped)
                {
                    nextStatus = Status.deliverd;
                    startTime = DateTime.Now;
                    handleTime = startTime.Value.AddSeconds(10);// Simulates a handling time of 10 seconds.

                    _proggressChange(Order.ID, Order.Status, nextStatus, startTime, handleTime);

                    RunProgressBar();// Continues the progress bar simulation.

                    bl.Order.UpdateDeliverdDate(Order.ID);// Updates the delivered date in the business logic layer.

                    _proggressChange(Order.ID, nextStatus, Status.deliverd, startTime, handleTime);
                }

            }
        }).Start();

    }

    /// <summary>
    /// Manages the simulation of the progress bar, incrementally updating the progress from 0 to 100%.
    /// </summary>
    private static void RunProgressBar()
    {
        for (int p = 0; p < 101; p++)
        {
            if (!isRun) // Checks if the simulation is still running.
                break;
            _runProgres(p);// Updates the progress bar.
            Thread.Sleep(100);// Waits 100 milliseconds before the next update.
        }
    }
}