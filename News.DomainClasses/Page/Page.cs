using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.DomainClasses.Page
{
    public class Page
    {
        public Page()
        {

        }

        [Key]
        public int PageId { get; set; }
        [Display(Name ="گروه خبری")]
        public int GroupId { get; set; }
        [Required]
        [MaxLength(400)]
        [Display(Name ="عنوان خبر")]
        public string PageTitle { get; set; }
        [Required]
        [MaxLength(300)]
        [Display(Name = "توضیح مختصر خبر")]
        public string ShortDescription { get; set; }
        [Required]
        [Display(Name = "توضیحات کامل خبر")]
        public string PageContent { get; set; }
        [Display(Name ="تعداد بازدید")]
        public int Visit { get; set; }
        [Display(Name ="عکس")]
        public string ImageName { get; set; }
        [Display(Name = "نمایش در اسلایدر ؟")]
        public bool ShowSlider { get; set; }
        [Display(Name = "کلمات کلیدی خبر(با , جدا کنید)")]
        public string PageTag { get; set; }
        [Display(Name ="تاریخ ساخت")]
        public DateTime CreateDate { get; set; }
        [Display(Name ="نویسنده")]
        public string Writer { get; set; }
        [Display(Name ="گروه خبر")]
        public virtual PageGroups.PageGroup PageGroup { get; set; }

    }
}
