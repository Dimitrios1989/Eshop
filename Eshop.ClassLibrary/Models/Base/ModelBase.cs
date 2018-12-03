using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eshop.ClassLibrary.Models.Base
{
    /// <summary>
    /// Στανταρ ιδιότητες για όλες τις οντότητες του e-shop μας. 
    /// </summary>
    public class ModelBase
    {
        [Key] // Αυτο εδω δηλωνει οτι ειναι το πρωτευον κλειδι
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

    }
}