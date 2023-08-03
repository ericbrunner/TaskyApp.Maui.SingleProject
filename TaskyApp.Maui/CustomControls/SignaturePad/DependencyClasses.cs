using System.Windows.Input;

namespace TaskyApp.Maui.SingleProject.CustomControls.SignaturePad
{
    public class MonteurUnterschrift
    {
    }

    public class SignatureViewModel
    {
        public SignatureViewModel(
            List<MonteurUnterschrift> sigInfoList,
            string pageTitle,
            string pageSubTitle,
            Func<List<MonteurUnterschrift>, Task> saveSignaturesFunc,
            Action removeSignaturePadEventsAction,
            Action clearSignaturePadAction)
        {
            SignatoryTitle = "Some Signature Title";
            EditSignatoryCommand = new Command(() =>
            {
                Application.Current.MainPage.DisplayAlert("Open", "OpenEditSignatoryPage", "Ok");
            });
            SignatureDate = DateTime.Now.ToString("dd.MM.yyyy");
            Signatory = "Eric B.";
            SignatoryTitle = nameof(SignatureDate);
            SignatoryMandatory = null;

        }

        public string? SignatoryTitle { get; }

        public Command SaveSignatureCommand { get; set; }
        public IEnumerable<PointF>? SignaturePoints { get; set; }
        public byte[]? SignatureBytes { get; set; }

        public ICommand EditSignatoryCommand { get; }
        public string SignatureDate { get; }
        public string? Signatory { get; }
        public string? SignatoryMandatory { get; }
    }


    public interface IView
    {
    }
    public class NullToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Konvertiert einen Wert zu Bool, falls dieser null ist
        /// </summary>
        /// <param name="value">Momentan implementiert: string, decimal</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Boolean: invertiert das Ergebnis</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool.TryParse(parameter?.ToString(), out bool invert);
            if (value == null)
            {
                return invert;
            }
            else
            {
                if (value is string)
                {
                    string val = value as string;
                    if (string.IsNullOrEmpty(val))
                    {
                        return invert;
                    }
                }
                else if (value is decimal)
                {
                    decimal val = (decimal)value;
                    if (val == 0)
                    {
                        return invert;
                    }
                }
                else
                {
                    if (value == null)
                    {
                        return invert;
                    }
                }
                return !invert;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }


}
