using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DXC_Net_homework_student
{
    internal class studentModel
    {
        //添加一条学生记录
        public void addStudent(student student)
        {
            DBConnect db = new DBConnect();
           string sql = "insert into student values("+"'"+ student.Name +"'" + "," + "'" + student.Sex+"'"  + ","+ "'" + 
                                                            student.Birthday+"'" + ","+ "'" + student.Phone+"'" + "," + "'"+
                                                            student.Address+"'" +","+"'"+student.ScoreYW+"'"+","+
                                                            "'"+student.ScoreSX+"'"+","+"'"+student.ScoreYY+"'"+ ")";

            db.ExecuteUpdate(sql);

            // 还需要从数据库当中获取自增的主键ID并赋给当前student.id
            string q1 = string.Format("select 学生ID from student where 电话='{0}'", student.Phone);//电话具有唯一约束
            DataTable primaryKeyTable=db.ExecuteQuery(q1);
            if (primaryKeyTable.Rows.Count > 0)
            {
                student.Id = Convert.ToInt32(primaryKeyTable.Rows[0][0]);
            }

            else
            {
                Console.WriteLine("没有找到该学生");
            }

        }

        //删除一条学生记录
        public void deleteStudent(int id)
        {
            DBConnect db = new DBConnect();
            string sql = "delete from student where 学生ID=" + id;
            db.ExecuteUpdate(sql);
        }

        //修改一条学生记录
        public void updateStudent(student student)
        {
            DBConnect db = new DBConnect();
            string sql = "update student set 姓名='" + student.Name + "',性别='" + student.Sex + "',出生年月='" +
                student.Birthday + "',电话='" + student.Phone + "',住址='" + student.Address +
                "',语文=" + student.ScoreYW + ",数学=" + student.ScoreSX + ",英语=" + student.ScoreYY + " where 学生ID=" + student.Id;
            db.ExecuteUpdate(sql);
        }
        
        //查询所有学生记录
        public List<student> GetAllStudents()
        {
            DBConnect db = new DBConnect();
            string sql = "select * from student";
            DataTable dt = db.ExecuteQuery(sql);
            
            List<student> students = new List<student>();
            
            foreach (DataRow row in dt.Rows)
            {
                // 从数据库行创建student对象
                student s = new student(
                    row["姓名"].ToString(),
                    row["性别"].ToString(),
                    row["出生年月"].ToString().Substring(0,10),
                    row["电话"].ToString(),
                    row["住址"].ToString(),
                    Convert.ToInt32(row["语文"]),
                    Convert.ToInt32(row["数学"]),
                    Convert.ToInt32(row["英语"])
                );
                s.Id = Convert.ToInt32(row["学生ID"]);
                students.Add(s);
            }
            
            return students;
        }
    }
}
