using System;

namespace Settings
{
    public class Settings
    {
        private static Settings _current;

        public string ConnectionString { get; set; }

        public static Settings Current => _current;

        public static void Initialize(Settings settings)
        {
            _current = settings;
        }
    }
}
