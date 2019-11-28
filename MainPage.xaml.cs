using Syncfusion.UI.Xaml.CellGrid;
using Syncfusion.UI.Xaml.CellGrid.Helpers;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TemplateDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Random random = new Random();
        public MainPage()
        {
            this.InitializeComponent();
            grid.ColumnWidths[0] = 35;

            grid.CellContextMenu = GetCellContextMenu();
            grid.CellContextMenuOpening += CellGrid_CellContextMenuOpening;

            grid.Model.QueryCellInfo += this.Model_QueryCellInfo;
        }
        private void CellGrid_CellContextMenuOpening(object sender, CellContextMenuOpeningEventArgs e)
        {
            if (e.Cell.ColumnIndex == 0 && e.Cell.RowIndex > 0) //For Rows
                e.CellContextMenu = GetRowCellMenu();
            else if (e.Cell.RowIndex == 0 && e.Cell.ColumnIndex > 0) //For columns
                e.CellContextMenu = GetColumnCellMenu();
            else
                e.CellContextMenu = GetCellContextMenu(); //For Cells
        }

        private void Model_QueryCellInfo(object sender, Syncfusion.UI.Xaml.CellGrid.Styles.GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex > 0 && e.Cell.ColumnIndex > 0)
            {
                if (e.Cell.ColumnIndex == 1)
                    e.Style.CellValue = name1[e.Cell.RowIndex % 6];
                else if (e.Cell.ColumnIndex == 2)
                    e.Style.CellValue = country[e.Cell.RowIndex % 6];
                else if (e.Cell.ColumnIndex == 3)
                    e.Style.CellValue = city[e.Cell.RowIndex % 6];
                else if (e.Cell.ColumnIndex == 4)
                    e.Style.CellValue = scountry[e.Cell.RowIndex % 6];
                else if (e.Cell.ColumnIndex == 5)
                    e.Style.CellValue = DateTime.Now;
            }
            if (e.Cell.ColumnIndex == 0 && e.Cell.RowIndex > 0)
            {
                e.Style.CellValue = e.Style.RowIndex;
            }
            if (e.Cell.RowIndex == 0 && e.Cell.ColumnIndex > 0)
            {
                e.Style.CellValue = columnNames[e.Cell.ColumnIndex - 1];
            }
            if (e.Cell.RowIndex == 0 || e.Style.ColumnIndex == 0)
            {
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
                e.Style.Font.FontFamily = new FontFamily("Segoe UI");
                e.Style.Font.FontSize = 14f;
                e.Style.Background = new SolidColorBrush(Colors.LightGray);
            }
        }
        private ContextMenu GetCellContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            for (int i = 1; i < grid.ColumnCount; i++)
            {
                MenuFlyoutItem query = new MenuFlyoutItem { Text = grid.Model[grid.CurrentCell.RowIndex, i].Text, Height = 50, VerticalAlignment = VerticalAlignment.Center };
                contextMenu.Items?.Add(query);
            }
            return contextMenu;
        }
        private ContextMenu GetRowCellMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            for (int i = 0; i < 5; i++)
            {
                MenuFlyoutItem query = new MenuFlyoutItem { Text = string.Format("Row{0}", i), Height = 50, VerticalAlignment = VerticalAlignment.Center };
                contextMenu.Items?.Add(query);
            }
            return contextMenu;
        }
        public ContextMenu GetColumnCellMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            for (int i = 0; i < 5; i++)
            {
                MenuFlyoutItem query = new MenuFlyoutItem { Text = columnNames[i], Height = 50, VerticalAlignment = VerticalAlignment.Center };
                contextMenu.Items?.Add(query);
            }
            return contextMenu;
        }

        #region "DataTable"
        string[] name1 = new string[] { "John", "Peter", "Smith", "Jay", "Krish", "Mike" };
        string[] country = new string[] { "UK", "USA", "Pune", "India", "China", "England" };
        string[] city = new string[] { "Graz", "Resende", "Bruxelles", "Aires", "Rio de janeiro", "Campinas" };
        string[] scountry = new string[] { "Brazil", "Belgium", "Austria", "Argentina", "France", "Beiging" };
        string[] columnNames = new string[] { "Name", "Country", "City", "Scountry", "Date" };
    }
    #endregion
}