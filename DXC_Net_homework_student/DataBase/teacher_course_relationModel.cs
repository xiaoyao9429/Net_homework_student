using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC_Net_homework_student
{
    internal class teacher_course_relationModel
    {
        //添加一条教师课程关系记录
        public void addTeacherCourseRelation(teacher_course_relation relation)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = string.Format("insert into teacher_course_relation(teacher_id, course_id) values({0}, {1})",
                    relation.TeacherId, relation.CourseId);

                db.ExecuteUpdate(sql);

                // 记录添加教师课程关系日志
                Log.info(string.Format("成功添加教师课程关系：教师ID={0}, 课程ID={1}", relation.TeacherId, relation.CourseId));
            }
            catch (Exception ex)
            {
                // 记录添加教师课程关系异常日志
                Log.error(string.Format("添加教师课程关系失败：教师ID={0}, 课程ID={1}, 错误信息={2}", relation.TeacherId, relation.CourseId, ex.Message));
                throw;
            }
        }

        //删除一条教师课程关系记录
        public void deleteTeacherCourseRelation(int teacherId, int courseId)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = string.Format("delete from teacher_course_relation where teacher_id={0} and course_id={1}", teacherId, courseId);
                db.ExecuteUpdate(sql);

                // 记录删除教师课程关系日志
                Log.info(string.Format("成功删除教师课程关系：教师ID={0}, 课程ID={1}", teacherId, courseId));
            }
            catch (Exception ex)
            {
                // 记录删除教师课程关系异常日志
                Log.error(string.Format("删除教师课程关系失败：教师ID={0}, 课程ID={1}, 错误信息={2}", teacherId, courseId, ex.Message));
                throw;
            }
        }

        //根据教师ID删除所有相关课程关系
        public void deleteTeacherCourseRelationsByTeacherId(int teacherId)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "delete from teacher_course_relation where teacher_id=" + teacherId;
                db.ExecuteUpdate(sql);

                // 记录删除教师课程关系日志
                Log.info(string.Format("成功删除教师ID={0}的所有课程关系", teacherId));
            }
            catch (Exception ex)
            {
                // 记录删除教师课程关系异常日志
                Log.error(string.Format("删除教师课程关系失败：教师ID={0}, 错误信息={1}", teacherId, ex.Message));
                throw;
            }
        }

        //根据课程ID删除所有相关教师关系
        public void deleteTeacherCourseRelationsByCourseId(int courseId)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "delete from teacher_course_relation where course_id=" + courseId;
                db.ExecuteUpdate(sql);

                // 记录删除教师课程关系日志
                Log.info(string.Format("成功删除课程ID={0}的所有教师关系", courseId));
            }
            catch (Exception ex)
            {
                // 记录删除教师课程关系异常日志
                Log.error(string.Format("删除教师课程关系失败：课程ID={0}, 错误信息={1}", courseId, ex.Message));
                throw;
            }
        }
        
        //查询所有教师课程关系记录
        public List<teacher_course_relation> GetAllTeacherCourseRelations()
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "select * from teacher_course_relation";
                DataTable dt = db.ExecuteQuery(sql);
                
                List<teacher_course_relation> relations = new List<teacher_course_relation>();
                
                foreach (DataRow row in dt.Rows)
                {
                    // 从数据库行创建teacher_course_relation对象
                    teacher_course_relation r = new teacher_course_relation(
                        Convert.ToInt32(row["teacher_id"]),
                        Convert.ToInt32(row["course_id"])
                    );
                    relations.Add(r);
                }
                
                // 记录查询教师课程关系日志
                Log.info(string.Format("成功查询所有教师课程关系，共{0}条记录", relations.Count));
                return relations;
            }
            catch (Exception ex)
            {
                // 记录查询教师课程关系异常日志
                Log.error(string.Format("查询教师课程关系失败：错误信息={0}", ex.Message));
                throw;
            }
        }

        //根据教师ID查询其教授的所有课程关系
        public List<teacher_course_relation> GetTeacherCourseRelationsByTeacherId(int teacherId)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "select * from teacher_course_relation where teacher_id=" + teacherId;
                DataTable dt = db.ExecuteQuery(sql);
                
                List<teacher_course_relation> relations = new List<teacher_course_relation>();
                
                foreach (DataRow row in dt.Rows)
                {
                    // 从数据库行创建teacher_course_relation对象
                    teacher_course_relation r = new teacher_course_relation(
                        Convert.ToInt32(row["teacher_id"]),
                        Convert.ToInt32(row["course_id"])
                    );
                    relations.Add(r);
                }
                
                // 记录查询教师课程关系日志
                Log.info(string.Format("成功查询教师ID={0}的课程关系，共{1}条记录", teacherId, relations.Count));
                return relations;
            }
            catch (Exception ex)
            {
                // 记录查询教师课程关系异常日志
                Log.error(string.Format("查询教师课程关系失败：教师ID={0}, 错误信息={1}", teacherId, ex.Message));
                throw;
            }
        }

        //根据课程ID查询教授该课程的所有教师关系
        public List<teacher_course_relation> GetTeacherCourseRelationsByCourseId(int courseId)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "select * from teacher_course_relation where course_id=" + courseId;
                DataTable dt = db.ExecuteQuery(sql);
                
                List<teacher_course_relation> relations = new List<teacher_course_relation>();
                
                foreach (DataRow row in dt.Rows)
                {
                    // 从数据库行创建teacher_course_relation对象
                    teacher_course_relation r = new teacher_course_relation(
                        Convert.ToInt32(row["teacher_id"]),
                        Convert.ToInt32(row["course_id"])
                    );
                    relations.Add(r);
                }
                
                // 记录查询教师课程关系日志
                Log.info(string.Format("成功查询课程ID={0}的教师关系，共{1}条记录", courseId, relations.Count));
                return relations;
            }
            catch (Exception ex)
            {
                // 记录查询教师课程关系异常日志
                Log.error(string.Format("查询教师课程关系失败：课程ID={0}, 错误信息={1}", courseId, ex.Message));
                throw;
            }
        }
    }
}