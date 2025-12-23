using System.Windows;

namespace DXC_Net_homework_student
{
    /// <summary>
    /// TeacherInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TeacherInfoWindow : Window
    {
        private TeacherInfoViewModel _viewModel;
        
        public TeacherInfoWindow()
        {
            InitializeComponent();
            
            // 初始化ViewModel
            _viewModel = new TeacherInfoViewModel();
            this.DataContext = _viewModel;
        }
        
        // 关闭窗口
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}