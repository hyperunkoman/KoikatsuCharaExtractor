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

namespace KoikatsuCharaExtractor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string getFileName(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files != null && files.Length > 0) return files[0];
            }
            return null;
        }

        private void Input_Drop(object sender, DragEventArgs e)
        {
            var f = getFileName(e);
            if (f != null) InputFileName.Text = f;
        }

        private void Output_Drop(object sender, DragEventArgs e)
        {
            var f = getFileName(e);
            if (f != null) OutPutFileName.Text = f;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var kkex = new KoikatsuSaveExtractor(InputFileName.Text);
                kkex.SearchAndSave(OutPutFileName.Text);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
