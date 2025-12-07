using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DXC_Net_homework_student
{
    /// <summary>
    /// 将整数转换为可见性的转换器
    /// </summary>
    public class IntToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// 将整数转换为可见性
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="parameter">参数</param>
        /// <param name="culture">文化信息</param>
        /// <returns>可见性</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                return count > 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        /// <summary>
        /// 将可见性转换为整数（不使用）
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="parameter">参数</param>
        /// <param name="culture">文化信息</param>
        /// <returns>整数</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
