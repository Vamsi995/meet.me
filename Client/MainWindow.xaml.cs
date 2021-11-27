﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using ScreenSharing;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static WhiteBoardView _whiteboard;
        public bool sharing = false;
        private static ChatView _chat;
        private static UsersList _userslist;
        private bool _chatFlag = false;
        private bool _ssFlag = false;
        private bool _wbFlag = true;
        private ScreenShareClient SSClient;

        public MainWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();
            _whiteboard = new WhiteBoardView();
            this.SSwb.Content = _whiteboard;
            _chat = new ChatView();
            this.Chat.Content = _chat;
            _userslist = new UsersList(this);
            this.UsersListControl.Content = _userslist;

            SSClient = ScreenShareFactory.GetScreenSharer();
        }
        //taken from https://stackoverflow.com/questions/4019831/how-do-you-center-your-main-window-in-wpf
        /// <summary>
        /// Function to launch the window on the center of the screen
        /// </summary>
        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
        /// <summary>
        /// Function to change the theme
        /// </summary>
        private void OnThemeClick(object sender, RoutedEventArgs e)
        {
            ResourceDictionary dict = new ResourceDictionary();
            if (Theme.IsChecked == true)
            {
                dict.Source = new Uri("Theme2.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dict);
            }
            else
            {
                dict.Source = new Uri("Theme1.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dict);
            }
        }
        /// <summary>
        /// Drag functionality
        /// </summary>
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        /// <summary>
        /// Minimize button functionality
        /// </summary>  
        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal || this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Minimized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
        /// <summary>
        /// Maximize button functionality
        /// </summary>
        private void OnMaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.WindowState = WindowState.Maximized;
            MaximizeButton.Visibility = Visibility.Collapsed;
            RestoreButton.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Restore button functionality
        /// </summary>
        private void OnRestoreButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            RestoreButton.Visibility = Visibility.Collapsed;
            MaximizeButton.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Close button functionality
        /// </summary>
        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Function to handle ScreenShare button click event
        /// </summary>
        private void OnScreenShareClick(object sender, RoutedEventArgs e)
        {
            _ssFlag = true;
            if (!sharing)
            {
                SSClient.StartSharing();
                sharing = true;
            }
            else
            {
               SSClient.StopSharing();
               sharing = false;
            }
            this.SSwb.Content = new ScreenShareUX();
            if (_chatFlag.Equals(true) && _userslist.UserListHidden.Equals(false))
            {
                SSwb.SetValue(Grid.ColumnProperty, 4);
                SSwb.SetValue(Grid.ColumnSpanProperty, 1);
            }
            else if (_chatFlag.Equals(true))
            {
                SSwb.SetValue(Grid.ColumnProperty, 2);
                SSwb.SetValue(Grid.ColumnSpanProperty, 3);
            }
            else if (_userslist.UserListHidden.Equals(false))
            {
                SSwb.SetValue(Grid.ColumnProperty, 4);
                SSwb.SetValue(Grid.ColumnSpanProperty, 3);
            }
            else
            {
                SSwb.SetValue(Grid.ColumnProperty, 2);
                SSwb.SetValue(Grid.ColumnSpanProperty, 5);
            }
        }
        /// <summary>
        /// Function to handle Whiteboard button click event
        /// </summary>
        private void OnWhiteboardClick(object sender, RoutedEventArgs e)
        {
            _wbFlag = true;
            this.SSwb.Content = _whiteboard;
            if (_chatFlag.Equals(true) && _userslist.UserListHidden.Equals(false))
            {
                SSwb.SetValue(Grid.ColumnProperty, 4);
                SSwb.SetValue(Grid.ColumnSpanProperty, 1);
            }
            else if (_chatFlag.Equals(true))
            {
                SSwb.SetValue(Grid.ColumnProperty, 2);
                SSwb.SetValue(Grid.ColumnSpanProperty, 3);
            }
            else if (_userslist.UserListHidden.Equals(false))
            {
                SSwb.SetValue(Grid.ColumnProperty, 4);
                SSwb.SetValue(Grid.ColumnSpanProperty, 3);
            }
            else
            {
                SSwb.SetValue(Grid.ColumnProperty, 2);
                SSwb.SetValue(Grid.ColumnSpanProperty, 5);
            }
        }
        /// <summary>
        /// Function to handle Chat button click event
        /// </summary>
        private void OnChatButtonClick(object sender, RoutedEventArgs e)
        {
            if (_chatFlag.Equals(false))
            {
                if (_ssFlag.Equals(true) || _wbFlag.Equals(true))
                {
                    if (_userslist.UserListHidden.Equals(false))
                    {
                        SSwb.SetValue(Grid.ColumnProperty, 4);
                        SSwb.SetValue(Grid.ColumnSpanProperty, 1);
                    }

                    else
                    {
                        SSwb.SetValue(Grid.ColumnProperty, 2);
                        SSwb.SetValue(Grid.ColumnSpanProperty, 3);
                    }
                }
                this.Chat.Visibility = Visibility.Visible;
                _chatFlag = true;
            }
            else
            {
                this.Chat.Visibility = Visibility.Collapsed;
                _chatFlag = false;
                if (_ssFlag.Equals(true) || _wbFlag.Equals(true))
                {
                    if (_userslist.UserListHidden.Equals(false))
                    {
                        SSwb.SetValue(Grid.ColumnProperty, 4);
                        SSwb.SetValue(Grid.ColumnSpanProperty, 3);
                    }
                    else
                    {
                        SSwb.SetValue(Grid.ColumnProperty, 2);
                        SSwb.SetValue(Grid.ColumnSpanProperty, 5);
                    }
                }
            }
        }
        /// <summary>
        /// Function to handle Dashboard button click event
        /// </summary>
        private void OnDashboardClick(object sender, RoutedEventArgs e)
        {
            DashboardView dashboard = new DashboardView();
            dashboard.Show();
        }
        /// <summary>
        /// Function to handle UsersList expansion button click event
        /// </summary>
        public void OnUsersListClick()
        {
            if (_userslist.UserListHidden.Equals(true))
            {
                UsersListControl.SetValue(Grid.ColumnSpanProperty, 3);
                if (_ssFlag.Equals(true) || _wbFlag.Equals(true))
                {
                    SSwb.SetValue(Grid.ColumnProperty, 4);
                    if (_chatFlag.Equals(true))
                    {
                        SSwb.SetValue(Grid.ColumnSpanProperty, 1);
                    }
                    else
                    {
                        SSwb.SetValue(Grid.ColumnSpanProperty, 3);
                    }
                }
            }
            else
            {
                UsersListControl.SetValue(Grid.ColumnSpanProperty, 1);
                if (_ssFlag.Equals(true) || _wbFlag.Equals(true))
                {
                    SSwb.SetValue(Grid.ColumnProperty, 2);
                    if (_chatFlag.Equals(true))
                    {
                        SSwb.SetValue(Grid.ColumnSpanProperty, 3);
                    }
                    else
                    {
                        SSwb.SetValue(Grid.ColumnProperty, 1);
                        SSwb.SetValue(Grid.ColumnSpanProperty, 5);
                    }
                }
            }
        }
        /// <summary>
        /// Function to call OnLeaveButtonClick() when Leave button is clicked
        /// </summary>
        private void OnLeaveButtonClicked(object sender, RoutedEventArgs e)
        {
            _userslist.OnLeaveButtonClick();
        }
        
    }
}