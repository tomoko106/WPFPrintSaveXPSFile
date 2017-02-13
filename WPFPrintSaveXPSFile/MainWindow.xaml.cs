using System;
using System.Collections.Generic;
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

namespace WPFPrintSaveXPSFile
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FixedDocument_Loaded(object sender, RoutedEventArgs e)
        {

            FixedDocument fixedDocument = sender as FixedDocument;

            ReportCtrl myRepot = new ReportCtrl();
            myRepot.DataContext = this;
            myRepot.HorizontalAlignment = HorizontalAlignment.Center;
            myRepot.VerticalAlignment = VerticalAlignment.Center;

            Grid grid = new Grid();
            grid.Children.Add(myRepot);

            FixedPage fixedPage = new FixedPage();
            fixedPage.Children.Add(grid);


            PageContent pc = new PageContent();
            pc.Child = fixedPage;
            fixedDocument.Pages.Add(pc);
        }
    }
}
