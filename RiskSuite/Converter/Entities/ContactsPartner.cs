using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class ContactsPartner
    {
        public string Id { get; set; }
        public string Partner { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string SurNameDat { get; set; }
        public string Patronymic { get; set; }
        public string Male { get; set; }
        public string Position { get; set; }
        public string Adress { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string Rus { get; set; }
        public string IsOfficial { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Admuser { get; set; }
        public DateTime? Admdate { get; set; }
    }
}
