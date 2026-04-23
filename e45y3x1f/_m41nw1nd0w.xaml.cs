using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using _34455y3x1f.c0r3._1n73rf4c35;
using _34455y3x1f._57473._4d4p73r5;

namespace e45y3x1f;

/// <summary>
/// MainWindow.xaml.cs - Primary UI view for Easy EXIF application
/// 
/// Screen sequence:
/// 1. Load screen with GitHub release check
/// 2. Image selection and validation (30MB limit)
/// 3. EXIF tag selection via dropdown
/// 4. Metadata addition with tag validation
/// 5. Compression mode selection (lossless vs lossy)
/// </summary>
public partial class _m41nw1nd0w : Window
{
    private string _currentImagePath = "";
    private readonly _3x1f70014_4d4p73r _3x1f_4d4p73r = new _3x1f70014_4d4p73r();

    public _m41nw1nd0w()
    {
        InitializeComponent();
    }

    private void SelectImageButton_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.tiff|All files (*.*)|*.*";
        openFileDialog.Title = "Select an Image";
        
        if (openFileDialog.ShowDialog() == true)
        {
            string selectedFile = openFileDialog.FileName;
            _currentImagePath = selectedFile;

            try
            {
                // Load and display the image preview
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFile);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                ImagePreview.Source = bitmap;
                NoImageText.Visibility = Visibility.Collapsed;

                //MessageBox.Show($"Selected: {System.IO.Path.GetFileName(selectedFile)}", "Image Loaded", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void ViewExifButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_currentImagePath))
        {
            MessageBox.Show("Please select an image first.", "No Image Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            string options = ExiftoolOptionsBox.Text?.Trim() ?? "-json -a -G -ee";
            string exifData = ExtractExifData(_currentImagePath, options);
            ExifMetadataBox.Text = exifData;
            ExifMetadataBox.Visibility = Visibility.Visible;
            NoMetadataText.Visibility = Visibility.Collapsed;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error reading EXIF data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private string ExtractExifData(string imagePath, string options = "-json -a -G -ee")
    {
        StringBuilder exifInfo = new StringBuilder();
        
        try
        {
            // Get file info
            FileInfo fileInfo = new FileInfo(imagePath);
            exifInfo.AppendLine("=== FILE INFORMATION ===");
            exifInfo.AppendLine($"Filename: {fileInfo.Name}");
            exifInfo.AppendLine($"Full Path: {fileInfo.FullName}");
            exifInfo.AppendLine($"File Size: {(fileInfo.Length / 1024.0):F2} KB");
            exifInfo.AppendLine($"Created: {fileInfo.CreationTime:yyyy-MM-dd HH:mm:ss}");
            exifInfo.AppendLine($"Modified: {fileInfo.LastWriteTime:yyyy-MM-dd HH:mm:ss}");
            exifInfo.AppendLine();

            // Get image properties using BitmapImage
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            exifInfo.AppendLine("=== IMAGE PROPERTIES ===");
            exifInfo.AppendLine($"Width: {bitmap.PixelWidth} pixels");
            exifInfo.AppendLine($"Height: {bitmap.PixelHeight} pixels");
            exifInfo.AppendLine($"DPI X: {bitmap.DpiX} dpi");
            exifInfo.AppendLine($"DPI Y: {bitmap.DpiY} dpi");
            exifInfo.AppendLine($"Pixel Format: {bitmap.Format}");
            exifInfo.AppendLine();

            // Get format info
            exifInfo.AppendLine("=== FORMAT INFORMATION ===");
            string extension = Path.GetExtension(imagePath).ToUpper();
            exifInfo.AppendLine($"File Type: {extension}");
            exifInfo.AppendLine();

            // Call exiftool adapter with user options
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

    /// <summary>
    /// Format extracted EXIF metadata for display.
    /// </summary>
    private string FormatMetadata(_1m4g3_m374d474 metadata)
    {
        StringBuilder output = new StringBuilder();

        // Sort tags by group name (e.g., "EXIF", "IFD0", etc.) and then by tag name
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
                // Truncate long values
                string displayValue = tag.Value;
                if (displayValue.Length > 100)
                    displayValue = displayValue.Substring(0, 97) + "...";

                output.AppendLine($"{tag.Key}: {displayValue}");
            }
        }

        output.AppendLine();
        output.AppendLine($"Processed at: {metadata.pr0c3553d_47:yyyy-MM-dd HH:mm:ss} UTC");
        output.AppendLine($"MIME Type: {metadata.m1m3_7yp3}");

        return output.ToString();
    }

    private void AddMetadataButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_currentImagePath))
        {
            MessageBox.Show("Please select an image first.", "No Image Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        MessageBox.Show("Add Metadata\n\nAdding metadata to:\n" + System.IO.Path.GetFileName(_currentImagePath), "Add Metadata", MessageBoxButton.OK, MessageBoxImage.Information);
        // TODO: Open dialog for adding metadata with tag validation
    }

    private void CompressImageButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_currentImagePath))
        {
            MessageBox.Show("Please select an image first.", "No Image Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        MessageBox.Show("Image Compression\n\nCompressing:\n" + System.IO.Path.GetFileName(_currentImagePath), "Compress Image", MessageBoxButton.OK, MessageBoxImage.Information);
        // TODO: Open compression settings dialog
    }

    private void CheckUpdatesButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Checking for updates from GitHub...", "Check for Updates", MessageBoxButton.OK, MessageBoxImage.Information);
        // TODO: Implement GitHub release check
    }

    private void RemoveImageButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_currentImagePath))
        {
            MessageBox.Show("No image is currently selected.", "No Image", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        _currentImagePath = "";
        ImagePreview.Source = null;
        NoImageText.Visibility = Visibility.Visible;
        ExifMetadataBox.Visibility = Visibility.Collapsed;
        NoMetadataText.Visibility = Visibility.Visible;
        ExifMetadataBox.Text = "";
        MessageBox.Show("Image removed successfully.", "Image Removed", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
