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
        
        private string _searchStudentName; // 搜索学生姓名
        private List<string> _suggestedStudentNames; // 模糊匹配的学生姓名列表
        private string _selectedSuggestedName; // 选中的建议姓名
        private string _searchStudentSex; // 搜索学生性别
        private string _searchSubject; // 搜索科目
        private string _minScore; // 最小分数
        private string _maxScore; // 最大分数

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
        private ICommand _selectSuggestedIdCommand;//选择建议ID命令(没用到)
        private ICommand _selectSuggestedNameCommand;//选择建议姓名命令(没用到)
        private ICommand _searchStuCommand;//检索学生命令
        private ICommand _loadToExcelCommand;//导出学生信息为Excel的命令
        


        public MainViewModel()
        {

            StudentList = new ObservableCollection<student>();// 初始化学生集合（此时无数据，只是生成实例对象）
            SuggestedStudentIds = new List<int>();//初始化建议ID集合（此时无数据，只是生成实例对象）
            SuggestedStudentNames = new List<string>();//初始化建议姓名集合（此时无数据，只是生成实例对象）


            AddStuCommand = new RelayCommand(OpenAddStudentWindow);
            UpdateStuCommand = new RelayCommand(UpdateStudent);
            RefreshCommand = new RelayCommand(RefreshList);
            DeleteStuCommand = new RelayCommand(DeleteSelectedStudents);
            SelectAllCommand = new RelayCommand(SelectAllStudent);
            SelectCommand = new RelayCommand(SelectStudent);
            SelectSuggestedIdCommand = new RelayCommand(SelectSuggestedId);
            SelectSuggestedNameCommand = new RelayCommand(SelectSuggestedName);
            SearchStuCommand = new RelayCommand(SearchStudent);
            LoadToExcelCommand = new RelayCommand(LoadExcelData);


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

        public List<string> SuggestedStudentNames
        {
            get { return _suggestedStudentNames; }
            set
            {
                _suggestedStudentNames = value;
                OnPropertyChanged();
            }
        }

        public string SearchStudentName
        {
            get { return _searchStudentName; }
            set
            {
                _searchStudentName = value;
                OnPropertyChanged();
                // 当搜索姓名变化时，更新建议列表
                UpdateSuggestedStudentNames();
            }
        }

        public string SelectedSuggestedName
        {
            get { return _selectedSuggestedName; }
            set
            {
                _selectedSuggestedName = value;
                OnPropertyChanged();
            }
        }

        public string SearchStudentSex
        {
            get { return _searchStudentSex; }
            set
            {
                _searchStudentSex = value;
                OnPropertyChanged();
            }
        }

        public string SearchSubject
        {
            get { return _searchSubject; }
            set
            {
                _searchSubject = value;
                OnPropertyChanged();
            }
        }

        public string MinScore
        {
            get { return _minScore; }
            set
            {
                _minScore = value;
                OnPropertyChanged();
            }
        }

        public string MaxScore
        {
            get { return _maxScore; }
            set
            {
                _maxScore = value;
                OnPropertyChanged();
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
