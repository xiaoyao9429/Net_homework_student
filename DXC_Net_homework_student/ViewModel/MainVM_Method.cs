using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC_Net_homework_student
{
    internal  partial class MainViewModel
    {

        private void LoadExcelData(object parameter)
        {
            try
            {
                // 获取所有被选中的学生
                var selectedStudents = StudentList.Where(s => s.IsSelected).ToList();

                if (selectedStudents.Count == 0)
                {
                    System.Windows.MessageBox.Show("请先选择要导出的学生！");
                    return;
                }

                // 准备导出路径和文件名
                string projectPath = System.AppDomain.CurrentDomain.BaseDirectory;
                // 向上移动两级目录，到达项目同级目录
                string exportPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(projectPath, "..", ".."));
                string fileName = "学生数据_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                string fullPath = System.IO.Path.Combine(exportPath, fileName);

                // 尝试使用EPPlus导出Excel
                bool exportSuccess = ExportToExcelUsingEPPlus(selectedStudents, fullPath);
                
                if (!exportSuccess)
                {
                    System.Windows.MessageBox.Show("无法使用EPPlus库导出Excel文件，请检查是否已安装该库。");
                    // 如果EPPlus不可用，尝试导出为CSV
                    fileName = "学生数据_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                    fullPath = System.IO.Path.Combine(exportPath, fileName);
                    ExportToCSV(selectedStudents, fullPath);
                }

                System.Windows.MessageBox.Show($"成功导出{selectedStudents.Count}名学生的数据到文件：{fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("导出学生数据失败: " + ex.Message);
                System.Windows.MessageBox.Show("导出学生数据失败: " + ex.Message);
            }
        }

        /// <summary>
        /// 使用EPPlus库导出数据到Excel
        /// </summary>
        /// <param name="students">要导出的学生列表</param>
        /// <param name="filePath">导出文件路径</param>
        /// <returns>导出是否成功</returns>
        private bool ExportToExcelUsingEPPlus(List<student> students, string filePath)
        {
            try
            {
                // 尝试加载EPPlus库
                var epplusAssembly = System.Reflection.Assembly.Load("EPPlus");
                if (epplusAssembly == null)
                {
                    return false;
                }

                // 使用反射创建Excel文件
                dynamic package = Activator.CreateInstance(epplusAssembly.GetType("OfficeOpenXml.ExcelPackage"));
                dynamic worksheet = package.Workbook.Worksheets.Add("学生数据");

                // 设置表头
                string[] headers = { "ID", "姓名", "性别", "出生日期", "电话", "地址", "语文成绩", "数学成绩", "英语成绩" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                    // 设置表头样式 - 只保留加粗，去掉背景色设置以避免System.Drawing依赖
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                }

                // 填充数据
                for (int row = 0; row < students.Count; row++)
                {
                    var student = students[row];
                    worksheet.Cells[row + 2, 1].Value = student.Id;
                    worksheet.Cells[row + 2, 2].Value = student.Name;
                    worksheet.Cells[row + 2, 3].Value = student.Sex;
                    worksheet.Cells[row + 2, 4].Value = student.Birthday;
                    worksheet.Cells[row + 2, 5].Value = student.Phone;
                    worksheet.Cells[row + 2, 6].Value = student.Address;
                    worksheet.Cells[row + 2, 7].Value = student.ScoreYW;
                    worksheet.Cells[row + 2, 8].Value = student.ScoreSX;
                    worksheet.Cells[row + 2, 9].Value = student.ScoreYY;
                }

                // 自动调整列宽
                worksheet.Cells[1, 1, students.Count + 1, 9].AutoFitColumns();

                // 保存文件
                package.SaveAs(new System.IO.FileInfo(filePath));
                return true;
            }
            catch (Exception)
            {
                // EPPlus不可用或导出失败
                return false;
            }
        }

        /// <summary>
        /// 导出数据到CSV文件
        /// </summary>
        /// <param name="students">要导出的学生列表</param>
        /// <param name="filePath">导出文件路径</param>
        private void ExportToCSV(List<student> students, string filePath)
        {
            // 创建CSV内容
            StringBuilder csvContent = new StringBuilder();
            
            // 添加表头
            csvContent.AppendLine("ID,姓名,性别,出生日期,电话,地址,语文成绩,数学成绩,英语成绩");
            
            // 添加数据行
            foreach (var student in students)
            {
                // 使用逗号分隔字段，对于包含逗号的字段需要用引号包围
                string[] fields = {
                    student.Id.ToString(),
                    EscapeCsvField(student.Name),
                    EscapeCsvField(student.Sex),
                    EscapeCsvField(student.Birthday),
                    EscapeCsvField(student.Phone),
                    EscapeCsvField(student.Address),
                    student.ScoreYW.ToString(),
                    student.ScoreSX.ToString(),
                    student.ScoreYY.ToString()
                };
                csvContent.AppendLine(string.Join(",", fields));
            }
            
            // 写入文件
            System.IO.File.WriteAllText(filePath, csvContent.ToString(), System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// 转义CSV字段中的特殊字符
        /// </summary>
        /// <param name="field">要转义的字段</param>
        /// <returns>转义后的字段</returns>
        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return "";
            
            // 如果字段包含逗号、引号或换行符，需要用引号包围
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            {
                // 替换字段中的引号为两个引号
                field = field.Replace("\"", "\"\"");
                // 用引号包围字段
                field = "\"" + field + "\"";
            }
            
            return field;
        }


        private void SearchStudent(object parameter)
        {
            try
            {
                // 无需检查是否至少输入了一个检索条件
                // 当所有条件都为空时，将返回所有学生信息

                // 从数据库加载所有学生数据
                studentModel model = new studentModel();
                List<student> allStudents = model.GetAllStudents();

                // 构建查询条件
                var query = allStudents.AsQueryable();

                // 学生ID检索
                if (!string.IsNullOrWhiteSpace(SearchStudentId))
                {
                    if (int.TryParse(SearchStudentId, out int searchId))
                    {
                        query = query.Where(s => s.Id == searchId);
                    }
                }

                // 学生姓名模糊检索
                if (!string.IsNullOrWhiteSpace(SearchStudentName))
                {
                    query = query.Where(s => s.Name.Equals(SearchStudentName));
                }

                // 学生性别检索 - 如果性别不为空则根据性别过滤，为空则忽略性别条件
                if (!string.IsNullOrWhiteSpace(SearchStudentSex))
                {
                    query = query.Where(s => s.Sex == SearchStudentSex);
                }

                // 执行查询
                var filteredStudents = query.ToList();

                // 清空现有列表并添加过滤后的学生
                StudentList.Clear();
                foreach (var student in filteredStudents)
                {
                    StudentList.Add(student);
                }

                // 如果没有找到匹配的学生，显示提示
                if (StudentList.Count == 0)
                {
                    System.Windows.MessageBox.Show("未找到符合条件的学生！");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("检索学生失败: " + ex.Message);
                System.Windows.MessageBox.Show("检索学生失败: " + ex.Message);
            }
        }
        private void OpenAddStudentWindow(object parameter)
        {
            _addStudentWidonw = new addStudentWindow();

            _addStudentWidonw.Closed += (sender, e) =>// // 当窗口关闭时刷新学生列表
            {

                LoadStudents();
            };

            _addStudentWidonw.Show();
        }

        /// <summary>
        /// 删除选中的学生
        /// </summary>
        /// <param name="parameter">命令参数</param>
        private void DeleteSelectedStudents(object parameter)
        {
            try
            {
                // 获取所有被选中的学生
                var selectedStudents = StudentList.Where(s => s.IsSelected).ToList();

                if (selectedStudents.Count == 0)
                {
                    System.Windows.MessageBox.Show("请先选择要删除的学生！");
                    return;
                }

                // 显示确认对话框
                var result = System.Windows.MessageBox.Show(
                    $"确定要删除选中的{selectedStudents.Count}名学生吗？",
                    "确认删除",
                    System.Windows.MessageBoxButton.YesNo,
                    System.Windows.MessageBoxImage.Warning);

                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    studentModel model = new studentModel();
                    int deletedCount = 0;

                    // 逐个删除学生
                    foreach (var student in selectedStudents)
                    {
                        model.deleteStudent(student.Id);
                        deletedCount++;
                    }

                    // 重新加载学生列表
                    LoadStudents();

                    // 重置全选状态
                    _isAllSelected = false;
                    OnPropertyChanged("IsAllSelected");

                    System.Windows.MessageBox.Show($"成功删除{deletedCount}名学生！");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除学生失败: " + ex.Message);
                System.Windows.MessageBox.Show("删除学生失败: " + ex.Message);
            }
        }

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="parameter">命令参数</param>
        private void UpdateStudent(object parameter)
        {
            try
            {
                // 获取所有被选中的学生
                var selectedStudents = StudentList.Where(s => s.IsSelected).ToList();

                if (selectedStudents.Count == 0)
                {
                    System.Windows.MessageBox.Show("请先选择要修改的学生信息！");
                    return;
                }

                studentModel model = new studentModel();
                int updatedCount = 0;

                // 逐个更新学生信息
                foreach (var student in selectedStudents)
                {
                    model.updateStudent(student);
                    updatedCount++;
                }

                // 更新成功后刷新学生列表
                LoadStudents();

                // 显示更新成功提示
                System.Windows.MessageBox.Show($"成功更新{updatedCount}名学生信息！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("更新学生信息失败: " + ex.Message);
                System.Windows.MessageBox.Show("更新学生信息失败: " + ex.Message);
            }
        }



        /// <summary>
        /// 从数据库加载学生数据
        /// </summary>
        public void LoadStudents()
        {
            try
            {
                studentModel model = new studentModel();
                List<student> studentList = model.GetAllStudents();

                // 清空现有数据
                StudentList.Clear();

                // 将student实体转换为StudentViewModel并添加到集合中
                foreach (var student in studentList)
                {
                    StudentList.Add(student);
                }
            }
            catch (Exception ex)
            {
                // 实际应用中应使用日志记录错误
                Console.WriteLine("加载学生数据失败: " + ex.Message);
            }
        }

        public void RefreshList(object parameter)
        {
            LoadStudents();
        }
        public void SelectStudent(object parameter)
        {
            if (parameter != null && parameter is student)
            {
                Student = parameter as student;
                Student.IsSelected = !Student.IsSelected;

                bool temp = Student.IsSelected;
                if (Student != null && temp == false)
                {
                    IsAllSelected = false;
                }

                else if (Student != null && temp == true)
                {
                    Func<bool> func = () =>
                    {
                        for (int i = 0; i < StudentList.Count; i++)
                        {

                            if (StudentList[i].IsSelected != true)
                            {

                                return false;
                            }
                        }

                        return true;
                    };

                    if (func() == true)
                    {
                        IsAllSelected = true;
                    }
                }
            }

            else
            {
                //todo
            }
        }

        public void SelectAllStudent(object parameter)
        {

            if (IsAllSelected == true)
            {

                for (int i = 0; i < StudentList.Count; i++)
                {
                    StudentList[i].IsSelected = true;
                    //OnPropertyChanged("StudentList[" + i + "].IsSelected");这样写没用
                }

            }

            else
            {
                //todo
            }


        }

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

        /// <summary>
        /// 更新建议学生姓名列表
        /// </summary>
        private void UpdateSuggestedStudentNames()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchStudentName))
                {
                    SuggestedStudentNames = new List<string>();
                    return;
                }

                studentModel model = new studentModel();
                List<student> allStudents = model.GetAllStudents();
                
                // 模糊匹配学生姓名
                List<string> suggestedNames = allStudents
                    .Where(s => s.Name.StartsWith(SearchStudentName))
                    .Select(s => s.Name)
                    .Distinct() // 去重
                    .ToList();

                SuggestedStudentNames = suggestedNames;
            }
            catch (Exception ex)
            {
                Console.WriteLine("更新建议学生姓名列表失败: " + ex.Message);
            }
        }

        /// <summary>
        /// 选择建议的学生姓名
        /// </summary>
        /// <param name="parameter">命令参数</param>
        public void SelectSuggestedName(object parameter)
        {
            if (parameter != null && parameter is string)
            {
                string selectedName = (string)parameter;
                SearchStudentName = selectedName;
                SelectedSuggestedName = selectedName;
                // 选择后清空建议列表
                SuggestedStudentNames = new List<string>();
            }
        }
    }
}
