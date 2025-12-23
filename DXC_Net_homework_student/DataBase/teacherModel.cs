using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC_Net_homework_student
{
    internal class teacherModel
    {
        //添加一条教师记录
        public void addTeacher(teacher teacher)
        {
            try
            {
                DBConnect db = new DBConnect();
                // 教师ID是自增的，不需要手动指定
                string sql = string.Format("insert into teacher(姓名, 性别, 特点) values('{0}', '{1}', '{2}')",
                    teacher.TeacherName, teacher.TeacherSex, teacher.TeacherStrong);

                db.ExecuteUpdate(sql);

                // 从数据库当中获取自增的主键ID并赋给当前teacher.TeacherId
                string q1 = string.Format("select id from teacher where 姓名='{0}' and 性别='{1}'", 
                    teacher.TeacherName, teacher.TeacherSex);
                DataTable primaryKeyTable = db.ExecuteQuery(q1);
                if (primaryKeyTable.Rows.Count > 0)
                {
                    teacher.TeacherId = Convert.ToInt32(primaryKeyTable.Rows[0][0]);
                }
                else
                {
                    Console.WriteLine("没有找到该教师");
                }

                // 记录添加教师日志
                Log.info(string.Format("成功添加教师：ID={0}, 姓名={1}, 性别={2}", teacher.TeacherId, teacher.TeacherName, teacher.TeacherSex));
            }
            catch (Exception ex)
            {
                // 记录添加教师异常日志
                Log.error(string.Format("添加教师失败：姓名={0}, 错误信息={1}", teacher.TeacherName, ex.Message));
                throw;
            }
        }

        //删除一条教师记录
        public void deleteTeacher(int id)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "delete from teacher where id=" + id;
                db.ExecuteUpdate(sql);

                // 记录删除教师日志
                Log.info(string.Format("成功删除教师：ID={0}", id));
            }
            catch (Exception ex)
            {
                // 记录删除教师异常日志
                Log.error(string.Format("删除教师失败：ID={0}, 错误信息={1}", id, ex.Message));
                throw;
            }
        }

        //修改一条教师记录
        public void updateTeacher(teacher teacher)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = string.Format("update teacher set 姓名='{0}', 性别='{1}', 特点='{2}' where id={3}",
                    teacher.TeacherName, teacher.TeacherSex, teacher.TeacherStrong, teacher.TeacherId);
                db.ExecuteUpdate(sql);

                // 记录更新教师日志
                Log.info(string.Format("成功更新教师：ID={0}, 姓名={1}, 性别={2}", teacher.TeacherId, teacher.TeacherName, teacher.TeacherSex));
            }
            catch (Exception ex)
            {
                // 记录更新教师异常日志
                Log.error(string.Format("更新教师失败：ID={0}, 姓名={1}, 错误信息={2}", teacher.TeacherId, teacher.TeacherName, ex.Message));
                throw;
            }
        }
        
        //查询所有教师记录
        public List<teacher> GetAllTeachers()
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "select * from teacher";
                DataTable dt = db.ExecuteQuery(sql);
                
                List<teacher> teachers = new List<teacher>();
                
                foreach (DataRow row in dt.Rows)
                {
                    // 从数据库行创建teacher对象
                    teacher t = new teacher(
                        Convert.ToInt32(row["id"]),
                        row["姓名"].ToString(),
                        row["性别"].ToString(),
                        row["特点"].ToString()
                    );
                  
                    teachers.Add(t);
                }
                
                // 记录查询教师日志
                Log.info(string.Format("成功查询所有教师，共{0}条记录", teachers.Count));
                return teachers;
            }
            catch (Exception ex)
            {
                // 记录查询教师异常日志
                Log.error(string.Format("查询教师失败：错误信息={0}", ex.Message));
                throw;
            }
        }
    }
}