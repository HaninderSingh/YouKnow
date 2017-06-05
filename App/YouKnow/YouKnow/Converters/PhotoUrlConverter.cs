using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YouKnow.Constants;

namespace YouKnow.Converters
{
    class PhotoUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string file = value as string;
            if (file != null)
            {
                return AppConstants.CONTACT_PHOTO_URL + file;
            }
            else
            {
                return "";
            }
        }

        

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
