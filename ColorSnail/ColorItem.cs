﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;


namespace ColorSnail
{
    class ColorItem : WrapPanel
    {
        private MainWindow father;

        private Color       argb;
        public  Color       Argb
        {
            set
            {
                argb = value;
                colorButton.Background = new System.Windows.Media.SolidColorBrush(argb);
                tbColorCode.Text = argb.ToString();
            }
            get
            {
                return argb;
            }
        }

        private TextBlock colorButton = new TextBlock()
        {
            Width = 20,
            Height = 20,
            Background = System.Windows.Media.Brushes.DarkGray,
            VerticalAlignment = System.Windows.VerticalAlignment.Bottom
        };

        private TextBlock tbColorCode = new TextBlock()
        {
            Width = 230,
            Height = 20,
            FontSize = 16,
            Margin = new System.Windows.Thickness(5),
            Foreground = System.Windows.Media.Brushes.White,
            Text = System.Windows.Media.Brushes.DarkGray.ToString(),
            VerticalAlignment = System.Windows.VerticalAlignment.Bottom

        };

        private Button closeButton = new Button()
        {
            Width = 10,
            Height = 20,
            FontSize = 10,
            Content = "X",
            Margin = new System.Windows.Thickness(5),
            Foreground = System.Windows.Media.Brushes.White,
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            VerticalAlignment = System.Windows.VerticalAlignment.Bottom

        };

        private void close()
        {
            father.removeItem(this);
        }

        private void copyColor()
        {
            System.Windows.Clipboard.SetText(argb.ToString());
        }

        public ColorItem(MainWindow _father) : base()
        {
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            this.father = _father;
            this.Margin = new System.Windows.Thickness(5, 0, 0, 5);

            Border b = new Border()
            {
                BorderBrush = System.Windows.Media.Brushes.White,
                BorderThickness = new System.Windows.Thickness(1),
                Margin = new System.Windows.Thickness(4),
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom
            };
            b.Child = colorButton;
            this.Children.Add(b);
            this.Children.Add(tbColorCode);
            this.Children.Add(closeButton);
            
            closeButton.Click += CloseButton_Click;
            colorButton.MouseUp += ColorButton_Click;
        }

        private void ColorButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.copyColor();
            father.prompt("Color copied");
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.close();
        }

    }
}
