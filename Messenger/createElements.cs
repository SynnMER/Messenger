using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Messenger
{
    //класс создания элемента TextBlock
    internal class createElements
    {
        public TextBlock textBlock = new TextBlock();
        public createElements()
        {
            createElement();
        }
        public void createElement()
        {
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
            TextWrapping textWrapping = TextWrapping.Wrap;
            textBlock.MinHeight = 30;
            textBlock.Background = new SolidColorBrush(Colors.Transparent);
            textBlock.Margin = new Thickness(40,0,0,0);
            textBlock.Width = 415;
            textBlock.HorizontalAlignment = horizontalAlignment;
            textBlock.TextWrapping = textWrapping;
            textBlock.FontSize = 18;
            textBlock.FontFamily = new FontFamily("Lucida Sans");
            textBlock.Foreground = new SolidColorBrush(Colors.White);
            textBlock.Text = "efwwe";
        }
    }
}
