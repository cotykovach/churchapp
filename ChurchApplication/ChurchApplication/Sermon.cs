using System;

namespace ChurchApplication
{
    class Sermon
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public int hideTitle { get; set; }
        public string Text { get; set; }
    }
}