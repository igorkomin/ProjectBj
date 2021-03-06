﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBj.Entities
{
    [Table("SystemLog")]
    public class SystemLog : BaseEntity
    {
        public string MachineName { get; set; }
        public string SiteName { get; set; }
        public string Level { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Properties { get; set; }
        public string ServerName { get; set; }
        public string Port { get; set; }
        public string Url { get; set; }
        public bool Https { get; set; }
        public string ServerAddress { get; set; }
        public string RemoteAddress { get; set; }
        public string CallSite { get; set; }
        public string Exception { get; set; }
    }
}
