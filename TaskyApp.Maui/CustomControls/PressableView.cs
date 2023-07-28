using System.Windows.Input;

namespace TaskyApp.Maui.SingleProject.CustomControls
{
    public class PressableView : ContentView
    {
        public event EventHandler? Pressed;
        public void RaisePressed()
        {
            Pressed?.Invoke(this, EventArgs.Empty);
            PressedCommand?.Execute(PressedCommandParameter);
        }

        public static readonly BindableProperty PressedCommandProperty = BindableProperty.Create(
            nameof(PressedCommand),
            typeof(ICommand),
            typeof(PressableView));

        public ICommand PressedCommand
        {
            get => (ICommand)GetValue(PressedCommandProperty); 
            set => SetValue(PressedCommandProperty, value);
        }


        public static readonly BindableProperty PressedCommandParameterProperty = BindableProperty.Create(
            nameof(PressedCommandParameter), 
            typeof(object), 
            typeof(PressableView));

        public object? PressedCommandParameter
        {
            get => (object?)GetValue(PressedCommandParameterProperty);
            set => SetValue(PressedCommandParameterProperty, value);
        }


        #region LongPress related

        public event EventHandler? LongPressed;

        public static readonly BindableProperty LongPressCommandProperty = BindableProperty.Create(
            nameof(LongPressCommand),
            typeof(ICommand),
            typeof(PressableView));

        public ICommand? LongPressCommand
        {
            get => (ICommand)GetValue(LongPressCommandProperty);
            set => SetValue(LongPressCommandProperty, value);
        }

        public static readonly BindableProperty LongPressCommandParameterProperty = BindableProperty.Create(
            nameof(LongPressCommandParameter),
            typeof(object),
            typeof(PressableView));

        public object? LongPressCommandParameter
        {
            get => (object?)GetValue(LongPressCommandParameterProperty);
            set => SetValue(LongPressCommandParameterProperty, value);
        }

        public static readonly BindableProperty LongPressDurationProperty = BindableProperty.Create(
            nameof(LongPressDuration),
            typeof(int),
            typeof(PressableView),
            defaultValue:500);


        public int LongPressDuration
        {
            get => (int)GetValue(LongPressDurationProperty); 
            set => SetValue(LongPressDurationProperty, value);
        }

        public void RaiseLongPressed()
        {
            LongPressed?.Invoke(this, EventArgs.Empty);
            LongPressCommand?.Execute(LongPressCommandParameter);
        }



        #endregion
    }
}