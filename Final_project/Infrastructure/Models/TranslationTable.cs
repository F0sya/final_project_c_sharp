using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Final_project_wpf.Infrastructure.Models
{

    [Table("Translation")]
    public class TranslationTable : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }
        [Column("word")]
        public string Word { get; set; }
        [Column("translation")]
        public string Translation { get; set; }
        [Column("sentence")]
        public string? Sentence { get; set; }
        [Column("translatedSentence")]
        public string? TranslatedSentence { get; set; }
    }
}
