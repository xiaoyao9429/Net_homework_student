using DXC_Net_homework_student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DXC_Net_homework_student
{
    /// <summary>
    /// addStudentWindow.xaml 的交互逻辑
    /// </summary>
    public partial class addStudentWindow : Window
    {
        private addStudenViewModel _viewModel;
        
        // 构造函数
        public addStudentWindow()
        {
            InitializeComponent();
            
            // 初始化ViewModel
            _viewModel = new addStudenViewModel(this);//窗口指针传过去，点击取消后隐藏窗口
            this.DataContext = _viewModel;
        }
    
    }
}
