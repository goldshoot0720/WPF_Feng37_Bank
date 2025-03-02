using System;
using System.Collections.Generic;
using System.IO;
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

namespace Wpf_Feng37_Bank
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        int[] bank_savings = new int[7];
        string dir_name = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).ToString() + "/Local/WPF_Feng37_Bank/";
        string file_name = "bank_savings.txt";

        private int Calc_Sum_Saving()
        {
            int sum_saving = 0;
            for (int i = 0; i < 7; i++)
            {
                sum_saving += bank_savings[i];
            }
            return sum_saving;
        }


        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (combobox1.SelectedIndex == -1)
            {
                label1.Content = "請選擇金融機構";
                return;
            }
            else
            {
                label1.Content = "";
                int textbox1Text;
                if (!int.TryParse(textbox1.Text, out textbox1Text))
                {
                    label1.Content = "請輸入數字";
                    return;
                }
                else
                {
                    bank_savings[combobox1.SelectedIndex] = textbox1Text;
                    label1.Content = Calc_Sum_Saving().ToString();
                }
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(dir_name))
            {
                Directory.CreateDirectory(dir_name);
            }     
            StreamWriter sw = new StreamWriter(dir_name+file_name, false, Encoding.ASCII);
            for (int i = 0; i < 7; i++)
            {
                sw.WriteLine(bank_savings[i]);
            }
            sw.Close();
        }

        private void Selection_Change1(object sender, SelectionChangedEventArgs e)
        {
            textbox1.Text = bank_savings[combobox1.SelectedIndex].ToString();
        }
        private void Window_Loaded1(object sender, RoutedEventArgs e)
        {
            if (File.Exists(dir_name+file_name))
            {
                StreamReader sr = new StreamReader(dir_name+file_name);
                for (int i = 0; i < 7; i++)
                {
                    int.TryParse(sr.ReadLine(), out bank_savings[i]);
                }
                sr.Close();
            }
            label1.Content = Calc_Sum_Saving().ToString();
        }

    }
}
