using DXC_Net_homework_student.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DXC_Net_homework_student
{
    internal class MainViewModel :  ViewModelBase
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private addStudentWindow _addStudentWidonw;

        private ICommand _addStuCommand;//添加学生命令
        private ICommand _deleteStuCommand;//删除学生命令
        private ICommand _updateStuCommand;//更新学生信息命令


        public MainViewModel() {
            // 初始化学生集合
            StudentList = new ObservableCollection<student>();
            
            AddStuCommand = new RelayCommand(OpenAddStudentWindow);
            UpdateStuCommand = new RelayCommand(UpdateStudent);

            // 加载学生数据
            LoadStudents();
        }


        private ObservableCollection<student> _studentList;

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
        /// <param name="parameter">选中的学生对象</param>
        private void UpdateStudent(object parameter)
        {
            if (parameter is student studentToUpdate)
            {
                try
                {
                    studentModel model = new studentModel();
                    model.updateStudent(studentToUpdate);
                    // 更新成功后刷新学生列表
                    LoadStudents();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("更新学生信息失败: " + ex.Message);
                }
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
