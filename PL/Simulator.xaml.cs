﻿using System.Collections;
using System.Windows;
using System.Collections.Generic;
using static BO.Enums;
using System;

namespace PL;

public partial class SimulatorWindow : Window
{
    public int CurrentID
    {
        get { return (int)GetValue(CurrentOrderInLineProperty); }
        set { SetValue(CurrentOrderInLineProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CurrentOrderInLine.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentOrderInLineProperty =
        DependencyProperty.Register("CurrentID", typeof(int), typeof(SimulatorWindow));

    public Status? NextStatus
    {
        get { return (Status)GetValue(NextStatusProperty); }
        set { SetValue(NextStatusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for NextStatus.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NextStatusProperty =
        DependencyProperty.Register("NextStatus", typeof(Status?), typeof(SimulatorWindow));

    public Status? Status
    {
        get { return (Status)GetValue(StatusProperty); }
        set { SetValue(StatusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for NextStatus.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register("Status", typeof(Status?), typeof(SimulatorWindow));


    public DateTime? StartTime
    {
        get { return (DateTime)GetValue(startTimeProperty); }
        set { SetValue(startTimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for startTime.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty startTimeProperty =
        DependencyProperty.Register("StartTime", typeof(DateTime?), typeof(SimulatorWindow));

    public DateTime? HandleTime
    {
        get { return (DateTime)GetValue(handleTimeProperty); }
        set { SetValue(handleTimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for handleTime.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty handleTimeProperty =
        DependencyProperty.Register("HandleTime", typeof(DateTime?), typeof(SimulatorWindow));


    public DateTime Clock
    {
        get { return (DateTime)GetValue(ClockProperty); }
        set { SetValue(ClockProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Clock.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ClockProperty =
        DependencyProperty.Register("Clock", typeof(DateTime), typeof(SimulatorWindow));




    public int TimeProgress
    {
        get { return (int)GetValue(TimeProgressProperty); }
        set { SetValue(TimeProgressProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TimeProgress.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TimeProgressProperty =
        DependencyProperty.Register("TimeProgress", typeof(int), typeof(SimulatorWindow));



    public SimulatorWindow()
    {
        this.WindowStyle = WindowStyle.None;
        InitializeComponent();
        Clock = DateTime.Now;
        TimeProgress = 0;
        Simulator.Simulator.StartSimulator(DataChange, RunClock, RunProgress);
    }


    private void DataChange(int id, Status? status, Status? nextStatus, DateTime? startTime, DateTime? handleTime)
    {
        Application.Current.Dispatcher.Invoke(new Action(() =>
        {
            CurrentID = id;
            NextStatus = nextStatus;
            Status = status;
            StartTime = startTime;
            HandleTime = handleTime;
        }));
    }
    private void RunClock(DateTime currentTime)
    {
        Application.Current?.Dispatcher.Invoke(new Action(() =>
        {
            Clock = currentTime;
        }));
    }

    private void RunProgress(int progress)
    {
        Application.Current?.Dispatcher.Invoke(new Action(() =>
        {
            TimeProgress = progress;
        }));
    }

    private void EndSimulationClick(object sender, RoutedEventArgs e)
    {
        Simulator.Simulator.isRun = false;
        this.Close();
    }
}