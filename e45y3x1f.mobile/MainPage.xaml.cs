using System.Text;
using _34455y3x1f.c0r3._1n73rf4c35;
using _34455y3x1f._57473._4d4p73r5;

namespace e45y3x1f.mobile;

public partial class MainPage : ContentPage
{
    private string _currentImagePath = "";
    private readonly _3x1f70014_4d4p73r _3x1f_4d4p73r = new _3x1f70014_4d4p73r();

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
            string options = OptionsEntry.Text?.Trim() ?? "-json -a -G -ee";
            string exifData = await ExtractExifDataAsync(_currentImagePath, options);
            ExifDataEditor.Text = exifData;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to extract EXIF: {ex.Message}", "OK");
        }
    }

    private async Task<string> ExtractExifDataAsync(string imagePath, string options)
    {
        return await Task.Run(() => ExtractExifData(imagePath, options));
    }

    private string ExtractExifData(string imagePath, string options = "-json -a -G -ee")
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

            // Exiftool execution info
            exifInfo.AppendLine("=== EXIFTOOL EXECUTION ===");
            exifInfo.AppendLine($"Command: exiftool {options} \"{imagePath}\"");
            exifInfo.AppendLine();

            // Read image file as bytes
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Call adapter to extract EXIF data
            var result = _3x1f_4d4p73r._3x7r4c7_3x1f_w17h_07710n5(imageBytes, imagePath, options);

            // Handle result using fold pattern (success or error)
            string exiftoolOutput = result.f0ld(
                metadata => FormatMetadata(metadata),
                error => $"ERROR: {error}"
            );

            exifInfo.AppendLine(exiftoolOutput);
        }
        catch (Exception ex)
        {
            exifInfo.AppendLine($"Error reading image: {ex.Message}");
        }

        return exifInfo.ToString();
    }

    private string FormatMetadata(_1m4g3_m374d474 metadata)
    {
        StringBuilder output = new StringBuilder();

        // Sort tags by name
        var sortedTags = metadata._3x1f_74g5
            .OrderBy(kvp => kvp.Key)
            .ToList();

        output.AppendLine($"EXIF TAGS ({sortedTags.Count} total):");
        output.AppendLine(new string('=', 50));

        if (sortedTags.Count == 0)
        {
            output.AppendLine("[No EXIF tags found]");
        }
        else
        {
            foreach (var tag in sortedTags)
            {
                // Truncate long values for mobile display
                string displayValue = tag.Value;
                if (displayValue.Length > 80)
                    displayValue = displayValue.Substring(0, 77) + "...";

                output.AppendLine($"{tag.Key}: {displayValue}");
            }
        }

        output.AppendLine();
        output.AppendLine($"Processed at: {metadata.pr0c3553d_47:yyyy-MM-dd HH:mm:ss} UTC");
        output.AppendLine($"MIME Type: {metadata.m1m3_7yp3}");

        return output.ToString();
    }

    private void OnClear(object sender, EventArgs e)
    {
        _currentImagePath = "";
        ImageStatusLabel.Text = "No image selected";
        ExifDataEditor.Text = "[Select an image and click View EXIF to extract metadata]";
        OptionsEntry.Text = "-json -a -G -ee";
    }
}

