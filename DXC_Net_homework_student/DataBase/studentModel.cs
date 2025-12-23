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
            try
            {
                DBConnect db = new DBConnect();
                // 明确指定列名，避免列数不匹配问题，且分数字段不需要单引号
                string sql = string.Format("insert into student(姓名, 性别, 出生年月, 电话, 住址, 语文, 数学, 英语, 学生照片) values('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}, {7}, '{8}')",
                    student.Name, student.Sex, student.Birthday, student.Phone, student.Address,
                    student.ScoreYW, student.ScoreSX, student.ScoreYY, student.PhotoPath);

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

                // 记录添加学生日志
                Log.info(string.Format("成功添加学生：ID={0}, 姓名={1}, 性别={2}, 电话={3}", student.Id, student.Name, student.Sex, student.Phone));
            }
            catch (Exception ex)
            {
                // 记录添加学生异常日志
                Log.error(string.Format("添加学生失败：姓名={0}, 错误信息={1}", student.Name, ex.Message));
                throw;
            }
        }

        //删除一条学生记录
        public void deleteStudent(int id)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "delete from student where 学生ID=" + id;
                db.ExecuteUpdate(sql);

                // 记录删除学生日志
                Log.info(string.Format("成功删除学生：ID={0}", id));
            }
            catch (Exception ex)
            {
                // 记录删除学生异常日志
                Log.error(string.Format("删除学生失败：ID={0}, 错误信息={1}", id, ex.Message));
                throw;
            }
        }

        //修改一条学生记录
        public void updateStudent(student student)
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "update student set 姓名='" + student.Name + "',性别='" + student.Sex + "',出生年月='" +
                    student.Birthday + "',电话='" + student.Phone + "',住址='" + student.Address +
                    "',语文=" + student.ScoreYW + ",数学=" + student.ScoreSX + ",英语=" + student.ScoreYY + " where 学生ID=" + student.Id;
                db.ExecuteUpdate(sql);

                // 记录更新学生日志
                Log.info(string.Format("成功更新学生：ID={0}, 姓名={1}, 性别={2}, 电话={3}", student.Id, student.Name, student.Sex, student.Phone));
            }
            catch (Exception ex)
            {
                // 记录更新学生异常日志
                Log.error(string.Format("更新学生失败：ID={0}, 姓名={1}, 错误信息={2}", student.Id, student.Name, ex.Message));
                throw;
            }
        }
        
        //查询所有学生记录
        public List<student> GetAllStudents()
        {
            try
            {
                DBConnect db = new DBConnect();
                string sql = "select * from student";
                DataTable dt = db.ExecuteQuery(sql);
                
                List<student> students = new List<student>();
                
                foreach (DataRow row in dt.Rows)
                {
                    // 获取照片文件名
                    string photoFileName = row["学生照片"].ToString();
                    
                    // 确保照片文件名包含.png扩展名
                    if (!string.IsNullOrEmpty(photoFileName) && !photoFileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        photoFileName += ".png";
                    }
                    
                    // 从数据库行创建student对象
                    student s = new student(
                        row["姓名"].ToString(),
                        row["性别"].ToString(),
                        FormatBirthday(row["出生年月"]), // 使用正确的日期格式化方法
                        row["电话"].ToString(),
                        row["住址"].ToString(),
                        Convert.ToInt32(row["语文"]),
                        Convert.ToInt32(row["数学"]),
                        Convert.ToInt32(row["英语"]),
                        photoFileName // 使用带有扩展名的照片文件名
                    );
                    s.Id = Convert.ToInt32(row["学生ID"]);
                    students.Add(s);
                }
                
                // 记录查询学生日志
                Log.info(string.Format("成功查询所有学生，共{0}条记录", students.Count));
                return students;
            }
            catch (Exception ex)
            {
                // 记录查询学生异常日志
                Log.error(string.Format("查询学生失败：错误信息={0}", ex.Message));
                throw;
            }
        }
        
        // 格式化出生日期
        private string FormatBirthday(object birthdayObj)
        {
            if (birthdayObj == DBNull.Value || birthdayObj == null)
            {
                return "";
            }
            
            try
            {
                // 将对象转换为DateTime
                DateTime birthday = Convert.ToDateTime(birthdayObj);
                
                // 格式化为yyyy-MM-dd格式，添加分隔符提高可读性
                return birthday.ToString("yyyy-MM-dd");
            }
            catch (FormatException)
            {
                // 如果转换失败，尝试直接处理字符串
                string birthdayStr = birthdayObj.ToString();
                
                // 移除所有空格
                birthdayStr = birthdayStr.Replace(" ", "");
                
                // 如果包含时间部分（有冒号），则只保留日期部分
                if (birthdayStr.Contains(":"))
                {
                    birthdayStr = birthdayStr.Split(':')[0];
                }
                
                // 确保是8位数字格式
                if (birthdayStr.Length > 8)
                {
                    birthdayStr = birthdayStr.Substring(0, 8);
                }
                
                // 如果字符串长度为8，添加分隔符
                if (birthdayStr.Length == 8)
                {
                    try
                    {
                        // 尝试按yyyyMMdd格式解析并重新格式化为yyyy-MM-dd
                        DateTime birthday = DateTime.ParseExact(birthdayStr, "yyyyMMdd", null);
                        return birthday.ToString("yyyy-MM-dd");
                    }
                    catch
                    {
                        // 如果解析失败，返回原始字符串
                        return birthdayStr;
                    }
                }
                
                return birthdayStr;
            }
        }
    }
}