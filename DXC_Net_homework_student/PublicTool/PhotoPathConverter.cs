using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DXC_Net_homework_student
{
    /// <summary>
    /// 照片路径转换器，将数据库中的照片文件名转换为完整的文件路径
    /// </summary>
    public class PhotoPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string photoName && !string.IsNullOrEmpty(photoName))
            {
                try
                {
                    // 确保照片文件名包含.png扩展名
                    if (!photoName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        photoName += ".png";
                    }

                    // 尝试两种路径：
                    // 1. 首先尝试相对于应用程序BaseDirectory的Photo文件夹（部署后）
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string photoPath = Path.Combine(baseDirectory, "Photo", photoName);
                    
                    // 2. 如果第一种路径不存在，尝试向上两级目录（开发环境）
                    if (!File.Exists(photoPath))
                    {
                        // 向上两级：bin/Debug -> 项目根目录
                        string projectDirectory = Path.GetFullPath(Path.Combine(baseDirectory, "..", ".."));
                        photoPath = Path.Combine(projectDirectory, "Photo", photoName);
                        Console.WriteLine("尝试项目根目录下的Photo文件夹: " + photoPath);
                    }
                    else
                    {
                        Console.WriteLine("使用BaseDirectory下的Photo文件夹: " + photoPath);
                    }
                    
                    // 检查文件是否存在
                    if (File.Exists(photoPath))
                    {
                        // 创建BitmapImage对象并设置缓存选项
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(photoPath);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        Console.WriteLine("成功加载照片: " + photoName);
                        return bitmap;
                    }
                    else
                    {
                        Console.WriteLine("照片文件不存在: " + photoPath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("加载照片时出错: " + ex.Message);
                }
            }
            
            // 如果照片不存在或加载失败，返回默认图像
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}