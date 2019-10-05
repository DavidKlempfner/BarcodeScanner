using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Color = System.Drawing.Color;

namespace BarcodeScanner
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Bitmap image = new Bitmap(ImagePathTextBox.Text);
            List<bool> barCodeHorizontalLine = GetBarCodeHorizontalLine(image);
            List<bool> barCodeVerticalLines = GetBarCodeVerticalLines(barCodeHorizontalLine);
        }

        private List<bool> GetBarCodeVerticalLines(List<bool> barcodeHorizontalLine)
        {
            int lengthOfLine = GetLengthOfLine(barcodeHorizontalLine);
            List<bool> barCodeVerticalLines = new List<bool>();
            for (int i = 0; i < barcodeHorizontalLine.Count; i += lengthOfLine)
            {
                barCodeVerticalLines.Add(barcodeHorizontalLine[i]);
            }
            return barCodeVerticalLines;
        }

        private int GetLengthOfLine(List<bool> barcodeHorizontalLine)
        {
            return barcodeHorizontalLine.IndexOf(false);
        }

        private List<bool> GetBarCodeHorizontalLine(Bitmap image)
        {
            List<bool> barcodeHorizontalLine = new List<bool>();

            int firstLineIndex = GetFirstLineIndex(image);
            int lastLineIndex = GetLastLineIndex(image);

            for (int i = firstLineIndex; i <= lastLineIndex; i++)
            {
                Color pixel = image.GetPixel(i, image.Height / 2);
                barcodeHorizontalLine.Add(IsPixelBlack(pixel));                
            }

            return barcodeHorizontalLine;
        }

        private int GetFirstLineIndex(Bitmap image)
        {
            for (int currentXIndex = 0; currentXIndex < image.Width; currentXIndex++)
            {
                Color pixel = image.GetPixel(currentXIndex, image.Height / 2);
                if (IsPixelBlack(pixel))
                {
                    return currentXIndex;
                }
            }

            return 0;
        }

        private int GetLastLineIndex(Bitmap image)
        {
            for (int currentXIndex = image.Width - 1; currentXIndex >= 0; currentXIndex--)
            {
                Color pixel = image.GetPixel(currentXIndex, image.Height / 2);
                if (IsPixelBlack(pixel))
                {
                    return currentXIndex;
                }
            }

            return 0;
        }

        private bool IsPixelBlack(Color pixel)
        {
            return pixel.Name == "ff000000";
        }
    }
}
