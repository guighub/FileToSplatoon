using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace FileToSplatoon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            int imageWidth = 320;
            int imageHeight = 120;
            byte colorBlack = 0xFF;
            byte colorWhite = 0x00;
            byte numberZero = 0x30;
            byte numberOne = 0x31;
            BitmapPalette palette = BitmapPalettes.Halftone256;
            var openFile = new Microsoft.Win32.OpenFileDialog();
            bool? openedFile = openFile.ShowDialog();

            if (openedFile == true)
            {
                FileStream fileStream = new FileStream(openFile.FileName, FileMode.Open, FileAccess.Read);
                if (fileStream.Length <= 4800)
                {
                    byte[] bitData = new byte[imageWidth * imageHeight];
                    byte[] bitDataAscii = new byte[imageWidth * imageHeight];
                    fileStream.Read(bitData);
                    fileStream.Position = 0;
                    int byteNum = 0;
                    for (int i = 0; i < 4800; i++)
                    {
                        byte curByte = (byte)fileStream.ReadByte();
                        for (int bit = 0; bit < 8; bit++)
                        {
                            if ((curByte & (1 << bit)) != 0)
                            {
                                if (!InvertCheckBox.IsChecked ?? false)
                                {
                                    bitData[bit + byteNum] = colorWhite;
                                    bitDataAscii[bit + byteNum] = numberOne;
                                }
                                else
                                {
                                    bitData[bit + byteNum] = colorBlack;
                                    bitDataAscii[bit + byteNum] = numberZero;
                                }
                            }
                            else
                            {
                                if (!InvertCheckBox.IsChecked ?? false)
                                {
                                    bitData[bit + byteNum] = colorBlack;
                                    bitDataAscii[bit + byteNum] = numberZero;
                                }
                                else
                                {
                                    bitData[bit + byteNum] = colorWhite;
                                    bitDataAscii[bit + byteNum] = numberOne;
                                }
                            }
                        }
                        byteNum += 8;
                    }
                    fileStream.Close();

                    BitmapSource image = BitmapSource.Create(
                            imageWidth,
                            imageHeight,
                            96,
                            96,
                            PixelFormats.Indexed8,
                            palette,
                            bitData,
                            imageWidth);
                    FileStream imageStream = new FileStream(openFile.FileName + ".png", FileMode.Create);
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Interlace = PngInterlaceOption.On;
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(imageStream);
                    imageStream.Close();

                    FileStream asciiOut = File.Create(openFile.FileName + ".txt");
                    asciiOut.Write(bitDataAscii, 0, bitDataAscii.Length);
                    asciiOut.Close();
                    LogText.Content = "Complete!";
                    LogText.Foreground = Brushes.Green;
                }
                else
                {
                    LogText.Content = "File too big!";
                    LogText.Foreground = Brushes.Red;
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
