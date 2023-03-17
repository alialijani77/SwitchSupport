using SwitchSupport.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.ViewModels.Admin.Tag
{
	public class FilterTagAdminViewModel : Paging<Domain.Entities.Tags.Tag>
	{
		public string? Title { get; set; }

		public FilterTagAdminStatus Status { get; set; }
	}

	public enum FilterTagAdminStatus
	{
		[Display(Name = "همه")] All,
		[Display(Name = "بدون توضیحات")] NoDescription,
		[Display(Name = "دارای توضیحات")] HasDescription
	}
}
