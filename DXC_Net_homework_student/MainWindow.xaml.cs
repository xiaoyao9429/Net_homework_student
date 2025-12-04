using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace DXC_Net_homework_student
{
    public partial  class MainWindow:Window
    {
        private MainViewModel _mainViewModel;

        public MainWindow()
        {
            _mainViewModel = new MainViewModel();
            InitializeComponent();
            this.DataContext = _mainViewModel;
        }
        
        // 当学生复选框被选中时
        private void StudentCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckAllSelectedState();
        }
        
        // 当学生复选框被取消选中时
        private void StudentCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
          
        }
        
        // 检查所有学生的选中状态，并更新全选复选框
        private void CheckAllSelectedState()
        {
           
        }
        
      

     
    }
}
