using System;
using System.Collections.Generic;
using System.ComponentModel;
using TaskyApp.Models;
using TaskyApp.ViewModels;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace TaskyApp.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Todo TodoItem { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}