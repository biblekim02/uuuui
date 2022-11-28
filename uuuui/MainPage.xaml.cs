using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace uuuui;

public partial class MainPage : ContentPage
{
    bool lstbool = false;

    string str;
    string[] abc;
    int k = 0;
    int j = -1;
    string doc;
    string pat;

    private const string BasePath = "https://maui-97ca5-default-rtdb.firebaseio.com/";   //본인의 FB URL
    private const string FirebaseSecret = "cngousij3PiSkdTVwulzQdSsc9HPclf0h7M2QXPa";    // FB 비번
    private static FirebaseClient _client;


    public MainPage()
    {
        InitializeComponent();
        
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = FirebaseSecret,
            BasePath = BasePath
        };
        _client = new FirebaseClient(config);
        getfb();

        Timer_Tick();

    }

    private void Timer_Tick()
    {
        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            //flbl.Text = DateTime.Now.ToString();
            if (lstbool == false)
            {
                getfb();
            }
            return true;
        });
    }

    private async void getfb()
    {
        //FirebaseResponse response = await _client.GetAsync("MAUI");
        FirebaseResponse response = await _client.GetAsync("PPP");
        if (response == null)
        {
            flbl.Text = "Nodata";
        }
        else
        {
            str = response.Body.ToString();
            if (str.Length > 7)
            {
                str = str.Remove(0, 7);
                str = str.Replace("\"]", "");
                abc = str.Split("\",\"");
                lstview.ItemsSource = abc;

                for (int i = 0; i < abc.Length; i++)
                {
                    if (abc[i].Contains("의:"))
                    {
                        doc = abc[i].Replace("의:", "");
                    }
                    else if (abc[i].Contains("환:"))
                    {
                        pat = abc[i].Replace("환:", "");
                    }
                }
            }
            else
            {
                doc = "";
                pat = "";
            }
            lbldoc.Text = doc;
            lblpat.Text = pat;
        }
    }

    public async void RecordVideo()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult video = await MediaPicker.Default.CaptureVideoAsync();  //PickPhotoAsync(); //CapturePhotoAsync(); //CaptureVideoAsync();
            if (video != null)
            {
                //string localFilePath = Path.Combine(FileSystem.CacheDirectory, video.FileName);
                //resultImage.Source = localFilePath.ToString();
                //lbln.Text = localFilePath.ToString();
                //using Stream sourceStream = await video.OpenReadAsync();
                //using FileStream localFileStream = File.OpenWrite(localFilePath);
                //await sourceStream.CopyToAsync(localFileStream);
            }
        }

        //server

        getfb();
    }

    private void camera_Clicked(object sender, EventArgs e)
    {
        RecordVideo();
    }

    public async void RecordVoice()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult voice = await MediaPicker.Default.CaptureVideoAsync();  //PickPhotoAsync(); //CapturePhotoAsync(); //CaptureVideoAsync();
            if (voice != null)
            {
                //string localFilePath = Path.Combine(FileSystem.CacheDirectory, voice.FileName);
                //resultImage.Source = localFilePath.ToString();
                //lbln.Text = localFilePath.ToString();
                //using Stream sourceStream = await voice.OpenReadAsync();
                //using FileStream localFileStream = File.OpenWrite(localFilePath);
                //await sourceStream.CopyToAsync(localFileStream);
            }
        }

        //server

        getfb();
    }

    private void record_Clicked(object sender, EventArgs e)
    {
        RecordVoice();
    }
    private void File_Clicked(object sender, EventArgs e)
    {
        getfb();

        if (lstbool == true)
        {
            lstbool = false;
            lstview.IsVisible = false;
            Frame1.IsVisible = true;
            Frame2.IsVisible = true;
        }
        else
        {
            lstbool = true;
            lstview.IsVisible = true;
            Frame1.IsVisible = false;
            Frame2.IsVisible = false;
        }

    }

    private async void setfb()
    {

        //FirebaseResponse response = await _client.GetAsync("MAUI");
        FirebaseResponse response = await _client.GetAsync("PPP");
        if (response != null)
        {
            _client.Set<string>("PPP", "");
        }

    }

    private void trash_Clicked(object sender, EventArgs e)
    {
        str = "";
        abc = null;
        k = 0;
        j = -1;
        doc = "";
        pat = "";
        lstview.ItemsSource = abc;

        lbldoc.Text = doc;
        lblpat.Text = pat;

        setfb();

        if (lstbool == true)
        {
            lstbool = false;
            lstview.IsVisible = false;
            Frame1.IsVisible = true;
            Frame2.IsVisible = true;
        }

    }

}