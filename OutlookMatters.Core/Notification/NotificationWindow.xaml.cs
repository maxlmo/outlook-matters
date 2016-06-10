﻿using System;
using System.Windows;
using System.Windows.Threading;

namespace OutlookMatters.Core.Notification
{
    public partial class NotificationWindow
    {
        public NotificationWindow()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

                this.Left = corner.X - this.ActualWidth - 100;
                this.Top = corner.Y - this.ActualHeight;
                this.Topmost = true;
            }));
        }
    }
}