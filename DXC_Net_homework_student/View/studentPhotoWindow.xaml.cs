using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace DXC_Net_homework_student
{
    /// <summary>
    /// StudentPhotoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StudentPhotoWindow : Window
    {
        // 学生数据集合
        private ObservableCollection<student> _students;
        
        public StudentPhotoWindow()
        {
            InitializeComponent();
            
            // 加载学生数据
            LoadStudents();
        }
        
        /// <summary>
        /// 加载所有学生数据
        /// </summary>
        private void LoadStudents()
        {
            try
            {
                // 创建学生模型实例
                studentModel studentModel = new studentModel();
                
                // 获取所有学生数据
                List<student> studentsList = studentModel.GetAllStudents();
                
                // 创建ObservableCollection
                _students = new ObservableCollection<student>(studentsList);
                
                // 设置数据上下文
                studentsItemsControl.ItemsSource = _students;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载学生数据失败: " + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine("加载学生数据失败: " + ex.Message);
            }
        }
        
        /// <summary>
        /// 从MainViewModel接收学生数据
        /// </summary>
        /// <param name="students">学生数据集合</param>
        public void SetStudentsData(IEnumerable<student> students)
        {
            if (students != null)
            {
                _students = new ObservableCollection<student>(students);
                studentsItemsControl.ItemsSource = _students;
            }
        }
    }
}