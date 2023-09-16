using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Threading.Tasks;
using Microsoft.UI;

namespace drawtest
{
    public sealed partial class MainWindow : Window
    {
        private bool IsMouseDown;
        private readonly List<Point> Path;
        readonly SolidColorBrush strokebrush;
        Microsoft.UI.Xaml.Shapes.Path drawnPath;
        PathFigure pathFigure;


        public MainWindow()
        {
            this.InitializeComponent();
            strokebrush = new SolidColorBrush
            {
                Color = Colors.Red
            };
            Path = new List<Point>();
        }


        //________________________________________________________________________________________________________________//


        private void Canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            IsMouseDown = true;
            PointerPoint mousePos = e.GetCurrentPoint(sender as UIElement);
            Path.Add(new Point(mousePos.Position.X, mousePos.Position.Y));

            PathGeometry pathGeometry = new();
            drawnPath = new();
            pathFigure = new()
            {
                StartPoint = mousePos.Position
            };
            pathGeometry.Figures.Add(pathFigure);
            drawCanvas.Children.Add(drawnPath);


            drawnPath.Stroke = strokebrush;
            drawnPath.StrokeThickness = 3;
            drawnPath.Data = pathGeometry;
        }

        private void Canvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            IsMouseDown = false;
            Path.Clear();

        }

        private void drawCanvas_PointerMoved(object sender, PointerRoutedEventArgs e) // <--- fires a lot, takes tons of cpu
        {
            if (IsMouseDown == true)
            {
                PointerPoint mousePos = e.GetCurrentPoint(sender as UIElement);
                LineSegment line = new()
                {
                    Point = mousePos.Position
                };
                pathFigure.Segments.Add(line);
            }
        }

    }
}
