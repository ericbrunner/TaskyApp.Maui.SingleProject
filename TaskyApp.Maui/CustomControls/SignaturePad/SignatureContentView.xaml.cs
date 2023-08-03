using CommunityToolkit.Maui.Core;

namespace TaskyApp.Maui.SingleProject.CustomControls.SignaturePad
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignatureContentView : ContentView, IView
    {
        private Action removeSignaturePadEventsAction;
        private Action clearSignaturePadAction;
        private SignatureViewModel finalPageSignatureViewModel;

        public Command SaveSignatureCommand => finalPageSignatureViewModel?.SaveSignatureCommand;

        public SignatureContentView() : this(null, null, null, null)
        {
        }

        public SignatureContentView(List<MonteurUnterschrift> sigInfoList, string pageTitle, string pageSubTitle,
            Func<List<MonteurUnterschrift>, Task> saveSignaturesFunc)
        {
            InitializeComponent();

            SigPad.DrawingLineCompleted += SigPad_StrokeCompleted;

            SigPad.Cleared += SigPad_Cleared;

            removeSignaturePadEventsAction = RemoveSignaturePadEvents;
            clearSignaturePadAction = () => { SigPad?.Clear(); };

            BindingContext = finalPageSignatureViewModel = new SignatureViewModel(sigInfoList, pageTitle, pageSubTitle,
                saveSignaturesFunc,
                removeSignaturePadEventsAction, clearSignaturePadAction);
        }

        private void SigPad_Cleared(object? sender, EventArgs e)
        {
            finalPageSignatureViewModel.SignaturePoints = new List<PointF>();
            finalPageSignatureViewModel.SignatureBytes = null;
        }

        private async void SigPad_StrokeCompleted(object? sender, DrawingLineCompletedEventArgs e)
        {
            finalPageSignatureViewModel.SignaturePoints = SigPad.Lines.FirstOrDefault()?.Points.AsEnumerable();

            await using var imgStream = await SigPad.GetImageStream(300, 300);
            finalPageSignatureViewModel.SignatureBytes = ReadFully(imgStream);
        }

        public static byte[] ReadFully(Stream input)
        {
            int read;
            var buffer = new byte[16 * 1024];

            using var ms = new MemoryStream();
            
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return ms.ToArray();
        }

        private void RemoveSignaturePadEvents()
        {
            if (SigPad == null)
                return;

            SigPad.DrawingLineCompleted -= SigPad_StrokeCompleted;

            SigPad.Cleared -= SigPad_Cleared;
        }

        private void Frame_SizeChanged(object sender, System.EventArgs e)
        {
            if (DeviceInfo.Idiom != DeviceIdiom.Phone)
            {
                // Aspektrate soll beibehalten werden
                if (((Frame)sender).Width * .6 < this.Height - 100)
                {
                    ((Frame)sender).VerticalOptions = LayoutOptions.Start;
                    ((Frame)sender).HeightRequest = ((Frame)sender).Width * .6;
                }
                else
                {
                    // wenn es nicht passen sollte einfach so groß anzeigen wie möglich
                    ((Frame)sender).VerticalOptions = LayoutOptions.Fill;
                }
            }
        }
    }
}