﻿namespace SpeiderappAPI.DataTransferObjects
{
    public class BadgeTagDto
    {
        public long TagID { get; set; }
        public string Value { get; set; }
        public long CategoryID { get; set; }
        public string CategoryTitle { get; set; }
    }
}
