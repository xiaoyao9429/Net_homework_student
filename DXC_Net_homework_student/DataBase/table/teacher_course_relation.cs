using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC_Net_homework_student
{
    internal class teacher_course_relation
    {

        int _teacherId;
        int _courseId;

        public teacher_course_relation(int teacherId, int courseId)
        {
            this._teacherId = teacherId;
            this._courseId = courseId;
        }


        public int TeacherId
        {
            get { return _teacherId; }
            set { _teacherId = value; }
        }


        public int CourseId
        {
            get { return _courseId; }
            set { _courseId = value; }
        }
    }
}
