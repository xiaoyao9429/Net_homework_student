using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC_Net_homework_student
{
    internal class courseModel
    {
        //添加一条课程记录
        public void addCourse(course course)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = string.Format("insert into course(id, 课程名) values({0}, '{1}')",
                    course.CourseId, course.CourseName);

                db.ExecuteUpdate(sql);

                // 记录添加课程日志
                Log.info(string.Format("成功添加课程：ID={0}, 名称={1}", course.CourseId, course.CourseName));
            }
            catch (Exception ex)
            {
                // 记录添加课程异常日志
                Log.error(string.Format("添加课程失败：名称={0}, 错误信息={1}", course.CourseName, ex.Message));
                throw;
            }
        }

        //删除一条课程记录
        public void deleteCourse(int id)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "delete from course where id=" + id;
                db.ExecuteUpdate(sql);

                // 记录删除课程日志
                Log.info(string.Format("成功删除课程：ID={0}", id));
            }
            catch (Exception ex)
            {
                // 记录删除课程异常日志
                Log.error(string.Format("删除课程失败：ID={0}, 错误信息={1}", id, ex.Message));
                throw;
            }
        }

        //修改一条课程记录
        public void updateCourse(course course)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "update course set 课程名='" + course.CourseName + "' where id=" + course.CourseId;
                db.ExecuteUpdate(sql);

                // 记录更新课程日志
                Log.info(string.Format("成功更新课程：ID={0}, 名称={1}", course.CourseId, course.CourseName));
            }
            catch (Exception ex)
            {
                // 记录更新课程异常日志
                Log.error(string.Format("更新课程失败：ID={0}, 名称={1}, 错误信息={2}", course.CourseId, course.CourseName, ex.Message));
                throw;
            }
        }
        
        //查询所有课程记录
        public List<course> GetAllCourses()
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "select * from course";
                DataTable dt = db.ExecuteQuery(sql);
                
                List<course> courses = new List<course>();
                
                foreach (DataRow row in dt.Rows)
                {
                    // 从数据库行创建course对象
                    course c = new course(
                        Convert.ToInt32(row["id"]),
                        row["课程名"].ToString()
                    );
                    courses.Add(c);
                }
                
                // 记录查询课程日志
                Log.info(string.Format("成功查询所有课程，共{0}条记录", courses.Count));
                return courses;
            }
            catch (Exception ex)
            {
                // 记录查询课程异常日志
                Log.error(string.Format("查询课程失败：错误信息={0}", ex.Message));
                throw;
            }
        }
    }
}