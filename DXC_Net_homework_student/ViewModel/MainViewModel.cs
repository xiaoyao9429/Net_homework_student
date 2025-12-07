using DXC_Net_homework_student;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DXC_Net_homework_student
{
    internal partial class MainViewModel :  ViewModelBase
    {

        private string _searchStudentId; // 搜索学生ID
        private List<int> _suggestedStudentIds; // 模糊匹配的学生ID列表
        private int? _selectedSuggestedId; // 选中的建议ID

        private addStudentWindow _addStudentWidonw;
        private ObservableCollection<student> _studentList;//所有学生的信息，展示在UI界面中
        private student _Student;//当前选中的学生，用于更新学生的信息
        private bool _isAllSelected; // 是否全选

        private ICommand _addStuCommand;//添加学生命令
        private ICommand _deleteStuCommand;//删除学生命令
        private ICommand _updateStuCommand;//更新学生信息命令
        private ICommand _refreshStuCommand;//刷新主窗口命令
        private ICommand _searchStuCommand;//根据条件检索学生命令
        private ICommand _selectAllCommand;//全选学生命令
        private ICommand _selectCommand;//单选学生命令
        private ICommand _selectSuggestedIdCommand;//选择建议ID命令

    

        public MainViewModel() {
            
            StudentList = new ObservableCollection<student>();// 初始化学生集合（此时无数据，只是生成实例对象）
            SuggestedStudentIds = new List<int>();//初始化建议ID集合（此时无数据，只是生成实例对象）


            AddStuCommand = new RelayCommand(OpenAddStudentWindow);
            UpdateStuCommand = new RelayCommand(UpdateStudent);
            RefreshCommand= new RelayCommand(RefreshList);
            SearchCommand = new RelayCommand(SearchStudent);
            DeleteStuCommand = new RelayCommand(DeleteSelectedStudents);
            SelectAllCommand = new RelayCommand(SelectAllStudent);
            SelectCommand = new RelayCommand(SelectStudent);
            SelectSuggestedIdCommand = new RelayCommand(SelectSuggestedId);


            // 加载学生数据
            LoadStudents();
        }

        public List<int> SuggestedStudentIds
        {
            get { return _suggestedStudentIds; }
            set
            {
                _suggestedStudentIds = value;
                OnPropertyChanged();
            }
        }

        public string SearchStudentId
        {
            get { return _searchStudentId; }
            set
            {
                _searchStudentId = value;
                OnPropertyChanged();
                // 当搜索ID变化时，更新建议列表
                UpdateSuggestedStudentIds();
            }
        }

        

        public int? SelectedSuggestedId
        {
            get { return _selectedSuggestedId; }
            set
            {
                _selectedSuggestedId = value;
                OnPropertyChanged();
            }
        }

        public void  SelectAllStudent(object parameter)
        {
           
            if (IsAllSelected == true) {
            
                for(int i = 0; i < StudentList.Count; i++)
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


        /// <summary>
        /// 是否全选
        /// </summary>
        public bool IsAllSelected
        {
            get { return _isAllSelected; }
            set
            {
                _isAllSelected = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 选中的学生对象
        /// </summary>
        public student Student
        {
            get { return _Student; }
            set
            {
                _Student = value;
                OnPropertyChanged();
            }
        }


      

        /// <summary>
        /// 学生数据集合
        /// </summary>
        public ObservableCollection<student> StudentList
        {
            get { return _studentList; }
            set
            {
                _studentList = value;
                OnPropertyChanged();
            }
        }



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

        public void SearchStudent(object parameter)
        {
            
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

        public ICommand SelectSuggestedIdCommand
        {
            get { return _selectSuggestedIdCommand; }
            set { _selectSuggestedIdCommand = value; }
        }
        public ICommand SelectCommand
        {
            get { return _selectCommand; }
            set { _selectCommand = value; }
        }

        public ICommand SelectAllCommand
        {
            get{return _selectAllCommand;}

            set
            {
                _selectAllCommand = value;
            }
        }


        public ICommand AddStuCommand
        {
            get { return _addStuCommand; }
            set
            {
                _addStuCommand = value;
            }
        }
        public ICommand DeleteStuCommand
        {
            get { return _deleteStuCommand; }
            set
            {
                _deleteStuCommand = value;
            }
        }

        public ICommand RefreshCommand
        {
            get { return _refreshStuCommand; }
            set
            {
                _refreshStuCommand = value;
            }
        }

        public ICommand SearchCommand
        {
            get { return _searchStuCommand; }
            set
            {
                _searchStuCommand = value;
            }
        }

        public ICommand UpdateStuCommand
        {
            get { return _updateStuCommand; }
            set
            {
                _updateStuCommand = value;
            }
        }
    }
}
