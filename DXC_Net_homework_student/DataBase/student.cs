using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC_Net_homework_student
{
    internal class student:ViewModelBase
    {
        private int _id;//学生id,数据库设置为自增，所以不用赋值（只在更新学生记录的时候需要用，先这样用着）
        private string _name;//学生姓名
        private string _sex;//性别
        private string _birthday;//出生年月
        private string _phone;//手机号
        private string _address;//住址
        private int _scoreYW;//语文分数
        private int _scoreSX;//数学分数
        private int _scoreYY;//英语分数
        private bool _isSelected; // 是否被选中，用于批量操作


        public student(string name, string sex, string birthday="不知道", string phone="不知道", string address="不知道", int scoreYW=-1, int scoreSX = -1, int scoreYY = -1)
        {
            _name=name;  _sex=sex; _birthday=birthday; _phone=phone; _address=address; _scoreYW = scoreYW;_scoreSX=scoreSX; _scoreYY=scoreYY;
            _isSelected = false; // 默认不选中
        }
        
        public bool IsSelected
        {
            get { return _isSelected; }
            set { 
                _isSelected = value;
                OnPropertyChanged();
               
            }
        }
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public int ScoreYW
        {
            get { return _scoreYW; }
            set { _scoreYW = value; }
        }

        public int ScoreSX
        {
            get { return _scoreSX; }
            set { _scoreSX = value; }
        }

        public int ScoreYY
        {
            get { return _scoreYY; }
            set { _scoreYY = value; }
        }

      
    }
}
