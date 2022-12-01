using Firebase.Storage;
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

    private const string BasePath = "https://signlanguage-51654-default-rtdb.firebaseio.com/";   //본인의 FB URL
    private const string FirebaseSecret = "4oFpHzS8P2EUYLZlUSL6ZwjczyJBtoWV6n9GDrAt";    // FB 비번
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

        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            getfb();
            return true;
        });
    }

    private async void getfb()
    {
        FirebaseResponse response = await _client.GetAsync("MAUI");

        str = response.Body.ToString();
        if (str.Length > 7)
        {
            str = str.Remove(0, 7);
            str = str.Replace("\"]", "");
            abc = str.Split("\",\"");
            lstview.ItemsSource = abc;

            for (int i = 0; i < abc.Length; i++)
            {
                if (abc[i].Contains("\",null,\"의사: "))
                {
                    abc[i] = abc[i].Replace("\",null,\"의사: ", " ");
                }
                else if (abc[i].Contains("\",null,\"환:"))
                {
                    abc[i] = abc[i].Replace("\",null,\"환:", " ");
                }

                if (abc[i].Contains("의사: "))
                {
                    doc = abc[i].Replace("의사: ", "");
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
    private async void setfb(string name, string keyname)
    {
        FirebaseResponse response = await _client.GetAsync(keyname);
        _client.Set<int>(keyname + "/flag", 1);
        _client.Set<string>("title", name);
    }
    public async void RecordVideo() //환자
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CaptureVideoAsync();

            if (photo != null)
            {
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();

                var task = new FirebaseStorage("signlanguage-51654.appspot.com",
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                })
                .Child("Patient")
                .Child(ClassId = photo.FileName.ToString())
                .PutAsync(await photo.OpenReadAsync());

                FirebaseResponse response = await _client.GetAsync("title");
                setfb(photo.FileName.ToString(), "video");
            }
        }
    }

    private void camera_Clicked(object sender, EventArgs e) //환자 녹화
    {
        RecordVideo();
    }

    public async void RecordVoice()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CaptureVideoAsync();

            if (photo != null)
            {
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();

                var task = new FirebaseStorage("signlanguage-51654.appspot.com",
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                })
                .Child("Doctor")
                .Child(ClassId = photo.FileName.ToString())
                .PutAsync(await photo.OpenReadAsync());
                

                FirebaseResponse response = await _client.GetAsync("title");
                setfb(photo.FileName.ToString(), "voice");
            }
        }
    }

    private void record_Clicked(object sender, EventArgs e) //의사 녹음
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

    private void trash_Clicked(object sender, EventArgs e)
    {
        _client.Set<string>("MAUI", "");
        _client.Set<int>("video/flag", 0);
        _client.Set<int>("voice/flag", 0);
        _client.Set<int>("title", 0);
        _client.Set<int>("num", 1);

        str = "";
        abc = null;
        k = 0;
        j = -1;
        doc = "";
        pat = "";

        lstview.ItemsSource = abc;

        lbldoc.Text = doc;
        lblpat.Text = pat;


        if (lstbool == true)
        {
            lstbool = false;
            lstview.IsVisible = false;
            Frame1.IsVisible = true;
            Frame2.IsVisible = true;
        }

    }
}