using System.Text;
using System.Text.Json;
#if ANDROID
using Android.Media;
#endif
using _34455y3x1f.c0r3._1n73rf4c35;
using _34455y3x1f._57473._4d4p73r5;

namespace e45y3x1f.mobile;

public partial class MainPage : ContentPage
{
    private string _currentImagePath = "";

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSelectImage(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Select an image"
            });

            if (result != null)
            {
                _currentImagePath = result.FullPath;
                string filename = Path.GetFileName(_currentImagePath);
                ImageStatusLabel.Text = $"Selected: {filename}";
                ExifDataEditor.Text = $"[Image loaded: {filename}]\n\nClick 'View EXIF' to extract metadata.";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to pick image: {ex.Message}", "OK");
        }
    }

    private async void OnViewExif(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_currentImagePath))
        {
            await DisplayAlert("No Image", "Please select an image first.", "OK");
            return;
        }

        try
        {
            string exifData = await ExtractExifDataAsync(_currentImagePath);
            ExifDataEditor.Text = exifData;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to extract EXIF: {ex.Message}", "OK");
        }
    }

    private async Task<string> ExtractExifDataAsync(string imagePath)
    {
        return await Task.Run(() => ExtractExifDataNative(imagePath));
    }

    private string ExtractExifDataNative(string imagePath)
    {
        StringBuilder exifInfo = new StringBuilder();

        try
        {
            // Get file info
            FileInfo fileInfo = new FileInfo(imagePath);
            exifInfo.AppendLine("=== FILE INFORMATION ===");
            exifInfo.AppendLine($"Name: {fileInfo.Name}");
            exifInfo.AppendLine($"Size: {(fileInfo.Length / 1024.0):F2} KB");
            exifInfo.AppendLine($"Modified: {fileInfo.LastWriteTime:yyyy-MM-dd HH:mm:ss}");
            exifInfo.AppendLine();

#if ANDROID
            // Android native EXIF extraction
            exifInfo.AppendLine("=== EXIF DATA (Android Native) ===");
            try
            {
                ExifInterface exifInterface = new ExifInterface(imagePath);
                
                // Camera info
                string make = exifInterface.GetAttribute(ExifInterface.TagMake) ?? "Unknown";
                string model = exifInterface.GetAttribute(ExifInterface.TagModel) ?? "Unknown";
                string datetime = exifInterface.GetAttribute(ExifInterface.TagDatetime) ?? "Unknown";
                
                exifInfo.AppendLine($"Make: {make}");
                exifInfo.AppendLine($"Model: {model}");
                exifInfo.AppendLine($"DateTime: {datetime}");
                
                // Image dimensions
                string width = exifInterface.GetAttribute(ExifInterface.TagImageWidth) ?? "Unknown";
                string height = exifInterface.GetAttribute(ExifInterface.TagImageLength) ?? "Unknown";
                exifInfo.AppendLine($"Width: {width}");
                exifInfo.AppendLine($"Height: {height}");
                
                // GPS info
                float[] latLong = new float[2];
                if (exifInterface.GetLatLong(latLong))
                {
                    exifInfo.AppendLine($"GPS Latitude: {latLong[0]}");
                    exifInfo.AppendLine($"GPS Longitude: {latLong[1]}");
                }
                else
                {
                    exifInfo.AppendLine("GPS: Not available");
                }
                
                exifInfo.AppendLine();
            }
            catch (Exception ex)
            {
                exifInfo.AppendLine($"Error reading EXIF: {ex.Message}");
                exifInfo.AppendLine();
            }
#else
            // Fallback for non-Android platforms
            exifInfo.AppendLine("=== EXIF DATA ===");
            exifInfo.AppendLine("EXIF extraction not available on this platform");
            exifInfo.AppendLine();
#endif

            // File format info
            exifInfo.AppendLine("=== FILE FORMAT ===");
            string extension = Path.GetExtension(imagePath).ToUpper();
            exifInfo.AppendLine($"Extension: {extension}");
            
            // Read first few bytes to identify format
            byte[] headerBytes = new byte[8];
            using (FileStream fs = File.OpenRead(imagePath))
            {
                fs.Read(headerBytes, 0, 8);
            }
            
            string format = IdentifyImageFormat(headerBytes);
            exifInfo.AppendLine($"Format: {format}");
        }
        catch (Exception ex)
        {
            exifInfo.AppendLine($"Error: {ex.Message}");
        }

        return exifInfo.ToString();
    }

    private string IdentifyImageFormat(byte[] header)
    {
        if (header.Length < 4) return "Unknown";
        
        // JPEG: FF D8 FF
        if (header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            return "JPEG";
        
        // PNG: 89 50 4E 47
        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return "PNG";
        
        // GIF: 47 49 46
        if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46)
            return "GIF";
        
        // WebP: RIFF ... WEBP
        if (header[0] == 0x52 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x46)
            return "WebP";
        
        // BMP: 42 4D
        if (header[0] == 0x42 && header[1] == 0x4D)
            return "BMP";
        
        return "Unknown Format";
    }

    private void OnClear(object sender, EventArgs e)
    {
        _currentImagePath = "";
        ImageStatusLabel.Text = "No image selected";
        ExifDataEditor.Text = "[Select an image and click View EXIF to extract metadata]";
    }
}

