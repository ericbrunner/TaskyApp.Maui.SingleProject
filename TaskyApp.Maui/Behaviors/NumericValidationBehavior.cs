namespace TaskyApp.Maui.SingleProject.Behaviors;

public class NumericValidationBehavior : Behavior<Entry>
{
    protected override void OnAttachedTo(Entry entry)
    {
        entry.TextChanged += EntryOnTextChanged;
        base.OnAttachedTo(entry);
    }

    private void EntryOnTextChanged(object sender, TextChangedEventArgs e)
    {
        var isValid = double.TryParse(e.NewTextValue, out _);
        ((Entry)sender).TextColor = isValid ? Colors.Green : Colors.Red; 
    }
}