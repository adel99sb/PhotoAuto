namespace PhotoAuto;

public partial class MainPage : ContentPage
{
    string MainPath;
    public MainPage()
    {
        var status = Permissions.RequestAsync<Permissions.StorageWrite>();
        if (Directory.Exists("/storage/emulated/0/DCIM/Rony/"))
           this.MainPath = "/storage/emulated/0/DCIM/Rony/";
        else
        {
            string mainDir = Path.Combine("/storage/emulated/0/DCIM/", "Rony");
            System.IO.Directory.CreateDirectory(mainDir);
            ReloadPageAsync();
        }
        InitializeComponent();        
    }
    private async void ondeletefileclick(object sender, EventArgs e)
    {
        try
        {
            if (DeletePhotoName.Text != null && SaveFolder.Text != null)
            {
                string filePath = $"{MainPath + SaveFolder.Text}/{DeletePhotoName.Text}.jpg";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    ReloadPageAsync();
                }
                else
                    await DisplayAlert("Alert", "File Dose not Exist", "OK");
            }
            else
                await DisplayAlert("Alert", "You have to wirte photo name & Choose Save Folder first", "OK");
        }  
        catch (Exception)
        {
            await DisplayAlert("Alert", "Folder Dose not Exist", "OK");
        }
    }
    private async void onflodercreateclick(object sender, EventArgs e)
    {
        try
        {
            if(CreateFolderName.Text != null)
                if (!Directory.Exists($"{MainPath + CreateFolderName.Text}"))
                {
                    string mainDir = Path.Combine(MainPath, CreateFolderName.Text);
                    System.IO.Directory.CreateDirectory(mainDir);
                    ReloadPageAsync();
                }
                else
                {
                    await DisplayAlert("Alert", "Folder is already Exist", "OK");
                }
            else
                await DisplayAlert("Alert", "You have to wirte Folder Name first", "OK");
        }
        catch (Exception)
        {
            await DisplayAlert("Alert", "Some Thing Worng", "OK");
        }

    }
    private async void onfloderDeleteclick(object sender, EventArgs e)
    {       
        try
        {
            if (DeleteFolderName.Text != null)
                if (Directory.Exists($"{MainPath + DeleteFolderName.Text}"))
                {
                    string mainDir = Path.Combine(MainPath, DeleteFolderName.Text);
                    System.IO.Directory.Delete(mainDir);
                    ReloadPageAsync();
                }
                else
                {
                    await DisplayAlert("Alert", "Folder is not Exist", "OK");
                }
            else
                await DisplayAlert("Alert", "You have to wirte Folder Name first", "OK");
        }
        catch (Exception)
        {
            await DisplayAlert("Alert", "You Have to empty folder First", "OK");
        }
    }
    private async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        try
        {
            if (CreatePhotoName.Text != null && FolderToSave.Text != null)
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                    if (photo != null)
                    {
                        // save the file into local storage
                        string localFilePath = Path.Combine($"{MainPath+FolderToSave.Text}/", $"{CreatePhotoName.Text}.jpg");

                        using Stream sourceStream = await photo.OpenReadAsync();
                        using FileStream localFileStream = File.OpenWrite(localFilePath);

                        await sourceStream.CopyToAsync(localFileStream);
                        ReloadPageAsync();
                    }
                }
            }
            else
                await DisplayAlert("Alert", "You have to wirte photo name & Choose Floder to save first", "OK");
        }
        catch (Exception)
        {
            await DisplayAlert("Alert", "You have to Choose Folder Already Exist", "OK");
        }
    }
    public void ReloadPageAsync()
    {
        var nextPage = new MainPage();
        Navigation.PushAsync(nextPage);
    }
}

