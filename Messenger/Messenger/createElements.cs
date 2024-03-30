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
        public TextBlock CreateTextBlock(string text)
        {
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Right;
            TextWrapping textWrapping = TextWrapping.Wrap;
            textBlock.MinHeight = 30;
            textBlock.Background = new SolidColorBrush(Colors.White);
            textBlock.Margin = new Thickness(40,0,0,0);
            textBlock.MinWidth = 20;
            textBlock.MaxWidth = 415;
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            textBlock.Text = text;
            textBlock.Padding = new Thickness(4);
            textBlock.HorizontalAlignment = horizontalAlignment;
            textBlock.TextWrapping = textWrapping;
            textBlock.FontSize = 18;
            textBlock.FontFamily = new FontFamily("Lucida Sans");
            textBlock.TextAlignment = TextAlignment.Right;
            //Border border = new Border();
            //border.BorderThickness = new Thickness(1); // Установка толщины границы
            //border.CornerRadius = new CornerRadius(15);
            //border.Background = new SolidColorBrush(Colors.White);
            //if (border.Parent != null)
            //{
            //    var parent = (ItemsControl)border.Parent;
            //    parent.Items.Remove(border);
            //}

            return textBlock;
        }
    }
}
