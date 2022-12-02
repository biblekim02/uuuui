<<<<<<< HEAD
﻿using Firebase.Storage;
using FireSharp;
=======
﻿using FireSharp;
>>>>>>> 3d5290191ac59befdcf1d19f186015af4146ffe8
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

<<<<<<< HEAD
    private const string BasePath = "https://signlanguage-51654-default-rtdb.firebaseio.com/";   //본인의 FB URL
    private const string FirebaseSecret = "4oFpHzS8P2EUYLZlUSL6ZwjczyJBtoWV6n9GDrAt";    // FB 비번
=======
    private const string BasePath = "https://maui-97ca5-default-rtdb.firebaseio.com/";   //본인의 FB URL
    private const string FirebaseSecret = "cngousij3PiSkdTVwulzQdSsc9HPclf0h7M2QXPa";    // FB 비번
>>>>>>> 3d5290191ac59befdcf1d19f186015af4146ffe8
    private static FirebaseClient _client;


    public MainPage()
    {
        InitializeComponent();
<<<<<<< HEAD

=======
        
>>>>>>> 3d5290191ac59befdcf1d19f186015af4146ffe8
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = FirebaseSecret,
            BasePath = BasePath
        };
        _client = new FirebaseClient(config);
<<<<<<< HEAD

        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            getfb();
=======
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
>>>>>>> 3d5290191ac59befdcf1d19f186015af4146ffe8
            return true;
        });
    }

    private async void getfb()
    {
<<<<<<< HEAD
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
                else if (abc[i].Contains("\",null,\"환자: "))
                {
                    abc[i] = abc[i].Replace("\",null,\"환자: ", " ");
                }

                if (abc[i].Contains("의사: "))
                {
                    doc = abc[i].Replace("의사: ", "");
                }
                else if (abc[i].Contains("환자: "))
                {
                    pat = abc[i].Replace("환자: ", "");
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
=======
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
>>>>>>> 3d5290191ac59befdcf1d19f186015af4146ffe8
    {
        RecordVideo();
    }

    public async void RecordVoice()
    {
<<<<<<< HEAD

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
=======
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
>>>>>>> 3d5290191ac59befdcf1d19f186015af4146ffe8
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

<<<<<<< HEAD
    private void trash_Clicked(object sender, EventArgs e)
    {
        _client.Set<string>("MAUI", "");

        _client.Set<int>("video/flag", 0);
        _client.Set<int>("voice/flag", 0);

        _client.Set<int>("title", 0);
        _client.Set<int>("num", 1);

=======
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
>>>>>>> 3d5290191ac59befdcf1d19f186015af4146ffe8
        str = "";
        abc = null;
        k = 0;
        j = -1;
        doc = "";
        pat = "";
<<<<<<< HEAD

=======
>>>>>>> 3d5290191ac59befdcf1d19f186015af4146ffe8
        lstview.ItemsSource = abc;

        lbldoc.Text = doc;
        lblpat.Text = pat;

<<<<<<< HEAD
=======
        setfb();
>>>>>>> 3d5290191ac59befdcf1d19f186015af4146ffe8

        if (lstbool == true)
        {
            lstbool = false;
            lstview.IsVisible = false;
            Frame1.IsVisible = true;
            Frame2.IsVisible = true;
        }

    }
<<<<<<< HEAD
=======

>>>>>>> 3d5290191ac59befdcf1d19f186015af4146ffe8
}