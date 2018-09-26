using Newtonsoft.Json;

namespace Brio.Domain.Utils
{
    public class PagingParameter
    {
        const int maxPageSize = 20;

        public int pageNumber { get; set; } = 1;

        [JsonIgnore]
        private int _pageSize { get; set; } = 10;

        public string Sort { get; set; } = "";

        public int pageSize
        {

            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
