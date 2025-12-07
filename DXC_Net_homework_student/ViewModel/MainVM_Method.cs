using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC_Net_homework_student
{
    internal  partial class MainViewModel
    {
        /// <summary>
        /// 选择建议的学生ID
        /// </summary>
        /// <param name="parameter">命令参数</param>
        public void SelectSuggestedId(object parameter)
        {
            if (parameter != null && parameter is int)
            {
                int selectedId = (int)parameter;
                SearchStudentId = selectedId.ToString();
                SelectedSuggestedId = selectedId;
                // 选择后清空建议列表
                SuggestedStudentIds = new List<int>();
                // 自动执行检索
                SearchStudent(null);
            }
        }


        private void UpdateSuggestedStudentIds()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchStudentId))
                {
                    SuggestedStudentIds = new List<int>();
                    return;
                }

                studentModel model = new studentModel();
                List<student> allStudents = model.GetAllStudents();
                
                // 模糊匹配学生ID
                List<int> suggestedIds = allStudents
                    .Where(s => s.Id.ToString().StartsWith(SearchStudentId))
                    .Select(s => s.Id)
                    .ToList();

                SuggestedStudentIds = suggestedIds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("更新建议学生ID列表失败: " + ex.Message);
            }
        }
    }
}
