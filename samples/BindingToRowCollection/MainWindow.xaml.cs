using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using Novacta.Analytics;

namespace BindingToRowCollection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly DoubleMatrix matrix =
            DoubleMatrix.Dense(
                numberOfRows: 2,
                numberOfColumns: 4,
                data: new double[8] { 100, 200, 300, 400, 500, 600, 700, 800 },
                storageOrder: StorageOrder.RowMajor);

        readonly DoubleMatrixRowCollection rowCollection;

        public MainWindow()
        {
            InitializeComponent();

            this.matrixBlock.Text = this.matrix.ToString();

            this.gridBoundToIndexer.CurrentCellChanged += 
                this.GridBoundToIndexer_CurrentCellChanged;

            this.gridBoundToDataProperties.CurrentCellChanged +=
                this.GridBoundToDataProperties_CurrentCellChanged;

            this.rowCollection = this.matrix.AsRowCollection();
            
            this.rowCollection.XDataColumn = 
                Convert.ToInt32(((ListBoxItem)this.xDataColumn.SelectedItem).Content);
            this.rowCollection.YDataColumn = 
                Convert.ToInt32(((ListBoxItem)this.yDataColumn.SelectedItem).Content);
            this.rowCollection.ZDataColumn = 
                Convert.ToInt32(((ListBoxItem)this.zDataColumn.SelectedItem).Content);
            this.xDataColumn.SelectionChanged += this.XDataColumn_SelectionChanged;
            this.yDataColumn.SelectionChanged += this.YDataColumn_SelectionChanged;
            this.zDataColumn.SelectionChanged += this.ZDataColumn_SelectionChanged;

            this.gridBoundToIndexer.ItemsSource = this.rowCollection;
            this.gridBoundToDataProperties.ItemsSource = this.rowCollection;

            double gridWidth = this.gridBoundToIndexer.Width;
            double columnWidth = .9 * gridWidth / this.matrix.NumberOfColumns;

            for (int j = 0; j < this.matrix.NumberOfColumns; j++)
            {
                DataGridTextColumn column = new DataGridTextColumn
                {
                    Binding = new Binding("[" + j + "]")
                    {
                        Mode = BindingMode.TwoWay,
                    },
                    Header = j,
                    Width = columnWidth
                };

                this.gridBoundToIndexer.Columns.Add(column);
            }
        }

        private void ZDataColumn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var source = (ListBox)e.Source;
            
            this.rowCollection.ZDataColumn =
                Convert.ToInt32(((ListBoxItem)source.SelectedItem).Content);
        }

        private void YDataColumn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var source = (ListBox)e.Source;

            this.rowCollection.YDataColumn =
                Convert.ToInt32(((ListBoxItem)source.SelectedItem).Content);
        }

        private void XDataColumn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var source = (ListBox)e.Source;

            this.rowCollection.XDataColumn =
                Convert.ToInt32(((ListBoxItem)source.SelectedItem).Content);
        }

        private void GridBoundToIndexer_CurrentCellChanged(object sender, EventArgs e)
        {
            this.matrixBlock.Text = this.matrix.ToString();
            this.gridBoundToDataProperties.Items.Refresh();
        }

        private void GridBoundToDataProperties_CurrentCellChanged(object sender, EventArgs e)
        {
            this.matrixBlock.Text = this.matrix.ToString();
            this.gridBoundToIndexer.Items.Refresh();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            double gridWidth = this.gridBoundToDataProperties.ActualWidth;
            double columnWidth = .9 * gridWidth / 3.0;
            foreach (var column in this.gridBoundToDataProperties.Columns)
            {
                column.Width = columnWidth;
            }
        }
    }
}
