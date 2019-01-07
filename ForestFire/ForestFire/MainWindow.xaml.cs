using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ForestFire
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly WriteableBitmap _writeableBitmap;
        private Forest _forest;
        private const int _imageSize = 1000;
        private const int _forestSize = 100;
        private const int _treeRatio = 66;
        private int _squareSize = _imageSize / _forestSize;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
            _writeableBitmap = BitmapFactory.New(_imageSize, _imageSize);
            Earth.Source = _writeableBitmap;
            KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            ExecuteTime(() => _forest.Iterate(), "Iterate");

            DrawForest(_forest);

            Title = $"Generate {_stopwatch.ElapsedMilliseconds}ms || Untouched {_forest.UntouchedTrees.Count} || Fires {_forest.FireTrees.Count}";

            if (_forest.FireTrees.Count == 0)
            {
                InitForest();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitForest();
        }

        private void InitForest()
        {
            var random = new Random();
            var fireRow = random.Next(_forestSize);
            var fireColumn = random.Next(_forestSize);

            _forest = new Forest(fireRow, fireColumn, _forestSize, _treeRatio);

            InitDrawForest(_forest);
        }

        private void InitDrawForest(Forest forest)
        {
            using (_writeableBitmap.GetBitmapContext())
            {
                _writeableBitmap.Clear();
                foreach (var item in forest.UntouchedTrees)
                {
                    DrawTree(item);
                }

                foreach (var item in forest.FireTrees)
                {
                    DrawTree(item);
                }

                foreach (var item in forest.AshTrees)
                {
                    DrawTree(item);
                }
            }
        }

        private void DrawForest(Forest forest)
        {
            using (_writeableBitmap.GetBitmapContext())
            {
                foreach (var item in forest.FireTrees)
                {
                    DrawTree(item);
                }

                foreach (var item in forest.AshTrees)
                {
                    DrawTree(item);
                }
            }
        }

        private void DrawTree(ITree tree)
        {
            var rowPosition = tree.Position.Row * _squareSize;
            var columnPostion = tree.Position.Column * _squareSize;

            var treeColor = (Color)ColorConverter.ConvertFromString(tree.Color);
            _writeableBitmap.FillRectangle(rowPosition, columnPostion, rowPosition + _squareSize, columnPostion + _squareSize, treeColor);
        }

        private void ExecuteTime(Action action, string message = null)
        {
            _stopwatch.Restart();
            action.Invoke();
            _stopwatch.Stop();
            Console.WriteLine($"{message} {_stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
