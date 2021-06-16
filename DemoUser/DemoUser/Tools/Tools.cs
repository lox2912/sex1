using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DemoUser.Tools
{
   public static class Tools
    {
        public static List<T> CreateComboBoxList<T>(List<T> sourse, T item)
        {
            var result = sourse.ToList();
            result.Insert(0, item);
            return result;
        }

        public static BitmapImage BytesToImage(byte[] array)
        {
            using (MemoryStream ms=new MemoryStream(array))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
