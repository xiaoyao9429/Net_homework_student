using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC_Net_homework_student { 
    internal class teacher
    {

        int _teacherId;
        string _teacherName;
        string _teacherSex;
        string _teacherStrong;

        public teacher(int teacherId, string teacherName, string teacherSex, string teacherStrong)
        {
            this._teacherId = teacherId;
            this._teacherName = teacherName;
            this._teacherSex = teacherSex;
            this._teacherStrong = teacherStrong;
        }


        public int TeacherId
        {
            get { return _teacherId; }
            set { _teacherId = value; }
        }

        public string TeacherName
        {
            get { return _teacherName; }
            set { _teacherName = value; }
        }

        public string TeacherSex { 
            get { return _teacherSex; }
            set { _teacherSex = value; }
        }

        public string TeacherStrong
        {
            get { return _teacherStrong; }
            set { _teacherStrong = value; }
        }

    }
}
