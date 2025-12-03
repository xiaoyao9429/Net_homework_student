using DXC_Net_homework_student.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DXC_Net_homework_student.ViewModel
{
    internal class addStudenViewModel : ViewModelBase
    {
        addStudentWindow  _addStudentWidonw;
        public addStudenViewModel(addStudentWindow _addStudentWidonw)
        {
            _saveCommand = new RelayCommand(SaveStudent);
            _cancelCommand = new RelayCommand(Cancel);
            this._addStudentWidonw = _addStudentWidonw;
        }

        private ICommand _saveCommand;//确定命令
        private ICommand _cancelCommand;//取消命令

        // 学生信息
        private string _name;
        private string _sex;
        private string _birthday;
        private string _phone;
        private string _address;
        private int _scoreYW;
        private int _scoreSX;
        private int _scoreYY;
        public string Name
        { 
            get { return _name; } 
            set { _name = value; OnPropertyChanged(); }
        }

        
        public string Sex
        { 
            get { return _sex; } 
            set { _sex = value; OnPropertyChanged(); }
        }

        
        public string Birthday
        { 
            get { return _birthday; } 
            set { _birthday = value; OnPropertyChanged(); }
        }

      
        public string Phone
        { 
            get { return _phone; } 
            set { _phone = value; OnPropertyChanged(); }
        }

       
        public string Address
        { 
            get { return _address; } 
            set { _address = value; OnPropertyChanged(); }
        }

      
        public int ScoreYW
        { 
            get { return _scoreYW; } 
            set { _scoreYW = value; OnPropertyChanged(); }
        }

      
        public int ScoreSX
        { 
            get { return _scoreSX; } 
            set { _scoreSX = value; OnPropertyChanged(); }
        }

        
        public int ScoreYY
        { 
            get { return _scoreYY; } 
            set { _scoreYY = value; OnPropertyChanged(); }
        }

       
        public ICommand SaveCommand
        { 
            get 
            { 
                return _saveCommand; 
            } 
        }

        
        public ICommand CancelCommand
        { 
            get 
            { 
              
                return _cancelCommand; 
            } 
        }

        // 保存学生信息到数据库
        private void SaveStudent(object parameter)
        { 
            // 简单验证
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Sex))
            { 
                MessageBox.Show("姓名和性别不能为空！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning); 
                return; 
            }

            try
            { 
                // 创建student对象
                student newStudent = new student(Name, Sex, Birthday, Phone, Address, ScoreYW, ScoreSX, ScoreYY); 
                
                // 保存到数据库
                studentModel model = new studentModel();
                model.addStudent(newStudent);
                
                MessageBox.Show("学生信息保存成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information); 
                
               
            } 
            catch (Exception ex)
            { 
                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
        }

        // 取消操作
        private void Cancel(object parameter)
        { 
            _addStudentWidonw.Close();
        }
    }
}
