using System.ComponentModel;
using TaskyApp.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace TaskyApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}