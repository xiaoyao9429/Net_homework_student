using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DXC_Net_homework_student
{
    internal class TeacherInfoViewModel : ViewModelBase
    {
        private ObservableCollection<TeacherWithCourses> _teacherList;
        
        public ObservableCollection<TeacherWithCourses> TeacherList
        {
            get { return _teacherList; }
            set
            {
                _teacherList = value;
                OnPropertyChanged();
            }
        }
        
        public TeacherInfoViewModel()
        {
            LoadTeacherInfo();
        }
        
        // 加载教师信息，包括所教科目
        private void LoadTeacherInfo()
        {
            try
            {
                // 获取所有教师信息
                teacherModel teacherModel = new teacherModel();
                var teachers = teacherModel.GetAllTeachers();
                
                // 获取所有课程信息
                courseModel courseModel = new courseModel();
                var courses = courseModel.GetAllCourses();
                
                // 获取教师与课程的关系
                teacher_course_relationModel relationModel = new teacher_course_relationModel();
                var relations = relationModel.GetAllTeacherCourseRelations();
                
                // 创建教师信息集合，包含所教科目
                ObservableCollection<TeacherWithCourses> teacherWithCoursesList = new ObservableCollection<TeacherWithCourses>();
                
                foreach (var teacher in teachers)
                {
                    // 获取该教师所教的课程ID
                    var teachingCourseIds = relations
                        .Where(r => r.TeacherId == teacher.TeacherId)
                        .Select(r => r.CourseId)
                        .ToList();
                    
                    // 获取课程名称
                    var teachingCourseNames = courses
                        .Where(c => teachingCourseIds.Contains(c.CourseId))
                        .Select(c => c.CourseName)
                        .ToList();
                    
                    // 组合课程名称，用逗号分隔
                    string coursesText = string.Join(", ", teachingCourseNames);
                    
                    // 创建包含所教科目信息的教师对象
                    TeacherWithCourses teacherWithCourses = new TeacherWithCourses(teacher, coursesText);
                    teacherWithCoursesList.Add(teacherWithCourses);
                }
                
                TeacherList = teacherWithCoursesList;
            }
            catch (System.Exception ex)
            {
                // 记录加载教师信息异常日志
                Log.error(string.Format("加载教师信息失败：错误信息={0}", ex.Message));
            }
        }
        
        // 包含所教科目信息的教师类
        internal class TeacherWithCourses : ViewModelBase
        {
            private int _teacherId;
            private string _teacherName;
            private string _teacherSex;
            private string _teacherStrong;
            private string _teachingCourses;
            
            public int TeacherId
            {
                get { return _teacherId; }
                set
                {
                    _teacherId = value;
                    OnPropertyChanged();
                }
            }
            
            public string TeacherName
            {
                get { return _teacherName; }
                set
                {
                    _teacherName = value;
                    OnPropertyChanged();
                }
            }
            
            public string TeacherSex
            {
                get { return _teacherSex; }
                set
                {
                    _teacherSex = value;
                    OnPropertyChanged();
                }
            }
            
            public string TeacherStrong
            {
                get { return _teacherStrong; }
                set
                {
                    _teacherStrong = value;
                    OnPropertyChanged();
                }
            }
            
            public string TeachingCourses
            {
                get { return _teachingCourses; }
                set
                {
                    _teachingCourses = value;
                    OnPropertyChanged();
                }
            }
            
            public TeacherWithCourses(teacher teacher, string teachingCourses)
            {
                this._teacherId = teacher.TeacherId;
                this._teacherName = teacher.TeacherName;
                this._teacherSex = teacher.TeacherSex;
                this._teacherStrong = teacher.TeacherStrong;
                this._teachingCourses = teachingCourses;
            }
        }
    }
}