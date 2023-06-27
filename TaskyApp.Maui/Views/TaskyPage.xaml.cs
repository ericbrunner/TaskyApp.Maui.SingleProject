using System;
using System.ComponentModel;
using TaskyApp.Contracts;
using TaskyApp.ViewModels;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using TaskyApp.Maui.SingleProject;

namespace TaskyApp.Views
{
    public partial class TaskyPage : ContentPage
    {
        public TaskyPage()
        {
            InitializeComponent();

            BindingContext = App.Get<ITaskyViewModel>();
        }
    }
}