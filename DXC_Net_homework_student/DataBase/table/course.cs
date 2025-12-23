using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC_Net_homework_student { 
    internal class course
    {
        int _courseId;
        string _courseName;
        public course(int id, string name)
        {
            this._courseId = id;
            this._courseName = name;
        }

        public int CourseId
        {
            get { return _courseId; }
            set { _courseId = value; }
        }

        public string CourseName
        {
            get { return _courseName; }
            set { _courseName = value; }
        }

       
    }
}
