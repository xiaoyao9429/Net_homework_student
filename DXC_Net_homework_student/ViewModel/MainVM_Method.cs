using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC_Net_homework_student
{
    internal  partial class MainViewModel
    {

        private void OpenAddStudentWindow(object parameter)
        {
            _addStudentWidonw = new addStudentWindow();

            _addStudentWidonw.Closed += (sender, e) =>// // 当窗口关闭时刷新学生列表
            {

                LoadStudents();
            };

            _addStudentWidonw.Show();
        }

        /// <summary>
        /// 删除选中的学生
        /// </summary>
        /// <param name="parameter">命令参数</param>
        private void DeleteSelectedStudents(object parameter)
        {
            try
            {
                // 获取所有被选中的学生
                var selectedStudents = StudentList.Where(s => s.IsSelected).ToList();

                if (selectedStudents.Count == 0)
                {
                    System.Windows.MessageBox.Show("请先选择要删除的学生！");
                    return;
                }

                // 显示确认对话框
                var result = System.Windows.MessageBox.Show(
                    $"确定要删除选中的{selectedStudents.Count}名学生吗？",
                    "确认删除",
                    System.Windows.MessageBoxButton.YesNo,
                    System.Windows.MessageBoxImage.Warning);

                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    studentModel model = new studentModel();
                    int deletedCount = 0;

                    // 逐个删除学生
                    foreach (var student in selectedStudents)
                    {
                        model.deleteStudent(student.Id);
                        deletedCount++;
                    }

                    // 重新加载学生列表
                    LoadStudents();

                    // 重置全选状态
                    _isAllSelected = false;
                    OnPropertyChanged("IsAllSelected");

                    System.Windows.MessageBox.Show($"成功删除{deletedCount}名学生！");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除学生失败: " + ex.Message);
                System.Windows.MessageBox.Show("删除学生失败: " + ex.Message);
            }
        }

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="parameter">命令参数</param>
        private void UpdateStudent(object parameter)
        {
            try
            {
                // 获取所有被选中的学生
                var selectedStudents = StudentList.Where(s => s.IsSelected).ToList();

                if (selectedStudents.Count == 0)
                {
                    System.Windows.MessageBox.Show("请先选择要修改的学生信息！");
                    return;
                }

                studentModel model = new studentModel();
                int updatedCount = 0;

                // 逐个更新学生信息
                foreach (var student in selectedStudents)
                {
                    model.updateStudent(student);
                    updatedCount++;
                }

                // 更新成功后刷新学生列表
                LoadStudents();

                // 显示更新成功提示
                System.Windows.MessageBox.Show($"成功更新{updatedCount}名学生信息！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("更新学生信息失败: " + ex.Message);
                System.Windows.MessageBox.Show("更新学生信息失败: " + ex.Message);
            }
        }



        /// <summary>
        /// 从数据库加载学生数据
        /// </summary>
        public void LoadStudents()
        {
            try
            {
                studentModel model = new studentModel();
                List<student> studentList = model.GetAllStudents();

                // 清空现有数据
                StudentList.Clear();

                // 将student实体转换为StudentViewModel并添加到集合中
                foreach (var student in studentList)
                {
                    StudentList.Add(student);
                }
            }
            catch (Exception ex)
            {
                // 实际应用中应使用日志记录错误
                Console.WriteLine("加载学生数据失败: " + ex.Message);
            }
        }

        public void RefreshList(object parameter)
        {
            LoadStudents();
        }
        public void SelectStudent(object parameter)
        {
            if (parameter != null && parameter is student)
            {
                Student = parameter as student;
                Student.IsSelected = !Student.IsSelected;

                bool temp = Student.IsSelected;
                if (Student != null && temp == false)
                {
                    IsAllSelected = false;
                }

                else if (Student != null && temp == true)
                {
                    Func<bool> func = () =>
                    {
                        for (int i = 0; i < StudentList.Count; i++)
                        {

                            if (StudentList[i].IsSelected != true)
                            {

                                return false;
                            }
                        }

                        return true;
                    };

                    if (func() == true)
                    {
                        IsAllSelected = true;
                    }
                }
            }

            else
            {
                //todo
            }
        }

        public void SelectAllStudent(object parameter)
        {

            if (IsAllSelected == true)
            {

                for (int i = 0; i < StudentList.Count; i++)
                {
                    StudentList[i].IsSelected = true;
                    //OnPropertyChanged("StudentList[" + i + "].IsSelected");这样写没用
                }

            }

            else
            {
                //todo
            }


        }

        /// <summary>
        /// 选择建议的学生ID
        /// </summary>
        /// <param name="parameter">命令参数</param>
        public void SelectSuggestedId(object parameter)
        {
            if (parameter != null && parameter is int)
            {
                int selectedId = (int)parameter;
                SearchStudentId = selectedId.ToString();
                SelectedSuggestedId = selectedId;
                // 选择后清空建议列表
                SuggestedStudentIds = new List<int>();
            
            }
        }


        private void UpdateSuggestedStudentIds()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchStudentId))
                {
                    SuggestedStudentIds = new List<int>();
                    return;
                }

                studentModel model = new studentModel();
                List<student> allStudents = model.GetAllStudents();
                
                // 模糊匹配学生ID
                List<int> suggestedIds = allStudents
                    .Where(s => s.Id.ToString().StartsWith(SearchStudentId))
                    .Select(s => s.Id)
                    .ToList();

                SuggestedStudentIds = suggestedIds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("更新建议学生ID列表失败: " + ex.Message);
            }
        }
    }
}
