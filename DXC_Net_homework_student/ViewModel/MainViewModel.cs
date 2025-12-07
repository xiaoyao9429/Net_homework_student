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
    internal partial class MainViewModel : ViewModelBase
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
        private ICommand _selectAllCommand;//全选学生命令
        private ICommand _selectCommand;//单选学生命令
        private ICommand _selectSuggestedIdCommand;//选择建议ID命令



        public MainViewModel()
        {

            StudentList = new ObservableCollection<student>();// 初始化学生集合（此时无数据，只是生成实例对象）
            SuggestedStudentIds = new List<int>();//初始化建议ID集合（此时无数据，只是生成实例对象）


            AddStuCommand = new RelayCommand(OpenAddStudentWindow);
            UpdateStuCommand = new RelayCommand(UpdateStudent);
            RefreshCommand = new RelayCommand(RefreshList);
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



    }
}
