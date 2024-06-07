using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RequestHelper
{
    public class ImageColorDtoCompare : IEqualityComparer<ImageColorDto>
    {
        public bool Equals(ImageColorDto? x, ImageColorDto? y)
        {
            if (x == null || y == null)
                return false;

            return x.ColorId == y.ColorId && x.PictureUrl == y.PictureUrl;
        }

        public int GetHashCode([DisallowNull] ImageColorDto obj)
        {
            if (obj == null)
                return 0;

            int hashId = obj.ColorId.GetHashCode();
            int hashValue = obj.PictureUrl == null ? 0 : obj.PictureUrl.GetHashCode();

            return hashId ^ hashValue;
        }
    }
}
