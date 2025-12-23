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

       


        // 双击学生
        private void dgStudents_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedItem != null)
            {
                student selectedStudent = dataGrid.SelectedItem as student;
                if (selectedStudent != null)
                {
                    // 切换选中状态
                    selectedStudent.IsSelected =true;
                }
            }
        }

        // 选择建议的学生ID
        private void lstSuggestedIds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox != null && listBox.SelectedItem != null)
            {
                int selectedId = (int)listBox.SelectedItem;
                _mainViewModel.SelectSuggestedId(selectedId);
            }
        }

        // 选择建议的学生姓名
        private void lstSuggestedNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox != null && listBox.SelectedItem != null)
            {
                string selectedName = (string)listBox.SelectedItem;
                _mainViewModel.SelectSuggestedName(selectedName);
            }
        }

    }
}